namespace Compiler
{
    public class Elevate : BinaryExpression
    {
        public override bool CheckSemantic()
        {
            if(Left.Type == ExpressionType.Number || Right.Type ==ExpressionType.Number)
            {
                return true;
            }
            return false;
        }
        public override void Evaluate()
        {
            Left.Evaluate();
            Right.Evaluate();
            Value = Math.Pow((double)Left.Value , (double)Right.Value);
        }
        public Elevate(Expression left , Expression right , object value):base(value , left , right , ExpressionType.Elevate)
        {
        } 
    }
}