using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Compiler
{
 
public class Lexer
{
    private static readonly List<(TokenType Type, string Pattern)> tokenDefinitions = new List<(TokenType, string)>
    {
        (TokenType.Keyword, @"(?i)\b(card|type|name|faction|power|range)\b"),
        (TokenType.Number, @"\b\d+(\.\d+)?\b"),
        (TokenType.Plus, @"[\[\]+]"),
        (TokenType.Minus, @"[\[\]-]"),
        (TokenType.Multiplication, @"[\[\]*]"),
        (TokenType.Division, @"[\[\]/]"),
        (TokenType.Comparison_Op, @"==|>=|<=|>|<|="),
        (TokenType.Logic_Op, @"&&|\|\|"),
        (TokenType.Symbol, @"[\[\]:{}""']"),
        (TokenType.StatementSeparator, @"[\[\];]"),
        (TokenType.Comma, @"[\[\],]"),
        (TokenType.Whitespace, @"\s+"),
        (TokenType.Identifier, @"(?<="")([^""]*?)(?="")"),
        (TokenType.Unknown, @".")
        
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
                    if (type != TokenType.Whitespace) // Ignore whitespaces
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
        tokens.Add(new Token(TokenType.EOF , "END", tokens.Count - 1));
        return tokens;
    }
}
}