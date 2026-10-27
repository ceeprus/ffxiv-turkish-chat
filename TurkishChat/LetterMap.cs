using System;

namespace TurkishChat;

public static class LetterMap
{
    public static char Map(char c, bool iota) => c switch
    {
        'ğ' => 'g',
        'Ğ' => 'G',
        'ş' => 's',
        'ı' => iota ? 'ι' : 'i',
        'İ' => 'I',
        _ => c,
    };

    public static string Rewrite(string s, bool iota)
    {
        var first = s.AsSpan().IndexOfAny("ğĞşıİ");
        if (first < 0)
            return s;

        return string.Create(s.Length, (s, iota, first), static (span, state) =>
        {
            state.s.AsSpan().CopyTo(span);
            for (var i = state.first; i < span.Length; i++)
                span[i] = Map(span[i], state.iota);
        });
    }

    // UTF-8 for ğ Ğ ş ı İ is C4 9F, C4 9E, C5 9F, C4 B1, C4 B0; C4 and C5 are never continuation bytes.
    public static bool ContainsTarget(ReadOnlySpan<byte> utf8)
    {
        for (var i = 0; i < utf8.Length - 1; i++)
        {
            var lead = utf8[i];
            var next = utf8[i + 1];
            if ((lead == 0xC4 && next is 0x9E or 0x9F or 0xB0 or 0xB1) || (lead == 0xC5 && next == 0x9F))
                return true;
        }

        return false;
    }
}
