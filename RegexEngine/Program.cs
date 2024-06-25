using System.Diagnostics;
using RegexEngine;

Measure();

void StandardRun()
{
    Console.WriteLine(Main());
}

void Measure()
{
    // precompiling
    Console.WriteLine(Main());


    var sw = Stopwatch.StartNew();
    const int count = 100000;
    for (var i = 0; i < count; i++)
        _ = Main();
    Console.WriteLine($"{sw.ElapsedMilliseconds} {(float)sw.ElapsedMilliseconds / count}");
}


bool Main()
{
    const string s = "caccccaccbabbcb";
    var len = new Regex().Match(s, @"(c*c)*b*a*.*c*.a*a*a*").Len;
    return len == s.Length;
}