namespace Compiler
{
    public class Text : AtomExpression
    {
        public override object Value {get; set;}
        public Text (string value , int position) : base(value , ExpressionType.Text , position)
        {
            Value = value;
        }
    }
}