namespace RegexEngine.Lexer;

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
        while (_position < source.Length)
        {
            if (!TryAddBasicLexeme())
                continue;

            var startPos = _position;
            while (NotEnd && _lexemeTexts.All(x => !Slice.StartsWith(x.key)))
                _position++;

            var endPos = _position;

            if (startPos == endPos)
                throw new InvalidOperationException();

            _lexemes.Add(new Lexeme(source[startPos..endPos], LexemeType.Text));

            if (!NotEnd)
                TryAddBasicLexeme();
        }
    }

    private bool TryAddBasicLexeme()
    {
        var lexeme = GetBasicLexeme();

        if (lexeme == default) return true;

        _position += lexeme.key.Length;
        _lexemes.Add(new Lexeme(lexeme.key, lexeme.value));
        return false;
    }

    private (string key, LexemeType value) GetBasicLexeme()
    {
        return _lexemeTexts.FirstOrDefault(x => Slice.StartsWith(x.key));
    }
}