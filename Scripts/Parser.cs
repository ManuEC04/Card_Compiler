//Here we verify that the sequence of tokens complies with the language syntax through a function for each element
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ConstrainedExecution;
using System.Security.AccessControl;
using System.Security.Cryptography;

namespace Compiler
{
    public class Parser
    {
        int Pos { get; set; }
        public List<Token> tokens { get; private set; }
        TokenValues Checker = new TokenValues();
        Context context = new Context();
        public List<CompilingError> Errors { get; private set; }

        //CONSTRUCTOR
        public Parser(List<Token> tokens, List<CompilingError> Errors)
        {
            Pos = 0;
            this.tokens = tokens;
            this.Errors = Errors;
        }
        //MAIN METHOD
        public AST ParseProgram()
        {
            AST tree = new AST();
            while (!isAtEnd())
            {
                if (match(Checker.Card))
                {
                    Card? Card = ParseCard();
                    Card.Evaluate();
                    Scope scope = new Scope();
                    if (Card.CheckSemantic(context, Errors, scope))
                    {
                        tree.Nodes.Add(Card);
                        Console.WriteLine("Power:" + " " + Card.Power.Value);
                        Console.WriteLine("Type:" + " " + Card.Type.Value);
                        Console.WriteLine("Faction:" + " " + Card.Faction.Value);
                        Console.WriteLine("Name:" + " " + Card.Name.Value);
                        Console.WriteLine("Power:" + " " + Card.Power.Value);
                        Console.Write("Range:" + " ");
                        foreach (Expression exp in Card.Range)
                        {
                            Console.Write(exp.Value + " " );
                        }
                        Console.WriteLine(" ");

                    }
                }
                else if (match(Checker.effect))
                {
                    Effect? Effect = ParseEffect();
                    tree.Nodes.Add(Effect);
                }
                else
                {
                    Errors.Add(new CompilingError(peek().Position, ErrorCode.Invalid, "Invalid character"));
                }

            }

            foreach (CompilingError error in Errors)
            {
                Console.WriteLine(error.Argument + " " + "at line" + " " + error.Position);

            }

            return tree;
        }

