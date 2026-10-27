using System;
using System.Runtime.InteropServices;
using Dalamud.Bindings.ImGui;
using Dalamud.Hooking;
using Dalamud.Interface;
using Dalamud.Plugin.Services;
using GameFramework = FFXIVClientStructs.FFXIV.Client.System.Framework.Framework;

namespace TurkishChat;

internal sealed unsafe class TypingFix : IDisposable
{
    private const uint WmChar = 0x0102;

    private readonly Hook<DispatchMessageWDelegate> hook;
    private readonly DispatchMessageWDelegate original;
    private readonly IUiBuilder uiBuilder;
    private readonly Configuration config;
    private readonly IPluginLog log;
    private volatile bool imGuiWantsText;

    public TypingFix(IGameInteropProvider interop, IUiBuilder uiBuilder, Configuration config, IPluginLog log)
    {
        this.uiBuilder = uiBuilder;
        this.config = config;
        this.log = log;

        // Dalamud hooks the same import; the two hooks chain.
        hook = interop.HookFromImport<DispatchMessageWDelegate>(null, "user32.dll", "DispatchMessageW", 0, Detour);
        original = hook.Original;
        hook.Enable();
        uiBuilder.Draw += CacheImGuiState;
        uiBuilder.HideUi += ClearImGuiState;
    }

    private delegate nint DispatchMessageWDelegate(Msg* msg);

    // Read once per frame so the detour never calls ImGui.
    private void CacheImGuiState() => imGuiWantsText = ImGui.GetIO().WantTextInput;

    // Draw does not run while plugin UI is hidden, so the cached flag would go stale.
    private void ClearImGuiState() => imGuiWantsText = false;

    private nint Detour(Msg* msg)
    {
        try
        {
            if (config.FixOutgoing && msg->Message == WmChar && msg->WParam <= 0xFFFF && !imGuiWantsText && IsGameWindow(msg->Hwnd))
                msg->WParam = LetterMap.Map((char)msg->WParam, config.DotlessIAsIota);
        }
        catch (Exception e)
        {
            log.Error(e, "Could not rewrite a typed character");
        }

        return original(msg);
    }

    // Dalamud's pop-out ImGui windows are separate HWNDs and must keep the real letters.
    private static bool IsGameWindow(nint hwnd)
    {
        var window = GameFramework.Instance()->GameWindow;
        return window != null && window->WindowHandle == hwnd;
    }

    public void Dispose()
    {
        uiBuilder.HideUi -= ClearImGuiState;
        uiBuilder.Draw -= CacheImGuiState;
        hook.Dispose();
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct Msg
    {
        public nint Hwnd;
        public uint Message;
        public nuint WParam;
        public nint LParam;
        public uint Time;
        public int PointX;
        public int PointY;
        public uint Private;
    }
}
