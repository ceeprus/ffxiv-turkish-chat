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

        // The game font cannot draw the letters being replaced, so the left column names them.
        ImGui.Spacing();
        ImGui.TextUnformatted("Letters the game font can't show become plain ones:");
        if (ImGui.BeginTable("##swaps", 2, ImGuiTableFlags.SizingFixedFit))
        {
            SwapRow("soft g (g with a breve)", "g, capital G");
            SwapRow("s with a cedilla", "s");
            SwapRow("dotless i", config.DotlessIAsIota ? "ι" : "i");
            SwapRow("capital I with a dot", "I");
            ImGui.EndTable();
        }

        ImGui.TextUnformatted("Capital Ş, ç, ö and ü stay as they are.");
    }

    private static void SwapRow(string letter, string becomes)
    {
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TextUnformatted(letter);
        ImGui.TableNextColumn();
        ImGui.TextUnformatted(becomes);
    }

    private void Save() => pluginInterface.SavePluginConfig(config);
}
