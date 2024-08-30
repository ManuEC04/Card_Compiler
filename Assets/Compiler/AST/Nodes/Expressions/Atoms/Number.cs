namespace Compiler
{
    public class Number : AtomExpression
    {
        public override object Value {get; set;}
        public Number (double value , int position) : base(value , ExpressionType.Number , position)
        {
            Value = value;
        }
    }
}