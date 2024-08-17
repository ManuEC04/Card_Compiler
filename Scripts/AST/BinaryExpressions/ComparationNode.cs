namespace Compiler
{
    public class Comparation : BinaryExpression
    {
        public override void Evaluate()
        {
            Left.Evaluate();
            Right.Evaluate();
            switch(Value)
            {
                case ">=" : Console.WriteLine((double)Left.Value >= (double)Right.Value);break;
                case ">": Console.WriteLine((double)Left.Value > (double)Right.Value);break;
                case "<=": Console.WriteLine((double)Left.Value <= (double)Right.Value);break;
                case "<": Console.WriteLine((double)Left.Value < (double)Right.Value);break; 
                case "==": Console.WriteLine((double)Left.Value == (double)Right.Value);break;     
            }
        }
        public Comparation(Expression left , Expression right , object value , ExpressionType type):base(value , type , left , right)
        {
            type = ExpressionType.Binary;
        }
    }
}