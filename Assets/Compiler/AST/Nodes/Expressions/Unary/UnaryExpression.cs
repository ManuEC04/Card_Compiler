using System.Collections.Generic;
namespace Compiler
{
    public class UnaryExpression : Expression
    {
        public override object Value { get; set; }
        Expression Expr { get; set; }

        public UnaryExpression(object value, Expression expr, ExpressionType type , int position) : base(value, type , position)
        {
            Value = value;
            Expr = expr;
            type = ExpressionType.Unary;
        }
        public override bool CheckSemantic(Context Context , List<CompilingError> Errors , Scope scope)
        {
            if (Expr.Type == ExpressionType.Number)
            {
                return true;
            }
            return false;
        }
        public override void Evaluate()
        {
            Expr.Evaluate();

            if((string)Value == "++")
            {
                double temp =  (double)Expr.Value;
                temp++;
                Value = temp;    
            }
            else if ((string)Value == "--")
            {
                double temp = (double)Expr.Value;
                temp--;
                Value = temp;
            }
     
        }

    }
}