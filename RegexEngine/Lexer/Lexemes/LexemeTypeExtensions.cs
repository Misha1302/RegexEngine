namespace RegexEngine.Lexer.Lexemes;

public static class LexemeTypeExtensions
{
    public static bool IsController(this LexemeType lexemeType) =>
        lexemeType is LexemeType.OneOrMoreTimes or LexemeType.ZeroOrOneTimes or LexemeType.ZeroOrMoreTimes;
}