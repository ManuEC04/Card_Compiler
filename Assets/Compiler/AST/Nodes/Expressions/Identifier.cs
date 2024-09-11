
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
namespace Compiler
{
    public class Identifier : Expression
    {
        public override object Value { get; set; }
        object OriginalValue;
        public Expression? Expression { get; set; }
        bool done = false;

        public override void Evaluate()
        {
            if (Value.Equals("target"))
            {
                OriginalValue = Value;
                Context context = Context.Instance;
                Value = context.targets[0];
                done = true;
            }
            else if (Value.Equals("context"))
            {
                OriginalValue = Value;
                Value = Context.Instance;
                done = true;
            }
            else if (!done)
            {
                OriginalValue = Value;
                Context context = Context.Instance;
                UnityEngine.Debug.Log(Value);
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
            Value = OriginalValue;
            done = false;
        }


        public Identifier(object Value, int Position) : base(ExpressionType.Identifier)
        {
            this.Value = Value;
            this.Position = Position;
        }

    }
}