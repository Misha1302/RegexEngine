namespace RegexEngine.Lexer;

public static class LexemeTypeExtensions
{
    public static bool IsController(this LexemeType lexemeType) =>
        lexemeType is LexemeType.OneOrMoreChars or LexemeType.ZeroOrOneChars or LexemeType.ZeroOrMoreChars;
}