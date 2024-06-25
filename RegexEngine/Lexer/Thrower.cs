namespace RegexEngine.Lexer;

using RegexEngine.Interpreter;

public static class Thrower
{
    public static void InvalidOpEx() => throw new InvalidOperationException();

    public static Interpreter.Result ArgOutOfRangeEx() => throw new ArgumentOutOfRangeException();
}