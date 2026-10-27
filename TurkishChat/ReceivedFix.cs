using System;
using Dalamud.Game.Chat;
using Dalamud.Plugin.Services;

namespace TurkishChat;

internal sealed class ReceivedFix : IDisposable
{
    private readonly IChatGui chatGui;
    private readonly Configuration config;
    private readonly IPluginLog log;

    public ReceivedFix(IChatGui chatGui, Configuration config, IPluginLog log)
    {
        this.chatGui = chatGui;
        this.config = config;
        this.log = log;
        chatGui.CheckMessageHandled += OnMessage;
    }

    private void OnMessage(IHandleableChatMessage message)
    {
        if (!config.FixReceived)
            return;

        try
        {
            var text = message.Message;
            if (SeStringRewrite.RewritePayloads(text, config.DotlessIAsIota))
                message.Message = text;
        }
        catch (Exception e)
        {
            log.Error(e, "Could not rewrite a received message");
        }
    }

    public void Dispose() => chatGui.CheckMessageHandled -= OnMessage;
}
