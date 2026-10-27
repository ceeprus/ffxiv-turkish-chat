using System.Text;
using Xunit;

namespace TurkishChat.Tests;

public class LetterMapTests
{
    [Theory]
    [InlineData('ğ', false, 'g')]
    [InlineData('Ğ', false, 'G')]
    [InlineData('ş', false, 's')]
    [InlineData('ı', false, 'i')]
    [InlineData('ı', true, 'ι')]
    [InlineData('İ', false, 'I')]
    [InlineData('İ', true, 'I')]
    [InlineData('ğ', true, 'g')]
    public void Map_ReplacesMissingLetters(char input, bool iota, char expected)
    {
        Assert.Equal(expected, LetterMap.Map(input, iota));
    }

    [Theory]
    [InlineData('ç')]
    [InlineData('Ç')]
    [InlineData('ö')]
    [InlineData('Ö')]
    [InlineData('ü')]
    [InlineData('Ü')]
    [InlineData('Ş')]
    [InlineData('â')]
    [InlineData('a')]
    [InlineData('ж')]
    public void Map_LeavesOtherCharacters(char input)
    {
        Assert.Equal(input, LetterMap.Map(input, false));
        Assert.Equal(input, LetterMap.Map(input, true));
    }

    [Fact]
    public void Rewrite_FixesFullSentence()
    {
        const string input = "Ağaç dalında kuşu gördüm, İstanbul'a gidiyorum";

        Assert.Equal("Agaç dalinda kusu gördüm, Istanbul'a gidiyorum", LetterMap.Rewrite(input, false));
        Assert.Equal("Agaç dalιnda kusu gördüm, Istanbul'a gidiyorum", LetterMap.Rewrite(input, true));
    }

    [Fact]
    public void Rewrite_ReturnsSameInstanceWhenNothingChanges()
    {
        var input = "çok güzel Şeker 😀";

        Assert.Same(input, LetterMap.Rewrite(input, false));
    }

    [Fact]
    public void Rewrite_HandlesEmptyString()
    {
        Assert.Same(string.Empty, LetterMap.Rewrite(string.Empty, false));
    }

    [Theory]
    [InlineData("ğ")]
    [InlineData("Ğ")]
    [InlineData("ş")]
    [InlineData("ı")]
    [InlineData("İ")]
    [InlineData("abc ı def")]
    public void ContainsTarget_FindsMissingLetters(string text)
    {
        Assert.True(LetterMap.ContainsTarget(Encoding.UTF8.GetBytes(text)));
    }

    [Theory]
    [InlineData("")]
    [InlineData("abc")]
    [InlineData("çöüŞâ")]
    [InlineData("жι😀")]
    public void ContainsTarget_IgnoresOtherText(string text)
    {
        Assert.False(LetterMap.ContainsTarget(Encoding.UTF8.GetBytes(text)));
    }
}
