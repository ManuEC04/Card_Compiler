//Here we verify that the sequence of tokens complies with the language syntax through a function for each element
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;

namespace Compiler
{
    public class Parser
    {
        int Pos { get; set; }
        List<Token> tokens { get; set; }
        TokenValues Checker = new TokenValues();
        List<CompilingError> Errors;
        public Parser(List<Token> tokens)
        {
            Pos = 0;
            this.tokens = tokens;
            Errors = new List<CompilingError>();
        }
        public AST ParseProgram()
        {
            AST tree = new AST();
            foreach (CompilingError error in Errors)
            {
                Console.WriteLine(error.Argument);
            }
            return tree;
        }
        /* public CardAssignation ParseCard()
         {
             if (!Match(tokens[Pos], Checker.Card))
             {
                 return null;
             }
             CardAssignation Card = new CardAssignation();
             Next();
             if (!Match(tokens[Pos], Checker.OpenCurlyBraces))
             {
                 Exceptions.OpenCurlyException(Pos);
             }
             CloseSymbol(Pos);
             Next();
             CheckCardAssignations(Card);
             Console.WriteLine(Card.Faction_Assignation.Value);
             Console.WriteLine(Card.Name_Assignation.Value);
             Console.WriteLine(Card.Type_Assignation.Value);
             Console.WriteLine(Card.Range_Assignation.Value);
             Console.WriteLine(Card.Power_Assignation.Value);

             return Card;

         }
         void CheckCardAssignations(CardAssignation Card)
         {
             if (Match(tokens[Pos], Checker.Type))
             {
                 Next();
                 CheckTypeAssignation(Card);
             }
             if (Match(tokens[Pos], Checker.Name))
             {
                 Next();
                 CheckNameAssignation(Card);
             }
             if (Match(tokens[Pos], Checker.Faction))
             {
                 Next();
                 CheckFactionAssignation(Card);
             }
             if (Match(tokens[Pos], Checker.Range))
             {
                 Next();
                 CheckRangeAssignation(Card);
             }
             if (Match(tokens[Pos], Checker.Power))
             {
                 Next();
                 CheckPowerAssignation(Card);
             }
         }
         void CheckTypeAssignation(CardAssignation Card)
         {
             if (!Match(tokens[Pos], Checker.Points))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, ": expected"));
                 Exceptions.PointsException(Pos);
             }
             Next();
             if (!Match(tokens[Pos], Checker.QuotationMark))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "\" expected"));
                 Exceptions.QuotationMarkException(Pos);
             }
             Next();
             if (!MatchToken(tokens[Pos], "IDENTIFIER"))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "identifier expected"));
                 Exceptions.IdentifierException(Pos);
             }
             string temp = tokens[Pos].Value;
             Next();
             if (!Match(tokens[Pos], Checker.QuotationMark))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "\" expected"));
                 Exceptions.QuotationMarkException(Pos);
             }
             Next();
             if (!Match(tokens[Pos], Checker.Comma))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, " , expected"));
                 Exceptions.CommaException(Pos);
             }
             Card.Type_Assignation = new Expression(temp, ExpressionType.Text);
             Next();
         }
         void CheckNameAssignation(CardAssignation Card)
         {
             if (!Match(tokens[Pos], Checker.Points))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, " : expected"));
                 Exceptions.PointsException(Pos);
             }
             Next();
             if (!Match(tokens[Pos], Checker.QuotationMark))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "\" expected"));
                 Exceptions.QuotationMarkException(Pos);
             }
             Next();
             if (!MatchToken(tokens[Pos], "IDENTIFIER"))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, " identifier expected"));
                 Exceptions.IdentifierException(Pos);
             }
             string temp = tokens[Pos].Value;
             Next();

             if (!Match(tokens[Pos], Checker.QuotationMark))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "\" expected"));
                 Exceptions.QuotationMarkException(Pos);
             }
             Next();
             if (!Match(tokens[Pos], Checker.Comma))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, " , expected"));
                 Exceptions.CommaException(Pos);
             }
             Card.Name_Assignation = new Expression(temp, ExpressionType.Text);
             Next();
         }
         void CheckFactionAssignation(CardAssignation Card)
         {
             if (!Match(tokens[Pos], Checker.Points))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, " : expected"));
                 Exceptions.PointsException(Pos);
             }
             Next();
             if (!Match(tokens[Pos], Checker.QuotationMark))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "\" expected"));
                 Exceptions.QuotationMarkException(Pos);
             }
             Next();
             if (!MatchToken(tokens[Pos], "IDENTIFIER"))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, " identifier expected"));
                 Exceptions.IdentifierException(Pos);
             }
             string temp = tokens[Pos].Value;
             Next();
             if (MatchToken(tokens[Pos], "IDENTIFIER"))
             {
                 temp += tokens[Pos].Value;
                 Next();
             }
             if (!Match(tokens[Pos], Checker.QuotationMark))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "\" expected"));
                 Exceptions.QuotationMarkException(Pos);
             }
             Next();
             if (!Match(tokens[Pos], Checker.Comma))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, " , expected"));
                 Exceptions.CommaException(Pos);
             }
             Card.Faction_Assignation = new Expression(temp, ExpressionType.Text);
             Next();
         }
         void CheckRangeAssignation(CardAssignation Card)
         {
             if (!Match(tokens[Pos], Checker.Points))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, " : expected"));
                 Exceptions.PointsException(Pos);
             }
             Next();
             if (!Match(tokens[Pos], Checker.OpenSquareBracket))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, " [ expected"));
                 Exceptions.OpenSquareException(Pos);
             }
             CloseSymbol(Pos);
             Next();
             if (!Match(tokens[Pos], Checker.QuotationMark))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "\" expected"));
                 Exceptions.QuotationMarkException(Pos);
             }
             Next();
             if (!MatchToken(tokens[Pos], "IDENTIFIER"))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, " identifier expected"));
                 Exceptions.IdentifierException(Pos);
             }
             string temp = tokens[Pos].Value;
             Next();
             if (!Match(tokens[Pos], Checker.QuotationMark))
             {
                 Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "\" expected"));
                 Exceptions.QuotationMarkException(Pos);
             }
             Next();
             if (Match(tokens[Pos], Checker.Comma))
             {
                 Next();
                 if (!Match(tokens[Pos], Checker.QuotationMark))
                 {
                     Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "\" expected"));
                     Exceptions.QuotationMarkException(Pos);
                 }
                 Next();
                 if (!MatchToken(tokens[Pos], "IDENTIFIER"))
                 {
                     Errors.Add(new CompilingError(Pos, ErrorCode.Expected, " identifier expected"));
                     Exceptions.IdentifierException(Pos);
                 }
                 temp += tokens[Pos].Value;
                 Next();
                 if (!Match(tokens[Pos], Checker.QuotationMark))
                 {
                     Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "\" expected"));
                     Exceptions.QuotationMarkException(Pos);
                 }
                 Next();
                 if (!Match(tokens[Pos], Checker.ClosedSquareBracket))
                 {
                     Errors.Add(new CompilingError(Pos, ErrorCode.Expected, " ] expected"));
                     Exceptions.ClosedSquareException(Pos);
                 }
                 Next();
                 if (!Match(tokens[Pos], Checker.Comma))
                 {
                     Errors.Add(new CompilingError(Pos, ErrorCode.Expected, " , expected"));
                     Exceptions.CommaException(Pos);
                 }
                 Card.Range_Assignation = new Expression(temp, ExpressionType.Text);
                 Next();
             }
             else if (Match(tokens[Pos], Checker.ClosedSquareBracket))
             {
                 Next();
                 if (!Match(tokens[Pos], Checker.Comma))
                 {
                     Errors.Add(new CompilingError(Pos, ErrorCode.Expected, " , expected"));
                     Exceptions.CommaException(Pos);
                 }
                 Card.Range_Assignation = new Expression(temp, ExpressionType.Text);
                 Next();
             }
         }
         void CheckPowerAssignation(CardAssignation Card)
         {
             if (!Match(tokens[Pos], Checker.Points))
             {
                 Exceptions.PointsException(Pos);
             }
             Next();
             if (!MatchToken(tokens[Pos], "NUMBER"))
             {
                 Exceptions.IdentifierException(Pos);
             }
             string temp = tokens[Pos].Value;
             Next();
             if (!Match(tokens[Pos], Checker.Comma))
             {
                 Exceptions.CommaException(Pos);
             }
             Card.Power_Assignation = new Expression(temp, ExpressionType.Number);
             Next();
         }
         */
        public void ParseOperations()
        {
            Expression expr = Term();
            expr.Evaluate();
            Console.WriteLine(expr.Value);
        }
        Expression Term()
        {
            Expression expr = Factor();

            while (match(Checker.Plus))
            {
                Console.WriteLine("Encuentra la suma");
                Token op = previous();
                Expression right = Factor();
                expr = new Plus(expr, right, op, ExpressionType.Binary);
            }
            while (match(Checker.Minus))
            {
                Console.WriteLine("Encuentra la resta");
                Token op = previous();
                Expression right = Factor();
                expr = new Minus(expr, right, op, ExpressionType.Binary);
            }
            return expr;
        }
        Expression Factor()
        {
            Expression expr = Unary();
            while (match(Checker.Mult))
            {
                Console.WriteLine("Encuentra el producto");
                Token op = previous();
                Expression right = Unary();
                expr = new Mul(expr, right, op, ExpressionType.Binary);
            }
            while (match(Checker.Div))
            {
                Console.WriteLine("Encuentra la division");
                Token op = previous();
                Expression right = Unary();
                expr = new Div(expr, right, op, ExpressionType.Binary);
            }
            return expr;
        }
        Expression Unary()
        {
            if (match(Checker.Minus))
            {
                Token op = previous();
                Expression right = Unary();
                return new UnaryExpression( op.Value , right ,ExpressionType.Unary);
            }
            return Atom();
        }
        Expression Atom()
        {
            Expression expr = new NumberNode(0);
            if (matchtype(TokenType.Number))
            {
                Console.WriteLine ("Encuentra el numero");
                Token tok = previous();;
                return new NumberNode(Double.Parse(previous().Value));
            }
            if(match(Checker.OpenParenthesis))
            {
                Console.WriteLine("Encuentra el parentesis");
                expr = Term();
                if(!match(Checker.ClosedParenthesis)){Errors.Add(new CompilingError(Pos, ErrorCode.Expected , ") expected"));}
                return expr;
            }
            return expr;
        }
        void Next()
        {
            Pos++;
        }
        void Back()
        {
            Pos--;
        }
        void MoveBack(int i)
        {
            Pos = -i;
        }
        void MoveNext(int i)
        {
            Pos += i;
        }
        void Reset()
        {
            Pos = 0;
        }
        bool Match(Token token, string value)
        {
            if (token.Value == value)
            {
                return true;
            }
            return false;
        }
        void CloseSymbol(int position)

        

