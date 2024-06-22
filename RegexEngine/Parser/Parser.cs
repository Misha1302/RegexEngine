namespace RegexEngine.Parser;

using RegexEngine.Lexer.Lexemes;

public class Parser(IReadOnlyList<Lexeme> lexerLexemes)
{
    private readonly List<Unit> _ast = [];
    private int _index;
    public IReadOnlyList<Unit> Ast => _ast;

    public void MakeAst()
    {
        for (_index = lexerLexemes.Count - 1; _index >= 0; _index--)
            _ast.Add(MakeUnit());

        DeepReverse(_ast);
    }

    private static void DeepReverse(List<Unit> roots)
    {
        roots.Reverse();
        foreach (var root in roots)
            DeepReverse(root.Children);
    }

    private Unit MakeUnit()
    {
        if (lexerLexemes[_index].LexemeType == LexemeType.EndOfInnerScope)
            return MakeScope();

        return MakeBasicUnit();
    }

    private Unit MakeBasicUnit()
    {
        var unit = new Unit(lexerLexemes[_index], []);
        AddChildrenIfNeed(unit);
        return unit;
    }

    private Unit MakeScope()
    {
        var unit = new Unit(new Lexeme("/", LexemeType.EndOfInnerScope), []);

        _index--;
        while (lexerLexemes[_index].LexemeType != LexemeType.StartOfInnerScope)
        {
            unit.Children.Add(MakeUnit());
            _index--;
        }

        return unit;
    }

    private void AddChildrenIfNeed(Unit unit)
    {
        if (!unit.Lexeme.LexemeType.IsController()) return;

        _index--;
        unit.Children.Add(MakeUnit());
    }


    public record Unit(Lexeme Lexeme, List<Unit> Children)
    {
        public override string ToString() =>
            Children.Count != 0
                ? $"({string.Join(", ", Children)})"
                : Lexeme.ToString();
    }
}