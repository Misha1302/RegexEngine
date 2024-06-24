namespace RegexEngine.Interpreter;

public record Match(string Source, Range Range, bool IsValid)
{
    public int Len => IsValid ? ToString().Length : -1;

    public static Match Invalid(string source) => new(source, default, false);
    public static Match Valid(string source, Range range) => new(source, range, true);

    public override string ToString() =>
        IsValid ? $"{Source[Range]}" : $"Invalid in '{Source}'";
}