namespace RegexEngine.Lexer;

public enum LexemeType
{
    AnyChar,
    OneOrMoreChars,
    ZeroOrOneChars,
    ZeroOrMoreChars,
    AnyDigit,
    RangeRepeatingChars,
    LeftCurlyBrace,
    RightCurlyBrace,
    Text,
    AnyWordChar,
    StartOfInnerScope,
    EndOfInnerScope
}