        //PARSING EFFECTS
        public Effect ParseEffect()
        {
            Effect effect = new Effect();
            if (!match(Checker.OpenCurlyBraces))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "{ expected"));
            }
            while (lookahead(Checker.Name) || lookahead(Checker.Params) || lookahead(Checker.Action))
            {
                if (match(Checker.Name))
                {
                    effect.Name = ParseTextProperties();
                }
                else if (match(Checker.Params))
                {
                    effect.Params = ParseParams();
                }
                else if (match(Checker.Action))
                {
                    effect.Action = ParseAction();
                }
            }
            if (!match(Checker.ClosedCurlyBraces))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "} expected"));
            }
            return effect;
        }
        public List<Expression> ParseParams()
        {
            List<Expression> Params = new List<Expression>();
            if (!match(Checker.Points))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ": expected"));
            }
            if (!match(Checker.OpenCurlyBraces))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "{ expected"));
            }
            while (looktype(TokenType.Identifier))
            {
                Identifier? identifier = ParseIdentifier();
                if (!match(Checker.Points))
                {
                    Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ": expected"));
                }
                identifier.Expression = ParseIdentifier();
                Params.Add(identifier);
                checkcomma();
            }
            if (!match(Checker.ClosedCurlyBraces))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "} expected"));
            }
            checkcomma();
            return Params;
        }
        public List<Expression> ParseAction()
        {
            List<Expression> Actions = new List<Expression>();
            if (!match(Checker.Points))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ": expected"));
            }
            if (!match(Checker.OpenParenthesis))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "( expected"));
            }
            if (!match(Checker.targets))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "targets expected"));
            }
            if (!match(Checker.Comma))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ", expected"));
            }
            if (!match(Checker.context))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "context expected"));
            }
            if (!match(Checker.ClosedParenthesis))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ") expected"));
            }
            if (!match(Checker.Equal))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "= expected"));
            }
            if (!match(Checker.Greater))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "> expected"));
            }
            if (!match(Checker.OpenCurlyBraces))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "{ expected"));
            }
            while (lookahead(Checker.For) || lookahead(Checker.While) || looktype(TokenType.Identifier))
            {
                if (match(Checker.For))
                {

                }
                else if (match(Checker.While))
                {
                    Actions.Add(ParseWhile());

                }
            }
            if (!match(Checker.ClosedCurlyBraces))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "} expected"));
            }

            return Actions;

        }
        //PARSING CARDS
        public Card ParseCard()
        {
            Card card = new Card();
            if (!match(Checker.OpenCurlyBraces))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "{ expected"));
            }
            while (lookahead(Checker.Type) || lookahead(Checker.Name) || lookahead(Checker.Faction)
            || lookahead(Checker.Power) || lookahead(Checker.Range) || lookahead(Checker.OnActivation))
            {
                ParseCardProperties(card);

            }
            if (!match(Checker.ClosedCurlyBraces))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "} expected"));
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
            else if (match(Checker.OnActivation))
            {
                ParseOnActivation(card.OnActivation);
            }
            else
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Invalid, "Invalid definition of Card"));
            }
            return;
        }
        Expression ParseTextProperties()  //Auxiliar Method for parsing name , type and faction of the card
        {
            if (!match(Checker.Points))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ": expected"));
            }
            Expression expr = ParseText();
            checkcomma();
            return expr;
        }
        Expression ParsePower() // Auxiliar Method for parsig Power
        {
            if (!match(Checker.Points))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ": expected"));
            }
            Expression expr = ParseTerm();
            expr.Evaluate();
            checkcomma();
            return expr;
        }
        List<Expression> ParseRange() // Auxiliar Method for parsig Range
        {
            if (!match(Checker.Points))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ": expected"));
            }
            if (!match(Checker.OpenSquareBracket))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "[ expected"));
            }
            List<Expression> range = new List<Expression>();
            range.Add(ParseText());
            while (match(Checker.Comma)) // With this loop every time that it find a comma it will call Parse Text function and after execute that the parser will be again on a comma token or at the end of the declaration
            {
                range.Add(ParseText());
            }
            if (!match(Checker.ClosedSquareBracket))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "] expected"));
            }
            checkcomma();
            return range;
        }


        //PARSING ONACTIVATION
        void ParseOnActivation(OnActivation onactivation)
        {
            if (!match(Checker.Points))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ": expected"));
            }
            if (!match(Checker.OpenSquareBracket))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "[ expected"));
            }
            while (match(Checker.OpenCurlyBraces))
            {
                DeclaredEffect effect = ParsingEffectExpressions();
                if (effect.Name.Type != ExpressionType.Null)
                {
                    onactivation.Effects.Add(effect);
                }
                if (!match(Checker.ClosedCurlyBraces))
                {
                    Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "} expected"));
                }
                checkcomma();
            }
            if (!match(Checker.ClosedSquareBracket))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "] expected"));
            }
        }
        DeclaredEffect ParsingEffectExpressions()
        {
            DeclaredEffect effect = new DeclaredEffect();
            while (lookahead(Checker.Effect) || lookahead(Checker.PostAction) || lookahead(Checker.Selector))
            {
                if (match(Checker.Effect))
                {
                    ParseNameParam(effect);
                    checkcomma();
                }
                else if (match(Checker.Selector))
                {
                    effect.Selector = ParseSelector();
                    checkcomma();
                }
                else if (match(Checker.PostAction))
                {
                    effect.PostAction = ParsePostAction();
                    checkcomma();
                }
                else { return null; }
            }
            return effect;
        }
        void ParseNameParam(DeclaredEffect effect)
        {
            if (!match(Checker.Points))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ": expected"));
            }
            if (!match(Checker.OpenCurlyBraces) && !looktype(TokenType.Symbol))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "{ expected"));
            }
            effect.Name = ParseText();
            if (effect.Name.Type != ExpressionType.Null)
            {
                return;
            }
            while (lookahead(Checker.Name) || lookahead(Checker.Amount))
            {
                if (match(Checker.Name))
                {
                    effect.Name = ParseTextProperties();
                    checkcomma();

                }
                else if (matchtype(TokenType.Identifier))
                {
                    if (!match(Checker.Points))
                    {
                        Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ": expected"));
                    }
                    effect.Params.Add(ParsingExpressions());
                    checkcomma();
                }
            }
            if (!match(Checker.ClosedCurlyBraces))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "} expected"));
            }
            checkcomma();
        }
        PostAction? ParsePostAction()
        {
            PostAction postAction = new PostAction();
            if (!match(Checker.Points))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ": expected"));
            }
            if (!match(Checker.OpenCurlyBraces))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "{ expected"));
            }
            while (lookahead(Checker.Type) || lookahead(Checker.Selector))
            {
                if (match(Checker.Type))
                {
                    postAction.Type = ParseTextProperties();
                    checkcomma();

                }
                else if (match(Checker.Selector))
                {
                    postAction.Selector = ParseSelector();
                    checkcomma();
                }
                else if (match(Checker.PostAction))
                {
                    postAction.Post = ParsePostAction();
                }
                else { return null; }
            }
            if (!match(Checker.ClosedCurlyBraces))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "} expected"));
            }
            return postAction;
        }
        Selector ParseSelector()
        {
            Selector selector = new Selector();
            if (!match(Checker.Points))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ": expected"));
            }
            if (!match(Checker.OpenCurlyBraces))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "{ expected"));
            }
            while (lookahead(Checker.Source) || lookahead(Checker.Single) || lookahead(Checker.Predicate))
            {
                if (match(Checker.Source))
                {
                    selector.Source = ParseTextProperties();
                    checkcomma();
                }
                else if (match(Checker.Predicate))
                {
                    selector.Predicate = ParsePredicate();
                }
                else if (match(Checker.Single))
                {
                    if (!match(Checker.Points))
                    {

                        Errors.Add(new CompilingError(Pos, ErrorCode.Expected, ": expected"));
                    }
                    selector.Single = ParsingExpressions();
                    checkcomma();
                }
            }
            if (!match(Checker.ClosedCurlyBraces))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "} expected"));
            }
            checkcomma();
            return selector;
        }
        Predicate ParsePredicate()
        {
            Property property = new Property("", peek().Position);

            if (!match(Checker.Points))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ": expected"));
            }
            if (!match(Checker.OpenParenthesis))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "( expected"));
            }
            if (!matchtype(TokenType.Identifier))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "An identifier was expected"));
            }
            if (!match(Checker.ClosedParenthesis))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ") expected"));
            }
            if (!match(Checker.Equal))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "= expected"));
            }
            if (!match(Checker.Greater))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "> expected"));
            }
            if (!matchtype(TokenType.Identifier))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "An identifier was expected"));
            }
            if (match(Checker.Point))
            {

                if (matchtype(TokenType.Keyword))
                {
                    property = new Property(previous().Value.ToString(), peek().Position);
                }

            }
            Back(); // We go back because the NodeComparation analyze first the left part of the expression
            Expression comparation = ParseComparation();
            return new Predicate(property, (Comparation)comparation);
        }


        //PARSING OTHER NODES
        While ParseWhile()
        {
            if (!match(Checker.OpenParenthesis))
            {
                Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "(  expected "));
            }

            Expression comparation = ParseLogic();

            if (!match(Checker.ClosedParenthesis))
            {
                Errors.Add(new CompilingError(Pos, ErrorCode.Expected, ")  expected "));
            }
            List<Expression> instructions = ParseInstructions();
            While whileloop = new While(comparation, instructions, peek().Position);

            return whileloop;
        }
        List<Expression> ParseInstructions()
        {
            List<Expression> Instructions = new List<Expression>();
            while (matchtype(TokenType.Identifier))
            {

                if (lookahead(Checker.Point))
                {
                    Back();
                    Instructions.Add(ParseProperty());
                }
            }
            return Instructions;
        }

        Expression ParseProperty()
        {
            Expression? argument = null;
            string Property = "";

            while (matchtype(TokenType.Identifier) || matchtype(TokenType.Keyword))
            {
                Property += previous().Value;
                if (match(Checker.Point))
                {
                    Property += ".";
                }
            }
            if (match(Checker.OpenParenthesis))
            {
                Property += "(";
            }
            if (matchtype(TokenType.Identifier))
            {
                if(lookahead(Checker.Point))
                {
                 argument = ParseProperty();
                }
                Back();
                argument = ParseIdentifier();
            }
            if (match(Checker.ClosedParenthesis))
            {
                Property += ")";
            }
            else
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ") expected"));
            }
            checkend();
            return new Property(Property,argument , peek().Position);
        }
        Identifier? ParseDeclaration()
        {
            if (!matchtype(TokenType.Identifier))
            {
                return null;
            }
            string name = previous().Value;
            if (!match(Checker.Equal))
            {
                return null;
            }
            if (lookahead(Checker.QuotationMark))
            {
                Expression text = ParseConcatenation();
                text.Evaluate();
                return new Identifier(name, text, ExpressionType.Identifier, peek().Position);
            }
            else if (looktype(TokenType.Number))
            {
                Expression number = ParseTerm();
                number.Evaluate();
                return new Identifier(name, number, ExpressionType.Identifier, peek().Position);
            }
            return null;
        }
        Expression ParseConcatenation()
        {
            Expression expr = ParseText();
            if (match(Checker.Concatenation) || match(Checker.Spaced_Concatenation))
            {
                Token op = previous();
                Expression right = ParseConcatenation();
                expr = new Concatenation(expr, right, op.Value, peek().Position);
            }
            return expr;
        }
        Expression ParseText()
        {
            Expression expr = new Null();
            if (!match(Checker.QuotationMark))
            {
                //Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "\" expected"));
            }
            while (matchtype(TokenType.Text))
            {
                Token expression = previous();
                expr = new Text(expression.Value, peek().Position);
            }
            if (!match(Checker.QuotationMark))
            {
                //Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "\" expected"));
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
                expr = new Logic(expr, right, op.Value, peek().Position);
            }
            return expr;
        }
        Identifier? ParseIdentifier()
        {
            if (matchtype(TokenType.Identifier))
            {
                Identifier declaration = new Identifier(previous().Value , previous().Position);
                  return declaration;
            }
            return null;
        }
        Expression ParsingExpressions()
        {
            if (peek().Type == TokenType.Keyword)
            {
                advance();
                return new Text(previous().Value, peek().Position);
            }
            else if (matchtype(TokenType.Identifier))
            {
                if (lookahead(Checker.DoublePlus) || lookahead(Checker.DoubleMinus))
                {
                    Back();
                    Expression expr = ParseUnary();
                    return expr;
                }
                return new Identifier(previous().Value);
            }
            else if (peek().Type == TokenType.Number)
            {
                Expression expr = ParseTerm();
                return expr;
            }
            else if (peek().Type == TokenType.Symbol)
            {

                Expression expr = ParseConcatenation();
                return expr;
            }
            else if (matchtype(TokenType.Boolean))
            {
                Token tok = previous();
                return new Boolean(tok.Value, peek().Position);
            }
            return null;

        }
        Expression ParseComparation()
        {

            Expression expr = ParsingExpressions();

            if (match(Checker.Less) || match(Checker.Less_Equal) || match(Checker.Greater)
            || match(Checker.Greater_Equal) || match(Checker.Equal_Equal))
            {
                Token op = previous();
                Expression right = ParsingExpressions();
                expr = new Comparation(expr, right, op.Value, peek().Position);
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
                expr = new Plus(expr, right, op.Value, peek().Position);
            }
            while (match(Checker.Minus))
            {
                Token op = previous();
                Expression right = ParseFactor();
                expr = new Minus(expr, right, op.Value, peek().Position);
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
                expr = new Mul(expr, right, op.Value, peek().Position);
            }
            while (match(Checker.Div))
            {
                Token op = previous();
                Expression right = ParseElevate();
                expr = new Div(expr, right, op.Value, peek().Position);
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
                expr = new Elevate(expr, right, op.Value, peek().Position);
            }
            return expr;
        }
        Expression ParseUnary()
        {
            if (match(Checker.Minus))
            {
                Token op = previous();
                Expression right = ParseUnary();
                return new UnaryExpression(op.Value, right, ExpressionType.Unary, peek().Position);
            }
            if (matchtype(TokenType.Identifier))
            {
                Identifier identifier = new Identifier(previous().Value , previous().Position);
                if (match(Checker.DoublePlus) || match(Checker.DoubleMinus))
                {
                    Token op = previous();
                    return new UnaryExpression(op.Value, identifier, ExpressionType.Unary, peek().Position);

                }
                return identifier;
            }
            return ParseAtom();
        }
        Expression ParseAtom()
        {
            Expression expr = new Number(00, peek().Position);
            if (matchtype(TokenType.Number))
            {
                Token tok = previous();
                return new Number(Double.Parse(tok.Value), peek().Position);
            }
            if (match(Checker.OpenParenthesis))
            {
                Token op = previous();
                expr = ParseTerm();
                expr = new Parenthesis(op.Value, ExpressionType.Merge, expr, peek().Position);
                if (!match(Checker.ClosedParenthesis)) { Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ") expected")); }
                return expr;
            }
            else if (matchtype(TokenType.Boolean))
            {
                Token tok = previous();
                return new Boolean(tok.Value, peek().Position);
            }

            return expr;
        }


        //AUXILIAR METHODS
        void checkend()
        {
            if (!match(Checker.StatementSeparator))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "; expected"));
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
        Token advance()
        {
            if (!isAtEnd())
            {
                Pos++;
            }
            return previous();

        }
        void Back()
        {
            Pos--;
        }
        bool isAtEnd()
        {
            return peek().Value == "END";
        }
        Token peek()
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
                if (looktype(TokenType.Keyword) || looktype(TokenType.Identifier) || lookahead(Checker.ClosedCurlyBraces) || lookahead(Checker.ClosedSquareBracket))
                {
                    return;
                }
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ", expected"));
            }
            return;
        }
    }

}


