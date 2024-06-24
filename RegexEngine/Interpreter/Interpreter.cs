namespace RegexEngine.Interpreter;

using RegexEngine.Lexer.Lexemes;
using RegexEngine.Parser;

public class Interpreter(string source, IReadOnlyList<Parser.Unit> ast)
{
    public Match Interpret()
    {
        var index = source.Length - 1;
        var result = InterpretNext(0, index);
        return result.Success
            ? Match.Valid(source, Math.Max(0, result.Index)..)
            : Match.Invalid(source);
    }

    private Result InterpretNext(int astIndex, int index)
    {
        if (index <= -1)
        {
            var all = true;
            for (var i = astIndex; i < ast.Count; i++)
                all = all && ast[i].Lexeme.LexemeType == LexemeType.ZeroOrMoreTimes;
            return new Result(index, all);
        }

        if (astIndex >= ast.Count)
        {
            Console.WriteLine(index);
            return new Result(index, index <= -1);
        }

        var result = SingleInterpret(index, ast[astIndex], astIndex);
        if (result.EndOfInterpretation) return result;
        if (!result.Success) return result;

        return InterpretNext(astIndex + 1, result.Index);
    }

    private Result SingleInterpret(int index, Parser.Unit unit, int astIndex)
    {
        return unit.Lexeme.LexemeType switch
        {
            LexemeType.AnyChar => AnyChar(index),
            LexemeType.ZeroOrMoreTimes => ZeroOrMoreTimes(index, unit, astIndex),
            LexemeType.Text => Text(index, unit),
            LexemeType.EndOfInnerScope => InnerScope(index, unit, astIndex),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private Result InnerScope(int index, Parser.Unit unit, int astIndex)
    {
        foreach (var child in unit.Children)
        {
            var tempResult = SingleInterpret(index, child, astIndex);
            if (!tempResult.Success) return tempResult;
            index = tempResult.Index;
        }

        return new Result(index, true);
    }

    private Result ZeroOrMoreTimes(int index, Parser.Unit unit, int astIndex)
    {
        for (var repeatCount = 0; repeatCount < 50; repeatCount++)
        {
            Console.WriteLine($"{repeatCount} {unit}");
            var startIndex = index;

            for (var i = 0; i < repeatCount; i++)
                if (TryInterpretChildren(index, unit, astIndex, out var zeroOrMoreTimes1))
                    index = zeroOrMoreTimes1.Index;
                else goto doubleContinue;

            var result = InterpretNext(astIndex + 1, index);
            if (result.Success) return new Result(result.Index, true, true);
        doubleContinue:
            index = startIndex;
        }

        return new Result(index, false);
    }

    private bool TryInterpretChildren(int index, Parser.Unit unit, int astIndex, out Result zeroOrMoreTimes1)
    {
        foreach (var child in unit.Children)
        {
            if (index <= -1)
            {
                zeroOrMoreTimes1 = new Result(-1, false);
                return false;
            }

            var tempResult = SingleInterpret(index, child, astIndex);
            if (!tempResult.Success)
            {
                zeroOrMoreTimes1 = tempResult;
                return false;
            }

            index = tempResult.Index;
        }

        zeroOrMoreTimes1 = new Result(index, true);
        return true;
    }

    private Result Text(int index, Parser.Unit unit) =>
        new(index - unit.Lexeme.Text.Length, source[..(index + 1)].EndsWith(unit.Lexeme.Text));

    private Result AnyChar(int index) =>
        new(index - 1, index >= 0);

    private sealed record Result(int Index, bool Success, bool EndOfInterpretation = false);
}