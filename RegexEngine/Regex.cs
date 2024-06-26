﻿namespace RegexEngine;

using RegexEngine.Interpreter;

public class Regex
{
    public Match DebugMatch(string source, string pattern)
    {
        ConsoleSeparate();
        var lexer = Lexer(pattern);
        Console.WriteLine(string.Join("\n", lexer.Lexemes.Select(x => x.ToFullString())));

        ConsoleSeparate();
        var parser = Parser(lexer);
        Console.WriteLine(string.Join("; ", parser.Ast));

        ConsoleSeparate();
        var match = Interpreter(source, parser);
        Console.WriteLine(match);

        return match;
    }

    public Match Match(string source, string pattern)
    {
        var lexer = Lexer(pattern);
        var parser = Parser(lexer);
        var match = Interpreter(source, parser);
        return match;
    }

    private Lexer.Lexer Lexer(string source)
    {
        var lexer = new Lexer.Lexer(source);
        lexer.Lex();
        return lexer;
    }

    private Parser.Parser Parser(Lexer.Lexer lexer2)
    {
        var parser = new Parser.Parser(lexer2.Lexemes);
        parser.MakeReversedAst();
        return parser;
    }

    private Match Interpreter(string source, Parser.Parser parser)
    {
        var interpreter = new Interpreter.Interpreter(source, parser.Ast);
        var match = interpreter.Interpret();
        return match;
    }

    private static void ConsoleSeparate()
    {
        Console.WriteLine(new string('-', Console.WindowWidth));
    }
}