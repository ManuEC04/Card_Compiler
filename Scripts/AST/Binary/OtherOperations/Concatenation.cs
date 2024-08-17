namespace Compiler
{
    public class Concatenation : BinaryExpression
    {
        public override void Evaluate()
        {
            Left.Evaluate();
            Right.Evaluate();
            if((string)Value == "@@")
            {
                Value = (string)Left.Value + " " + (string)Right.Value;
            }
            else if ((string)Value == "@")
            {
                Value = (string)Left.Value + (string)Right.Value;
            }
        }
        public override bool CheckSemantic()
        {
            if(Left.Type == ExpressionType.Text && Left.Type == ExpressionType.Text)
            {
                return true;
            }
            return false;
        }
        public Concatenation(Expression left , Expression right , object value):base(value , left , right , ExpressionType.Concatenation)
        {}

    }
}