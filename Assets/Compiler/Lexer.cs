using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Compiler
{
 
public class Lexer
{
    private static readonly List<(TokenType Type, string Pattern)> tokenDefinitions = new List<(TokenType, string)>
    {
        (TokenType.Whitespace, @"\s+"),
        (TokenType.Comma, @"[\,]"),
        (TokenType.Symbol, @"[\[\]:{}""'().]"),
        (TokenType.Text, "(?<=\")(.*?)(?=\")"),
        (TokenType.Keyword, @"(?i)\b(card|type|name|faction|power|range|params|action
        |targets|selector|source|single|predicate|PostAction|context|for|while|effect
        |Effect|Hand|Deck|Field|Graveyard|HandOfPlayer|DeckOfPlayer|FieldOfPlayer|GraveyardOfPlayer|
        Add|Suffle|Push|Pop|Remove|SendBottom|Find)\b"),
        (TokenType.Boolean, @"\b(true|false)\b"),
        (TokenType.Number, @"\b\d+(\.\d+)?\b"),
        (TokenType.DoublePlus, @"\+\+"),
        (TokenType.DoubleMinus, @"\-\-"),  
        (TokenType.EqualMinus , @"-="),
        (TokenType.EqualPlus , @"\+="),
        (TokenType.Plus, @"[\[\]+]"),
        (TokenType.Minus, @"[\[\]-]"),
        (TokenType.Multiplication, @"[\[\]*]"),
        (TokenType.Division, @"[\[\]/]"),
        (TokenType.Comparison_Op, @"==|>=|<=|>|<|="),
        (TokenType.Concatenation_Op , @"@@|@"),
        (TokenType.Logic_Op, @"&&|\|\|"),
        (TokenType.StatementSeparator, @"[\[\];]"),
        (TokenType.Identifier, @"\b[a-zA-Z]+\b")
        
    };

    public List<Token> Tokenize(string input , List<CompilingError> Errors)
    {
        var tokens = new List<Token>();
            int linenumber = 0;
            foreach(string line in File.ReadLines(input))
            {
                int position = 0;
                while(position < line.Length)
                {
            bool matchFound = false;

            foreach (var (type, pattern) in tokenDefinitions)
            {
                var regex = new Regex(pattern);
                var match = regex.Match(line, position);

                if (match.Success && match.Index == position)
                {
                    if (type != TokenType.Whitespace) // Ignore whitespaces
                    {
                        tokens.Add(new Token(type, match.Value , linenumber));
                        Console.WriteLine("TYPE"+ ":"+ " "+ type + " " + "VALUE"+ ":" + " "+ match.Value + " "+"POSITION" + ":" + " " + linenumber);
                    }
                    position += match.Length;
                    matchFound = true;
                    break;
                    
                }
                
            }
            if (!matchFound)
            {
              Errors.Add(new CompilingError(linenumber , ErrorCode.Unknown , "Unknow character"));
            }
            }
            linenumber++;
            
        }
        
        tokens.Add(new Token(TokenType.EOF , "END", tokens.Count - 1));
        return tokens;
    }
}
}