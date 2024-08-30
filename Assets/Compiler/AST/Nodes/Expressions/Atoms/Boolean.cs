namespace Compiler
{
    public class Boolean : AtomExpression
    {
        public override object Value {get;set;}
        public Boolean (object value , int position) : base(value , ExpressionType.Boolean , position)
        {
            Value = value;
        }
    }
}