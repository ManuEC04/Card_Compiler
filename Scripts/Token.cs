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
            Position = Position;
        }
        public override string ToString()
        {
            return $"Type: {Type}, Value: {Value}";
        }
    }
    public enum TokenType
    {
      Keyword , Number , Plus , Minus , Multiplication , Division , Comparison_Op , Logic_Op , Concatenation_Op , Symbol , StatementSeparator , Comma ,
      Whitespace , Identifier , Text ,Unknown , EOF
    }
    public class TokenValues
    {
        public int position{get;set;}
        public string Points { get; private set; }
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
        public string Picture {get; private set;}
        public string Faction { get; private set; }
        public string Power { get; private set; }
        public string Range { get; private set; }
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


        public TokenValues()
        {
            Points = ":";
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
        }
    }
}