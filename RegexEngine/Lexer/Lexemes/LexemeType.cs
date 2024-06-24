﻿namespace RegexEngine.Lexer.Lexemes;

public enum LexemeType
{
    AnyChar,
    OneOrMoreTimes,
    ZeroOrOneTimes,
    ZeroOrMoreTimes,
    AnyDigit,
    LeftCurlyBrace,
    RightCurlyBrace,
    Text,
    AnyWordChar,
    StartOfInnerScope,
    EndOfInnerScope
}