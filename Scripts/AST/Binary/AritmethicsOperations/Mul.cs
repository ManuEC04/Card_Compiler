namespace Compiler
{
    public class Mul : BinaryExpression
    {
        public override void Evaluate()
        {
            Left.Evaluate();
            Right.Evaluate();

            Value = (double)Left.Value * (double)Right.Value;
        }
         public override bool CheckSemantic()
        {
            if (Left.Type == ExpressionType.Number || Right.Type == ExpressionType.Number)
            {
                return true;
            }
            return false;
        }
        public Mul(Expression left, Expression right, object value) : base(value,left, right , ExpressionType.Mul) {

         }
    }
}