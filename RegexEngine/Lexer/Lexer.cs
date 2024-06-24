namespace RegexEngine.Lexer;

using RegexEngine.Lexer.Lexemes;

public class Lexer(string source)
{
    private int _position;
    private readonly List<Lexeme> _lexemes = [];
    private readonly List<(string key, LexemeType value)> _lexemeTexts = LexemesHelper.LexemeTexts;

    private string Slice => source[_position..];
    private bool NotEnd => _position < source.Length;
    public IReadOnlyList<Lexeme> Lexemes => _lexemes;

    public void Lex()
    {
        while (NotEnd)
        {
            if (TryAddBasicLexeme())
                continue;

            AddComplexLexeme();
        }
    }

    private void AddComplexLexeme()
    {
        var startPos = _position;
        while (NotEnd && _lexemeTexts.All(x => !Slice.StartsWith(x.key)))
        {
            _lexemes.Add(new Lexeme(source[_position..(_position + 1)], LexemeType.Text));
            _position++;
        }

        var endPos = _position;

        if (startPos == endPos)
            throw new InvalidOperationException();
    }

    private bool TryAddBasicLexeme()
    {
        var lexeme = GetBasicLexeme();

        if (lexeme == default) return false;

        _position += lexeme.key.Length;
        _lexemes.Add(new Lexeme(lexeme.key, lexeme.value));
        return true;
    }

    private (string key, LexemeType value) GetBasicLexeme()
    {
        return _lexemeTexts.FirstOrDefault(x => Slice.StartsWith(x.key));
    }
}