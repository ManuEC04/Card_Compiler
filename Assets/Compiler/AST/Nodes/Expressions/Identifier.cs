
using System.Collections.Generic;
using System.Diagnostics;
namespace Compiler
{
    public class Identifier : Expression
    {
        public override object Value { get; set; }
        public Expression? Expression { get; set; }
        bool done;

        public override void Evaluate()
        {
            if (!done)
            {
                UnityEngine.Debug.Log("Evaluamos el identificador");
                Context context = Context.Instance;
                Expression expr = context.scope.Declaration[(string)Value];
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
            done = false;
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