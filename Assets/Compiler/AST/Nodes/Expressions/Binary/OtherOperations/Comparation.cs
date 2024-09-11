
namespace Compiler
{
    public class Comparation : BinaryExpression
    {
        public string op = null;
        public override void Evaluate()
        {
            if(op !=null)
            {
                Value = op;
            }
            Left.Evaluate();
            Right.Evaluate();
            switch (Value)
            {
                case ">=": op = ">=";Value = (double)Left.Value >= (double)Right.Value; break;
                case ">": op = ">";Value = (double)Left.Value > (double)Right.Value; break;
                case "<=": op = "<=";Value = (double)Left.Value <= (double)Right.Value; break;
                case "<": op = "<";UnityEngine.Debug.Log("Esta es la parte izquierda de Comparation" + Left.Value);Value = (double)Left.Value < (double)Right.Value; break;
                case "==": op = "==";Value = Left.Value == Right.Value; break;
            }
        }
        public Comparation(Expression left, Expression right, object value, int position) : base(value, left, right, ExpressionType.Boolean, position)
        {

        }
    }
}