
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
namespace Compiler
{
    public class Identifier : Expression
    {
        public override object Value { get; set; }
        Expression SavedExpression;
        public Expression? Expression { get; set; }
        bool done;

        public override void Evaluate()
        {
            if (!done)
            {
                UnityEngine.Debug.Log("Evaluamos el identificador");
                Context context = Context.Instance;
                Expression expr = context.scope.Declaration[(string)Value];
                SavedExpression = expr;
                expr.Evaluate();
                Value = expr.Value;
                done = true;
            }
        }
        public override bool CheckSemantic(Context Context, List<CompilingError> Errors, Scope scope)
        {
            UnityEngine.Debug.Log("Chequeo semantico y obtengo el valor del identifier");
            if (scope.Declaration[(string)Value] != null)
            {
                scope.Declaration[(string)Value].Evaluate();
                Value = scope.Declaration[(string)Value].Value;
                return true;
            }
            return false;
        }
          public override void ResetValues()
        {
            Expression = SavedExpression;
            Expression.Evaluate();
            Value = Expression.Value;
            UnityEngine.Debug.Log("Se reseteo el identificador" + Expression.Value);
        }

        public Identifier(object Value, Expression Expression, ExpressionType Type, int Position) : base(Value, ExpressionType.Identifier, Position)
        {
            this.Value = Value;
            this.Position = Position;
            this.Expression = Expression;
        }
        public Identifier(object Value, int Position) : base(ExpressionType.Identifier)
        {
            this.Value = Value;
            this.Position = Position;

        }

    }
}