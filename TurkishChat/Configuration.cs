using System;
using Dalamud.Configuration;

namespace TurkishChat;

[Serializable]
public sealed class Configuration : IPluginConfiguration
{
    public int Version { get; set; }

    public bool FixOutgoing { get; set; } = true;

    public bool FixReceived { get; set; } = true;

    public bool DotlessIAsIota { get; set; }
}
