namespace Compiler
{
    public class Number : AtomExpression
    {
        public override object Value {get; protected set;}
        public override bool CheckSemantic()
        {
            return true;
        }
        public Number (double value) : base(value , ExpressionType.Number)
        {
            Value = value;
        }
        public override void Evaluate()
        {

        }
    }
}