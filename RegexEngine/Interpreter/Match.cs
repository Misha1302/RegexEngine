namespace RegexEngine.Interpreter;

public record Match(string Source, Range Range, bool IsValid)
{
    public override string ToString() =>
        IsValid ? $"{Source[Range]}" : $"Invalid in '{Source}'";
}