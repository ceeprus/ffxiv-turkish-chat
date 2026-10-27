using System;
using System.Collections.Generic;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace TurkishChat;

public sealed class Plugin : IDalamudPlugin
{
    private readonly List<IDisposable> parts = [];

    public Plugin(
        IDalamudPluginInterface pluginInterface,
        IGameInteropProvider interop,
        IChatGui chatGui,
        IAddonLifecycle addonLifecycle,
        IPluginLog log)
    {
        var config = pluginInterface.GetPluginConfig() as Configuration ?? new Configuration();

        Start(log, "typing fix", () => new TypingFix(interop, pluginInterface.UiBuilder, config, log));
        Start(log, "received fix", () => new ReceivedFix(chatGui, config, log));
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
        for (var i = parts.Count - 1; i >= 0; i--)
            parts[i].Dispose();
    }
}
