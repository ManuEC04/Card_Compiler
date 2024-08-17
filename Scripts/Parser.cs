//Here we verify that the sequence of tokens complies with the language syntax through a function for each element
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Security.AccessControl;
using System.Security.Cryptography;

namespace Compiler
{
    public class Parser
    {
        int Pos { get; set; }
        List <Token> tokens { get; set; }
        TokenValues Checker = new TokenValues();
        Context context = new Context();
        List <CompilingError> Errors;
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
            while (!isAtEnd())
            {
                if (match(Checker.Card))
                {
                    Card? Card = ParseCard();
                    if (Card.CheckSemantic())
                    {
                        tree.Nodes.Add(Card);
                        Card.Evaluate();
                        Console.WriteLine("Power" + " " + Card.Power.Value);
                        Console.WriteLine("Type" + " " + Card.Type.Value);
                        Console.WriteLine("Faction" + " " + Card.Faction.Value);
                        Console.WriteLine("Name" + " " + Card.Name.Value);
                        Console.Write("Range" + " ");
                        foreach (Expression exp in Card.Range)
                        {
                            Console.Write(exp.Value + " " + "," + " ");
                        }
                        Console.WriteLine(" ");
                        Console.WriteLine(" ");
                    }
                }
                else
                {
                    ParseVar();
                }
            }
            return tree;
        }
        public Card ParseCard()
        {
            Card card = new Card();
            if (!match(Checker.OpenCurlyBraces))
            {
                Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "{ expected"));
            }
            while (lookahead(Checker.Type) || lookahead(Checker.Name) || lookahead(Checker.Faction)
            || lookahead(Checker.Power) || lookahead(Checker.Range))
            {
                ParseCardProperties(card);

            }
            if (!match(Checker.ClosedCurlyBraces))
            {
                Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "} expected"));
            }
            return card;
        }
        void ParseCardProperties(Card card)
        {
            if (match(Checker.Type) && card.Type == null)
            {
                card.Type = ParseTextProperties();
            }
            else if (match(Checker.Name) && card.Name == null)
            {
                card.Name = ParseTextProperties();
            }
            else if (match(Checker.Faction) && card.Faction == null)
            {
                card.Faction = ParseTextProperties();
            }
            else if (match(Checker.Power) && card.Power == null)
            {
                Expression power = ParsePower();
                power.Evaluate();
                card.Power = power;
            }
            else if (match(Checker.Range) && card.Range == null)
            {
                card.Range = ParseRange();
            }
            else
            {
                Errors.Add(new CompilingError (Pos , ErrorCode.Invalid , "Invalid definition of Card"));
            }
            return;
        }
        Expression ParseTextProperties()  //Auxiliar Method for parsing name , type and faction of the card
        {
            if (!match(Checker.Points))
            {
                Errors.Add(new CompilingError(Pos, ErrorCode.Expected, ": expected"));
            }
            Expression expr = ParseText();
            expr.Evaluate();
            checkcomma();
            return expr;
        }
        Expression ParsePower() // Auxiliar Method for parsig Power
        {
            if (!match(Checker.Points))
            {
                Errors.Add(new CompilingError(Pos, ErrorCode.Expected, ": expected"));
            }
            Expression expr = ParseTerm();
            checkcomma();
            return expr;
        }
        List<Expression> ParseRange() // Auxiliar Method for parsig Range
        {
            if (!match(Checker.Points))
            {
                Errors.Add(new CompilingError(Pos, ErrorCode.Expected, ": expected"));
            }
            if (!match(Checker.OpenSquareBracket))
            {
                Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "[ expected"));
            }
            List<Expression> range = new List<Expression>();
            range.Add(ParseText());
            while (match(Checker.Comma)) // With this loop every time that it find a comma it will call Parse Text function and after execute that the parser will be again on a comma token or at the end of the declaration
            {
                range.Add(ParseText());
            }
            if (!match(Checker.ClosedSquareBracket))
            {
                Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "] expected"));
            }
            checkcomma();
            return range;
        }
        public void ParseVar()
        {

            if (tokens[Pos].Type == TokenType.Identifier)
            {
                context.Declaration.Add(tokens[Pos].Value, ParseDeclaration());
            }
            checkend();

            Console.WriteLine(context.Declaration.Count);
        }
        Declaration ParseDeclaration()
        {
            Declaration declaration = new Declaration();
            if (!matchtype(TokenType.Identifier))
            {
                return declaration;
            }
            string name = previous().Value;
            if (!match(Checker.Equal))
            {
                return declaration;
            }
            if (lookahead(Checker.QuotationMark))
            {
                Expression text = ParseConcatenation();
                text.Evaluate();
                declaration = new Declaration(name, text);
            }
            else if (looktype(TokenType.Number))
            {
                Expression number = ParseTerm();
                number.Evaluate();
                declaration = new Declaration(name, number);
            }
            return declaration;
        }

        Expression ParseConcatenation()
        {
            Expression expr = ParseText();
            if (match(Checker.Concatenation) || match(Checker.Spaced_Concatenation))
            {
                Token op = previous();
                Expression right = ParseConcatenation();
                expr = new Concatenation(expr, right, op.Value);
            }
            return expr;
        }
        Expression ParseText()
        {
            Expression expr = new Text("");
            if (!match(Checker.QuotationMark))
            {
                Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "\" expected"));
            }
            while (matchtype(TokenType.Text))
            {
                Token expression = previous();
                expr = new Text(expression.Value);
            }
            if (!match(Checker.QuotationMark))
            {
                Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "\" expected"));
            }
            return expr;
        }
        Expression ParseLogic()
        {
            Expression expr = ParseComparation();
            if (match(Checker.And) || match(Checker.Or))
            {
                Token op = previous();
                Expression right = ParseLogic();
                expr = new Logic(expr, right, op.Value);
            }
            return expr;
        }
        Expression ParseComparation()
        {
            Expression expr = ParseTerm();

            if (match(Checker.Less) || match(Checker.Less_Equal) || match(Checker.Greater)
            || match(Checker.Greater_Equal) || match(Checker.Equal_Equal))
            {
                Token op = previous();
                Expression right = ParseTerm();
                expr = new Comparation(expr, right, op.Value);
            }
            return expr;
        }
        Expression ParseTerm()
        {
            Expression expr = ParseFactor();

            while (match(Checker.Plus))
            {
                Token op = previous();
                Expression right = ParseFactor();
                expr = new Plus(expr, right, op.Value);
            }
            while (match(Checker.Minus))
            {
                Token op = previous();
                Expression right = ParseFactor();
                expr = new Minus(expr, right, op.Value);
            }
            return expr;
        }
        Expression ParseFactor()
        {
            Expression expr = ParseElevate();
            while (match(Checker.Mult))
            {
                Token op = previous();
                Expression right = ParseElevate();
                expr = new Mul(expr, right, op.Value);
            }
            while (match(Checker.Div))
            {
                Token op = previous();
                Expression right = ParseElevate();
                expr = new Div(expr, right, op.Value);
            }
            return expr;
        }
        Expression ParseElevate()
        {
            Expression expr = ParseUnary();
            while (match(Checker.Elevate))
            {
                Token op = previous();
                Expression right = ParseUnary();
                expr = new Elevate(expr, right, op.Value);
            }
            return expr;
        }
        Expression ParseUnary()
        {
            if (match(Checker.Minus))
            {
                Token op = previous();
                Expression right = ParseUnary();
                return new UnaryExpression(op.Value, right, ExpressionType.Unary);
            }
            return ParseAtom();
        }
        Expression ParseAtom()
        {
            Expression expr = new Number(00);
            if (matchtype(TokenType.Number))
            {
                Token tok = previous(); ;
                return new Number(Double.Parse(tok.Value));
            }
            if (match(Checker.OpenParenthesis))
            {
                Token op = previous();
                expr = ParseTerm();
                expr = new Parenthesis(op.Value, ExpressionType.Merge, expr);
                if (!match(Checker.ClosedParenthesis)) { Errors.Add(new CompilingError(Pos, ErrorCode.Expected, ") expected")); }
                return expr;
            }
            return expr;
        }

        //Auxiliar Methods
        void checkend()
        {
            if (!match(Checker.StatementSeparator))
            {
                Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "; expected"));
            }
        }
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
        bool lookahead(string value)
        {
            if (checkvalue(value))
            {
                return true;
            }
            return false;
        }
        bool looktype(TokenType type)
        {
            if (checktype(type))
            {
                return true;
            }
            return false;
        }
        void checkcomma()
        {
            if (!match(Checker.Comma))
            {
                Errors.Add(new CompilingError(Pos, ErrorCode.Expected, ", expected"));
            }
            return;
        }


        //GARBAGE METHODS
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
    }
}

