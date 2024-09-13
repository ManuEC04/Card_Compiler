//Here we verify that the sequence of tokens complies with the language syntax through a function for each element
using System.Collections.Generic;
using System;
using System.Diagnostics;
using UnityEditor.PackageManager;
namespace Compiler
{
    public class Parser
    {
        int Pos { get; set; }
        public List<Token> tokens { get; private set; }
        TokenValues Checker = new TokenValues();
        Context context = Context.Instance;
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
                    Card Card = ParseCard();
                    Scope scope = new Scope();
                    tree.Nodes.Add(Card);

                }
                else if (match(Checker.effect))
                {
                    Effect? Effect = ParseEffect();
                    tree.Nodes.Add(Effect);
                    if (Effect != null)
                    {


                        UnityEngine.Debug.Log(Effect.Name.Value);
                        Context context = Context.Instance;
                        context.Effects.Add((string)Effect.Name.Value, Effect);
                        UnityEngine.Debug.Log(context.Effects.Count);
                    }
                    else { UnityEngine.Debug.Log("El efecto es nulo"); }
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
                    UnityEngine.Debug.Log("Parsea el name");
                }
                else if (match(Checker.Params))
                {
                    effect.Params = ParseParams();
                    UnityEngine.Debug.Log("Parsea Params");
                }
                else if (match(Checker.Action))
                {
                    effect.Action = ParseAction();
                    UnityEngine.Debug.Log("Parsea Action");
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
                //Here we add the Param declaration to the declarations list
                Context context = Context.Instance;
                context.scope.Declaration.Add((string)identifier.Value , identifier.Expression);
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
            Actions = ParseInstructions();
            if (!match(Checker.ClosedCurlyBraces))
            {
                Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "} expected"));
            }

            return Actions;

        }
        //PARSING CARDS
        public Card ParseCard()
        {
            Card card = new Card(peek().Position);
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
            while (lookahead(Checker.Name) || looktype(TokenType.Identifier))
            {
                if (match(Checker.Name))
                {
                    effect.Name = ParseTextProperties();
                    checkcomma();

                }
                else if (matchtype(TokenType.Identifier))
                {
                    Back();
                    Identifier identifier = ParseIdentifier();
                    if (!match(Checker.Points))
                    {
                        Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ": expected"));
                    }
                    Expression expr = ParsingExpressions();
                    Declaration declaration = new Declaration(identifier , expr , peek().Position);
                    effect.Params.Add(declaration);
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
            while (lookahead(Checker.Type) || lookahead(Checker.Selector) || lookahead(Checker.PostAction))
            {
                if (match(Checker.Type))
                {
                    postAction.Name = ParseTextProperties();
                    checkcomma();

                }
                else if (match(Checker.Selector))
                {
                    postAction.Selector = ParseSelector();
                    checkcomma();
                }
                else if (match(Checker.PostAction))
                {
                    postAction.PostAction = ParsePostAction();
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
            if (!match(Checker.Points))
            {
                // Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, ": expected"));
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
            Identifier identifier = ParseIdentifier();
            Property property = new Property("", identifier, peek().Position);
            if (match(Checker.Point))
            {
                if (matchtype(TokenType.Keyword))
                {
                    property = new Property(previous().Value.ToString(), identifier, peek().Position);
                }
            }

            Back(); // We go back because the NodeComparation analyze first the left part of the expression
            Expression comparation = ParseComparation();
            return new Predicate(property, (Comparation)comparation, peek().Position);
        }

        //PARSING OTHER NODES
        For ParseFor()
        {
            if (!match(Checker.target))
            {
                Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "target expected "));
            }
            if (!match(Checker.In))
            {
                Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "in  expected "));
            }
            if (!match(Checker.targets))
            {
                Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "targets  expected "));
            }
            if (!match(Checker.OpenCurlyBraces))
            {
                Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "{  expected "));
            }
            List<Expression> instructions = ParseInstructions();
            if (!match(Checker.ClosedCurlyBraces))
            {
                Errors.Add(new CompilingError(Pos, ErrorCode.Expected, "}  expected "));
            }
            checkend();
            return new For(instructions, peek().Position);
        }
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
            UnityEngine.Debug.Log("Empieza a parsear instrucciones");
            while (looktype(TokenType.Identifier) || looktype(TokenType.Keyword))
            {
                if (match(Checker.While))
                {
                    Instructions.Add(ParseWhile());
                }
                else if (match(Checker.For))
                {
                    Instructions.Add(ParseFor());
                }
                else { advance(); }

                if (lookahead(Checker.Point))
                {
                    UnityEngine.Debug.Log("Encontro una propiedad");
                    Back();
                    Expression property = ParseProperty();
                    UnityEngine.Debug.Log("Termino la propiedad");
                    UnityEngine.Debug.Log(peek().Value);
                    if (match(Checker.Equal) || matchtype(TokenType.EqualMinus) || matchtype(TokenType.EqualPlus))
                    {
                        UnityEngine.Debug.Log("Aqui detecta que lo que viene es una asignacion");
                        Token op = previous();
                        Expression right = ParsingExpressions();
                        Asignation asignation = new Asignation(property, right, op.Value, peek().Position);
                        Instructions.Add(asignation);
                        checkend();
                    }
                    else
                    {
                        UnityEngine.Debug.Log("Detecta que no viene ni -= ni += ni =");
                        UnityEngine.Debug.Log("Detecta que no viene ni -= ni += ni =");
                        Instructions.Add(property);
                        checkend();
                    }
                }
                else if (lookahead(Checker.Equal))
                {
                    UnityEngine.Debug.Log(peek().Value);
                    Back();
                    UnityEngine.Debug.Log(peek().Value);
                    Instructions.Add(ParseDeclaration());
                    checkend();
                }

            }
            UnityEngine.Debug.Log("Ya va a retornar las instrucciones");
            return Instructions;
        }
        Expression ParseProperty()
        {
            UnityEngine.Debug.Log("Parsea la propiedad");
            UnityEngine.Debug.Log(peek().Value);

            Identifier? identifier = ParseIdentifier();
            if (identifier != null)
            {
                UnityEngine.Debug.Log("El identifier de la prop no es null");
            }
            else
            {
                UnityEngine.Debug.Log("El identifier es null");
            }
            Expression Argument = null;
             Expression index = null;
            string Sintaxys = null;
            string CardContainer = null;
            string Method = null;
            if (lookahead(Checker.context) || lookahead(Checker.target))
            {
                UnityEngine.Debug.Log("Encontro a context o a target");
                Sintaxys = peek().Value;
                advance();
            }
            if (match(Checker.Point))
            {
                UnityEngine.Debug.Log("Encuentra el CardContainer");
                CardContainer = peek().Value;
                advance();

            }
            if (match(Checker.Point))
            {
                UnityEngine.Debug.Log("Encuentra algun metodo");
                Method = peek().Value;
                advance();
            }
            if (match(Checker.OpenParenthesis))
            {
                if (!match(Checker.ClosedParenthesis))
                {
                    Argument = ParsePropertyArgument();
                }
                if (!match(Checker.ClosedParenthesis))
                {
                    Errors.Add(new CompilingError(Pos, ErrorCode.Expected, ") expected"));
                }
            }
            if(match(Checker.OpenSquareBracket))
            {
                index = ParsingExpressions();
            }
            if(!match(Checker.ClosedSquareBracket))
            {
                Errors.Add(new CompilingError(Pos , ErrorCode.Expected , "] expected"));
            }
            UnityEngine.Debug.Log(peek().Value);
            return new Property(Sintaxys, CardContainer, Method, identifier, Argument, index , peek().Position);
        }
        Expression? ParsePropertyArgument()
        {
            if (matchtype(TokenType.Identifier) || matchtype(TokenType.Keyword))
            {

                if (lookahead(Checker.Point))
                {
                    Back();
                    UnityEngine.Debug.Log("Sabe que es una propiedad");
                    Expression property = ParseProperty();
                    UnityEngine.Debug.Log("La parsea");
                    return property;
                }
                Back();
                Expression expr = ParseIdentifier();
                return expr;
            }
            else if (match(Checker.QuotationMark))
            {
                Expression expr = ParseConcatenation();
                return expr;
            }
            else if (looktype(TokenType.Number))
            {
                Expression expr = ParseTerm();
                return expr;
            }
            else if (lookahead(Checker.OpenParenthesis))
            {
                Expression expr = ParsePredicate();
                if (!match(Checker.ClosedParenthesis))
                {
                    Errors.Add(new CompilingError(Pos, ErrorCode.Expected, ") was expected"));
                }
                return expr;
            }
            return null;
        }
        Declaration ParseDeclaration()
        {
            Identifier identifier = ParseIdentifier();
            if (!match(Checker.Equal))
            {
                return null;
            }
            if (lookahead(Checker.QuotationMark))
            {
                Expression text = ParseConcatenation();
                text.Evaluate();
                return new Declaration(identifier, text, peek().Position);
            }
            Expression expr = ParsePropertyArgument();
            UnityEngine.Debug.Log(expr.Value);
            Context context = Context.Instance;
            //context.scope.Declaration.Add((string)identifier.Value, expr);
            UnityEngine.Debug.Log(identifier.Value);
            return new Declaration(identifier, expr, peek().Position);
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
            if (matchtype(TokenType.Identifier) || match(Checker.target))
            {
                
                Token token = previous();
                Expression index = null;
                if (match(Checker.OpenSquareBracket))
                {
                    throw new Exception("Detecta el if");
                    index = ParseTerm();
                    
                    if (!match(Checker.ClosedSquareBracket))
                    {
                        Errors.Add(new CompilingError(peek().Position, ErrorCode.Expected, "] expected"));
                    }
                }
                Identifier declaration = new Identifier(token.Value, index, previous().Position);
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
                if (lookahead(Checker.Point))
                {
                    Back();
                    Expression expr = ParseProperty();
                    return expr;
                }
                return new Identifier(previous().Value, peek().Position);
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
                Identifier identifier = new Identifier(previous().Value, previous().Position);
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
            if (lookahead(Checker.Equal) || lookahead(Checker.Minus) || lookahead(Checker.Plus))
            {
                return;
            }
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


