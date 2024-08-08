namespace Compiler
{
    public class NumberNode : AtomExpression
    {
        public override bool CheckSemantic()
        {
            return true;
        }
        public NumberNode (double value) : base(value , ExpressionType.Number)
        {
            Value = value;
        }
        public override void Evaluate()
        {

        }
    }
}