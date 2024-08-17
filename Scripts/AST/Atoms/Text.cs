namespace Compiler
{
    public class Text : Expression
    {
        public override object Value {get; protected set;}
        public override void Evaluate()
        {
            
        }
        public override bool CheckSemantic()
        {
            return true;
        }
        public Text (string value) : base(value , ExpressionType.Text)
        {
            Value = value;
        }
    }
}