using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Token
{
    public string Type { get; private set; }
    public string Value { get; private set; }
    public int Position {get; private set;}
    public Token(string type, string value , int position)
    {
        Type = type;
        Value = value;
        Position = Position;
    }
    public override string ToString()
    {
        return $"Type: {Type}, Value: {Value}";
    }
}
public class Lexer
{
    private static readonly List<(string Type, string Pattern)> tokenDefinitions = new List<(string, string)>
    {
        ("KEYWORD", @"(?i)\b(card|type|name|faction|power|range|
        onactivation|effect|params|amount|number|action|for|
        while|selector|source|single|predicate|postaction)\b"),
        ("IDENTIFIER", @"\b[a-zA-Z_][a-zA-Z0-9_]*\b"),
        ("NUMBER", @"\b\d+\b"),
        ("OPERATOR", @"[+\-*/=<>!&|]=?|&&|\|\||==|!=|<=|>=]"),
        ("WHITESPACE", @"\s+"),
        ("STRING", "\".*?\""),
        ("UNKNOWN", @".")
    };

    public List<Token> Tokenize(string input)
    {
        var tokens = new List<Token>();
        int position = 0;

        while (position < input.Length)
        {
            bool matchFound = false;

            foreach (var (type, pattern) in tokenDefinitions)
            {
                var regex = new Regex(pattern);
                var match = regex.Match(input, position);

                if (match.Success && match.Index == position)
                {
                    if (type != "WHITESPACE") // Ignore whitespaces
                    {
                        tokens.Add(new Token(type, match.Value , position));
                    }
                    position += match.Length;
                    matchFound = true;
                    break;
                }
            }
            if (!matchFound)
            {
              throw new Exception($"Unexpected character at line {position}");
            }
        }
        return tokens;
    }
}

public class Program
{
    public static void Main()
    {
        string code = "card my new card is beautiful";

        Lexer lexer = new Lexer();
        List<Token> tokens = lexer.Tokenize(code);

        foreach (var token in tokens)
        {
            Console.WriteLine(token);
            Console.WriteLine(token.Position);
        }
    }
}