using RegexEngine.Lexer;
using RegexEngine.Parser;

var lexer = new Lexer(@"qq(x(y(z(xyz\w\d)q)))f");
lexer.Lex();
Console.WriteLine(string.Join("\n", lexer.Lexemes.Select(x => x.ToFullString())));

Console.WriteLine(new string('-', Console.WindowWidth));

var parser = new Parser(lexer.Lexemes);
parser.MakeAst();
Console.WriteLine(string.Join("; ", parser.Ast));