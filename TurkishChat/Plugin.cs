using System;
using System.Collections.Generic;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace TurkishChat;

public sealed class Plugin : IDalamudPlugin
{
    private readonly IDalamudPluginInterface pluginInterface;
    private readonly IPluginLog log;
    private readonly List<IDisposable> parts = [];
    private readonly WindowSystem windowSystem = new("TurkishChat");
    private readonly ConfigWindow configWindow;

    public Plugin(
        IDalamudPluginInterface pluginInterface,
        IGameInteropProvider interop,
        IChatGui chatGui,
        IAddonLifecycle addonLifecycle,
        IPluginLog log)
    {
        this.pluginInterface = pluginInterface;
        this.log = log;
        var config = pluginInterface.GetPluginConfig() as Configuration ?? new Configuration();

        Start(log, "typing fix", () => new TypingFix(interop, pluginInterface.UiBuilder, config, log));
        Start(log, "send fix", () => new SendFix(interop, config, log));
        Start(log, "received fix", () => new ReceivedFix(chatGui, config, log));
        Start(log, "bubble fix", () => new BubbleFix(addonLifecycle, config, log));

        configWindow = new ConfigWindow(config, pluginInterface);
        windowSystem.AddWindow(configWindow);
        pluginInterface.UiBuilder.Draw += windowSystem.Draw;
        pluginInterface.UiBuilder.OpenConfigUi += configWindow.Toggle;
        pluginInterface.UiBuilder.OpenMainUi += configWindow.Toggle;
    }

    // Each part starts on its own so one failed hook leaves the rest working.
    private void Start(IPluginLog log, string name, Func<IDisposable> create)
    {
        try
        {
            parts.Add(create());
        }
        catch (Exception e)
        {
            log.Error(e, $"Could not start the {name}");
        }
    }

    public void Dispose()
    {
        pluginInterface.UiBuilder.OpenMainUi -= configWindow.Toggle;
        pluginInterface.UiBuilder.OpenConfigUi -= configWindow.Toggle;
        pluginInterface.UiBuilder.Draw -= windowSystem.Draw;
        windowSystem.RemoveAllWindows();

        for (var i = parts.Count - 1; i >= 0; i--)
        {
            try
            {
                parts[i].Dispose();
            }
            catch (Exception e)
            {
                log.Error(e, $"Could not stop the part at index {i}");
            }
        }
    }
}
