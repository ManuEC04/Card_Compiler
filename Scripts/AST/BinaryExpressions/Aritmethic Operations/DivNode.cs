namespace Compiler
{
    public class Div : BinaryExpression
    {
        public override void Evaluate()
        {
            Left.Evaluate();
            Right.Evaluate();

            Value = (double)Left.Value / (double)Right.Value;
        }
      public override bool CheckSemantic()
        {
            if (Left.Type == ExpressionType.Number || Right.Type == ExpressionType.Number)
            {
                return true;
            }
            return false;
        }
        public Div(Expression left , Expression right , object value , ExpressionType type):base(value , type , left , right)
        {
            type = ExpressionType.Binary;
        }
    }
}