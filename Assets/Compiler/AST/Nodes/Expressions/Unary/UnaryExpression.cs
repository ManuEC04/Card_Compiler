using System.Collections.Generic;
using System.Diagnostics;
namespace Compiler
{
    public class UnaryExpression : Expression
    {
        public override object Value { get; set; }
        public object op;
        bool done;
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
          public override void ResetValues()
        {
            done = false;
        }
        public override void Evaluate()
        {
            if(!done)
            {
             Expr.Evaluate();
             done = true;
            }
            UnityEngine.Debug.Log(Value);
            if((string)op == "++")
            {
                double temp = (double)Value;
                temp++;
                Expr.Value = temp;
                Value = temp;

            }
            else if((string)Value == "++")
            {
                double temp =  (double)Expr.Value;
                temp++;
                Expr.Value = temp;
                op = Value;
                Value = temp; 
            }
            else if ((string)Value == "--")
            {
                double temp = (double)Expr.Value;
                temp--;
                Expr.Value = temp;
                op = Value;
                Value = temp;
            }
            UnityEngine.Debug.Log("Evalua correctamente la expresion unaria");
     
        }

    }
}