namespace Compiler
{
    public class Token
    {
        public TokenType Type { get; private set; }
        public string Value { get; private set; }
        public int Position { get; private set; }
        public Token(TokenType type, string value, int position)
        {
            Type = type;
            Value = value;
            Position = position;
        }
        public override string ToString()
        {
            return $"Type: {Type}, Value: {Value}";
        }
    }
    public enum TokenType
    {
      Keyword , Number , DoublePlus , Plus , Minus , Multiplication , Division , Comparison_Op , Logic_Op , Concatenation_Op , Symbol , StatementSeparator , Comma ,
      Whitespace , Identifier , Text , Boolean , Unknown , EOF
    }
    public class TokenValues
    {
        public int position{get;set;}
        public string Points { get; private set; }
        public string Point {get ; private set;}
        public string Comma {get; private set;}
        public string OpenSquareBracket {get; private set;}
        public string ClosedSquareBracket{get; private set;}
        public string OpenCurlyBraces { get; private set; }
        public string ClosedCurlyBraces { get; private set; }
        public string OpenParenthesis{get; private set;}
        public string ClosedParenthesis {get; private set;}
        public string QuotationMark {get; private set;}
        public string Card{get; private set;}
        public string Type { get; private set; }
        public string Name { get; private set; }
        public string Faction { get; private set; }
        public string Power { get; private set; }
        public string Range { get; private set; }
        public string OnActivation {get; private set;}
        public string PostAction {get; private set;}
        public string Selector {get; private set;}
        public string Source {get; private set;}
        public string Predicate {get; private set;}
        public string Single {get; private set;}
        public string Effect{get; private set;}
        public string effect {get; private set;}
        public string Params {get; private set;}
        public string Action {get; private set;}
        public string Amount {get; private set;}
        public string DoublePlus {get; private set;}
        public string EqualPlus {get; private set;}
        public string DoubleMinus{get; private set;}
        public string EqualMinus {get; private set;}
        public string Plus {get ; private set;}
        public string Minus {get; private set;}
        public string Div {get; private set;}
        public string Mult {get; private set;}
        public string Elevate {get; private set;}
        public string Less {get; private set;}
        public string Less_Equal {get; private set;}
        public string Greater {get; private set;}
        public string Greater_Equal {get; private set;}
        public string Equal_Equal {get; private set;}
        public string Equal {get; private set;}
        public string Concatenation {get; private set;}
        public string Spaced_Concatenation{get;private set;}
        public string And {get; private set;}
        public string Or {get; private set;}
        public string StatementSeparator {get; private set;}
        public string context {get; private set;}
        public string targets {get; private set;}
        public string For {get; private set;}
        public string While {get; private set;}
        public string target{get; private set;}
        public string In {get; private set;}


        public TokenValues()
        {
            Points = ":";
            Point =".";
            Comma = ",";
            StatementSeparator = ";";
            OpenSquareBracket = "[";
            ClosedSquareBracket = "]";
            OpenCurlyBraces = "{";
            ClosedCurlyBraces = "}";
            OpenParenthesis = "(";
            ClosedParenthesis = ")";
            QuotationMark = "\"";
            Card = "Card";
            Type = "Type";
            Name = "Name";
            Faction = "Faction";
            Power = "Power";
            Range = "Range";
            OnActivation = "OnActivation";
            PostAction = "PostAction";
            Selector = "Selector";
            Source = "Source";
            Predicate = "Predicate";
            Amount = "Amount";
            Single = "Single";
            Effect = "Effect";
            effect = "effect";
            Params = "Params";
            Action = "Action";
            DoublePlus = "++";
            EqualPlus = "+=";
            DoubleMinus = "--";
            EqualMinus = "-=";
            Plus = "+";
            Minus = "-";
            Mult = "*";
            Div = "/";
            Elevate = "^";
            Less = "<";
            Less_Equal = "<=";
            Greater = ">";
            Greater_Equal = ">=";
            Equal_Equal = "==";
            Equal = "=";
            Concatenation = "@";
            And = "&&";
            Or = "||";
            Spaced_Concatenation = "@@";
            context = "context";
            targets = "targets";
            While = "while";
            For = "for";
            target = "target";
            In = "in";
        }
    }
}