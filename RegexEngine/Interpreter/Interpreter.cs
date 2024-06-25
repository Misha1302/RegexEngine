namespace RegexEngine.Interpreter;

using RegexEngine.Lexer;
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
        if (astIndex >= ast.Count)
            return new Result(index, index <= -1);

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
            LexemeType.Char => Char(index, unit),
            LexemeType.EndOfInnerScope => InnerScope(index, unit, astIndex),
            _ => Thrower.ArgOutOfRangeEx()
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
        if (index <= -1)
            return new Result(index, true);

        var maxRepeatCount = source.Length + 1;
        for (var repeatCount = maxRepeatCount - 1; repeatCount >= 0; repeatCount--)
        {
            var startIndex = index;

            for (var i = 0; i < repeatCount; i++)
            {
                var r = TryInterpretChildren(index, unit, astIndex);
                if (r.Success)
                    index = r.Index;
                else goto doubleContinue;
            }

            var result = InterpretNext(astIndex + 1, index);
            if (result.Success) return new Result(result.Index, true, true);
        doubleContinue:
            index = startIndex;
        }

        return new Result(index, false);
    }

    private Result TryInterpretChildren(int index, Parser.Unit unit, int astIndex)
    {
        foreach (var child in unit.Children)
        {
            if (index <= -1) return new Result(-1, false);

            var tempResult = SingleInterpret(index, child, astIndex);
            if (!tempResult.Success)
                return tempResult;

            index = tempResult.Index;
        }

        return new Result(index, true);
    }

    private Result Char(int index, Parser.Unit unit) =>
        new(index - 1, index >= 0 && source[index] == unit.Lexeme.Text[0]);

    private Result AnyChar(int index) =>
        new(index - 1, index >= 0);

    public readonly ref struct Result(int index, bool success, bool endOfInterpretation = false)
    {
        public readonly int Index = index;
        public readonly bool Success = success;
        public readonly bool EndOfInterpretation = endOfInterpretation;
    }
}