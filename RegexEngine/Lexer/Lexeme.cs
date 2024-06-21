namespace RegexEngine.Lexer;

public record Lexeme(string Text, LexemeType LexemeType)
{
    public string ToFullString() => $"{Text} {LexemeType}";
    public override string ToString() => Text;
}