        {
            string symbol;
            if (tokens[position].Value == Checker.OpenCurlyBraces)
            {
                symbol = Checker.ClosedCurlyBraces;
            }
            else if (tokens[position].Value == Checker.OpenSquareBracket)
            {
                symbol = Checker.ClosedSquareBracket;
            }
            else if (tokens[position].Value == Checker.QuotationMark)
            {
                symbol = Checker.QuotationMark;
            }
            else
            {
                throw new Exception("El string no es un simbolo");
            }
            for (int i = position; i < tokens.Count; i++)
            {
                if (tokens[i].Value == symbol)
                {
                    return;
                }
            }
            throw new Exception("Se esperaba el caracter" + " " + symbol + " " + "debido a" + " " + tokens[Pos].Value + " " + "que se encuentra en la posicion" + " " + tokens[Pos].Position);
        }
        //Auxiliar Methods
        bool checkvalue(string value)
        {
            if (isAtEnd()) { return false; }
            return peek().Value == value;
        }
        bool checktype(TokenType type)
        {
            if (isAtEnd()) { return false; }
            return peek().Type == type;
        }
        private Token advance()
        {
            if (!isAtEnd())
            {
                Pos++;
            }
            return previous();

        }
        private bool isAtEnd()
        {
            return peek().Value == "END";
        }
        private Token peek()
        {
            return tokens[Pos];
        }
        Token previous()
        {
            return tokens[Pos - 1];
        }
        bool match(string value)
        {
            if (checkvalue(value))
            {
                advance();
                return true;
            }
            return false;
        }
        bool matchtype(TokenType type)
        {
            if (checktype(type))
            {
                advance();
                return true;

            }
            return false;
        }
    }

}

