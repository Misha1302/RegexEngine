namespace RegexEngine.Lexer.Lexemes;

public static class LexemesHelper
{
    public static readonly List<(string key, LexemeType value)> LexemeTexts;

    static LexemesHelper()
    {
        LexemeTexts =
        [
            (".", LexemeType.AnyChar),
            ("+", LexemeType.OneOrMoreTimes),
            ("?", LexemeType.ZeroOrOneTimes),
            ("*", LexemeType.ZeroOrMoreTimes),
            ("\\d", LexemeType.AnyDigit),
            ("\\w", LexemeType.AnyWordChar),
            ("{", LexemeType.LeftCurlyBrace),
            ("}", LexemeType.RightCurlyBrace),
            ("(", LexemeType.StartOfInnerScope),
            (")", LexemeType.EndOfInnerScope)
        ];
        LexemeTexts = LexemeTexts.OrderBy(x => x.key.Length).ToList();
    }
}