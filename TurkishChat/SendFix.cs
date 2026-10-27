using System;
using Dalamud.Hooking;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI;

namespace TurkishChat;

internal sealed unsafe class SendFix : IDisposable
{
    private readonly Hook<UIModule.Delegates.ProcessChatBoxEntry> hook;
    private readonly Configuration config;
    private readonly IPluginLog log;

    public SendFix(IGameInteropProvider interop, Configuration config, IPluginLog log)
    {
        this.config = config;
        this.log = log;

        var address = (nint)UIModule.MemberFunctionPointers.ProcessChatBoxEntry;
        if (address == 0)
            throw new InvalidOperationException("ProcessChatBoxEntry was not found");

        hook = interop.HookFromAddress<UIModule.Delegates.ProcessChatBoxEntry>(address, Detour);
        hook.Enable();
    }

    private void Detour(UIModule* uiModule, Utf8String* message, nint a4, bool saveToHistory)
    {
        try
        {
            if (config.FixOutgoing)
            {
                var rewritten = SeStringRewrite.Rewrite(message->AsSpan(), config.DotlessIAsIota);
                if (rewritten != null)
                    message->SetString((ReadOnlySpan<byte>)rewritten);
            }
        }
        catch (Exception e)
        {
            log.Error(e, "Could not rewrite an outgoing message");
        }

        hook.Original(uiModule, message, a4, saveToHistory);
    }

    public void Dispose() => hook.Dispose();
}
