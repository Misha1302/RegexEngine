using System.Diagnostics;
using RegexEngine;

for (var i = 0; i < 5; i++)
    Console.WriteLine(Main());


var sw = Stopwatch.StartNew();
for (var i = 0; i < 100000; i++)
    _ = Main();
Console.WriteLine(sw.ElapsedMilliseconds);

bool Main()
{
    const string s = "caccccaccbabbcb";
    var len1 = new Regex().Match(s, @"(c*c)*b*a*.*c*.a*a*a*").Len;
    return len1 == s.Length;
}