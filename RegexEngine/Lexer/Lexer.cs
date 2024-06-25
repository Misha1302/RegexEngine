namespace RegexEngine.Lexer;

using RegexEngine.FastLinqs;
using RegexEngine.Lexer.Lexemes;

public class Lexer(string source)
{
    private int _position;
    private readonly List<Lexeme> _lexemes = [];
    private readonly List<(string key, LexemeType value)> _lexemeTexts = LexemesHelper.LexemeTexts;

    private bool NotEnd => _position < source.Length;
    public IReadOnlyList<Lexeme> Lexemes => _lexemes;

    public void Lex()
    {
        while (NotEnd)
        {
            if (TryAddBasicLexeme())
                continue;

            if (TryAddComplexLexeme())
                continue;

            Thrower.InvalidOpEx();
        }
    }

    private bool TryAddComplexLexeme()
    {
        var startPos = _position;
        while (NotEnd && _lexemeTexts.FastAll(x => !source.FastStartsWith(x.key, _position)))
        {
            _lexemes.Add(new Lexeme(source[_position..(_position + 1)], LexemeType.Char));
            _position++;
        }

        return startPos != _position;
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
        return _lexemeTexts.FastFirstOrDefault(x => source.FastStartsWith(x.key, _position));
    }
}