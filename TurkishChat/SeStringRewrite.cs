using System;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;

namespace TurkishChat;

internal static class SeStringRewrite
{
    // Only text payloads are touched; macro payloads carry length fields and must stay byte-exact.
    public static bool RewritePayloads(SeString text, bool iota)
    {
        var changed = false;
        foreach (var payload in text.Payloads)
        {
            if (payload is not TextPayload { Text: { } original } textPayload)
                continue;

            var rewritten = LetterMap.Rewrite(original, iota);
            if (ReferenceEquals(rewritten, original))
                continue;

            textPayload.Text = rewritten;
            changed = true;
        }

        return changed;
    }

    public static byte[]? Rewrite(ReadOnlySpan<byte> encoded, bool iota)
    {
        if (!LetterMap.ContainsTarget(encoded))
            return null;

        var text = SeString.Parse(encoded);
        return RewritePayloads(text, iota) ? text.Encode() : null;
    }
}
