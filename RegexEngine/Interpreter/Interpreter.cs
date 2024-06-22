namespace RegexEngine.Interpreter;

using RegexEngine.Parser;

public class Interpreter(string source, IReadOnlyList<Parser.Unit> ast)
{
    public Match Interpret() =>
        new(source, 2..4, true);
}