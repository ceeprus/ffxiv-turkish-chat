using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;

namespace TurkishChat;

internal sealed class ConfigWindow : Window
{
    private readonly Configuration config;
    private readonly IDalamudPluginInterface pluginInterface;

    public ConfigWindow(Configuration config, IDalamudPluginInterface pluginInterface)
        : base("Turkish Chat Support###TurkishChatConfig", ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoCollapse)
    {
        this.config = config;
        this.pluginInterface = pluginInterface;
    }

    public override void Draw()
    {
        var fixOutgoing = config.FixOutgoing;
        if (ImGui.Checkbox("Fix my messages (typing and sending)", ref fixOutgoing))
        {
            config.FixOutgoing = fixOutgoing;
            Save();
        }

        var fixReceived = config.FixReceived;
        if (ImGui.Checkbox("Fix messages and speech bubbles from other players", ref fixReceived))
        {
            config.FixReceived = fixReceived;
            Save();
        }

        ImGui.TextUnformatted("Dotless i becomes");
        ImGui.SameLine();
        if (ImGui.RadioButton("i", !config.DotlessIAsIota))
        {
            config.DotlessIAsIota = false;
            Save();
        }

        ImGui.SameLine();
        if (ImGui.RadioButton("ι (Greek iota)", config.DotlessIAsIota))
        {
            config.DotlessIAsIota = true;
            Save();
        }

        ImGui.Spacing();
        ImGui.TextUnformatted($"Swaps: g with breve to g, s with cedilla to s, dotless i to {(config.DotlessIAsIota ? "ι" : "i")}, dotted capital I to I");
    }

    private void Save() => pluginInterface.SavePluginConfig(config);
}
