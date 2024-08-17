namespace Compiler
{
    public class Plus : BinaryExpression
    {
         public override void Evaluate()
        {
            Left.Evaluate();
            Right.Evaluate();

            Value = (double)Left.Value + (double)Right.Value;
        }
        public override bool CheckSemantic()
        {
            if (Left.Type == ExpressionType.Number || Right.Type == ExpressionType.Number)
            {
                return true;
            }
            return false;
        }
        public Plus(Expression left, Expression right, object value) : base(value,left, right , ExpressionType.Plus)
        {

        }
    }
}