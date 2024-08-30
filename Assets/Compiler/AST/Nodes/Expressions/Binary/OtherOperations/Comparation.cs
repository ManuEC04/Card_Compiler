
namespace Compiler
{
    public class Comparation : BinaryExpression
    {
        public override void Evaluate()
        {
            Left.Evaluate();
            Right.Evaluate();
            switch (Value)
            {
                case ">=": Value = (double)Left.Value >= (double)Right.Value; break;
                case ">": Value = (double)Left.Value > (double)Right.Value; break;
                case "<=": Value = (double)Left.Value <= (double)Right.Value; break;
                case "<": Value = (double)Left.Value < (double)Right.Value; break;
                case "==": Value = Left.Value == Right.Value; break;
            }
        }
        public Comparation(Expression left, Expression right, object value, int position) : base(value, left, right, ExpressionType.Boolean, position)
        {

        }
    }
}