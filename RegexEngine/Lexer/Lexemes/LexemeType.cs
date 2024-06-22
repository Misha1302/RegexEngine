namespace RegexEngine.Lexer.Lexemes;

public enum LexemeType
{
    AnyChar,
    OneOrMoreChars,
    ZeroOrOneChars,
    ZeroOrMoreChars,
    AnyDigit,
    LeftCurlyBrace,
    RightCurlyBrace,
    Text,
    AnyWordChar,
    StartOfInnerScope,
    EndOfInnerScope
}