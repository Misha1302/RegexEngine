using RegexEngine;

const string source = "caccccaccbabbcb";
var len = new Regex().DebugMatch(source, @"(c*c)*b*a*.*c*.a*a*a*").Len;
Console.WriteLine(len == source.Length);