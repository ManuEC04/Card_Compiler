using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Compiler
{
 
public class Lexer
{
    private static readonly List<(string Type, string Pattern)> tokenDefinitions = new List<(string, string)>
    {
        ("KEYWORD", @"(?i)\b(card|type|name|faction|power|range|
        onactivation|effect|params|amount|number|action|for|
        while|selector|source|single|predicate|postaction)\b"),
        ("NUMBER", @"\b\d+\b"),
        ("OPERATOR", @"[+\-*/=<>!&|]=?|&&|\|\||==|!=|<=|>=]"),
        ("SYMBOL", @"[\[\]:{},]"),
        ("WHITESPACE", @"\s+"),
        ("IDENTIFIER", @"(?<="")([^""]*?)(?="")"),
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
                        Console.WriteLine("TYPE"+ ":"+ " "+ type + " " + "VALUE"+ ":" + " "+ match.Value);
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
}