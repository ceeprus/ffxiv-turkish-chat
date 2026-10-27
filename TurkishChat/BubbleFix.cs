using System;
using Dalamud.Game.Addon.Lifecycle;
using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.UI;

namespace TurkishChat;

internal sealed unsafe class BubbleFix : IDisposable
{
    private const string AddonName = "_MiniTalk";

    private readonly IAddonLifecycle addonLifecycle;
    private readonly Configuration config;
    private readonly IPluginLog log;
    private bool loggedFailure;

    public BubbleFix(IAddonLifecycle addonLifecycle, Configuration config, IPluginLog log)
    {
        this.addonLifecycle = addonLifecycle;
        this.config = config;
        this.log = log;
        addonLifecycle.RegisterListener(AddonEvent.PreDraw, AddonName, OnPreDraw);
    }

    private void OnPreDraw(AddonEvent type, AddonArgs args)
    {
        if (!config.FixReceived)
            return;

        var addon = args.GetAddonPointer<AddonMiniTalk>();
        if (addon == null)
            return;

        for (var i = 0; i < addon->TalkBubbles.Length; i++)
        {
            try
            {
                var node = addon->TalkBubbles[i].BubbleTextNode;
                if (node == null)
                    continue;

                var rewritten = SeStringRewrite.Rewrite(node->NodeText.AsSpan(), config.DotlessIAsIota);
                if (rewritten == null)
                    continue;

                node->SetText((ReadOnlySpan<byte>)rewritten);
                log.Debug($"Rewrote speech bubble slot {i}");
            }
            catch (Exception e)
            {
                if (!loggedFailure)
                {
                    loggedFailure = true;
                    log.Error(e, $"Could not rewrite speech bubble slot {i}");
                }
            }
        }
    }

    public void Dispose() => addonLifecycle.UnregisterListener(AddonEvent.PreDraw, AddonName, OnPreDraw);
}
