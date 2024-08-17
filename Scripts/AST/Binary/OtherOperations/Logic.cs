namespace Compiler
{
    public class Logic : BinaryExpression
    {
        public override void Evaluate()
        {
            Left.Evaluate();
            Right.Evaluate();
            if ((string)Value == "&&")
            {
                Value = (bool)Left.Value && (bool)Right.Value;
            }
            else if ((string)Value == "||")
            {
                Value = (bool)Left.Value || (bool)Right.Value;
            }
        }
        public override bool CheckSemantic()
        {
            if (Left.Type == ExpressionType.Boolean || Right.Type == ExpressionType.Boolean)
            {
                return true;
            }
            return false;
        }
        public Logic(Expression left, Expression right, object value) : base(value, left, right, ExpressionType.Boolean)
        { }
    }
}