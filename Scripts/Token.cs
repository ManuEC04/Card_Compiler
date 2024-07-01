namespace Compiler
{
    public class Token
    {
        public string Type { get; private set; }
        public string Value { get; private set; }
        public int Position { get; private set; }
        public Token(string type, string value, int position)
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
    public class TokenValues
    {
        public string Points { get; private set; }
        public string Comma {get; private set;}
        public string OpenSquareBracket {get; private set;}
        public string ClosedSquareBracket{get; private set;}
        public string OpenCurlyBraces { get; private set; }
        public string ClosedCurlyBraces { get; private set; }
        public string Card{get; private set;}
        public string Type { get; private set; }
        public string Name { get; private set; }
        public string Faction { get; private set; }
        public string Power { get; private set; }
        public string Range { get; private set; }


        public TokenValues()
        {
            Points = ":";
            Comma = ",";
            OpenSquareBracket = "[";
            ClosedSquareBracket = "]";
            OpenCurlyBraces = "{";
            ClosedCurlyBraces = "}";
            Card = "Card";
            Type = "Type";
            Name = "Name";
            Faction = "Faction";
            Power = "Power";
            Range = "Range";
        }
    }
}