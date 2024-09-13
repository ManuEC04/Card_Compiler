
using System.Collections.Generic;
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
                return;
            }
            else if (Value.Equals("context"))
            {
                OriginalValue = Value;
                Value = Context.Instance;
                done = true;
                return;
            }
            else if (!done)
            {
                UnityEngine.Debug.Log("Vamos a evaluar el identifier");
                OriginalValue = Value;
                Context context = Context.Instance;
                UnityEngine.Debug.Log(Value);
                Expression expr = context.scope.Declaration[(string)Value];
                expr.Evaluate();
                UnityEngine.Debug.Log("Esta es la expresion que obtiene el identifier del context" + expr.Value);
                Value = expr.Value;
                done = true;

            }


        }
        public override bool CheckSemantic(Context Context, List<CompilingError> Errors, Scope scope)
        {
            if((string)Value != "target" && (string)Value != "context")
            {
               if (scope.Declaration.ContainsKey((string)Value))
            {
                return true;
            }
            }
            
            return false;
        }
        public override void ResetValues()
        {
            UnityEngine.Debug.Log("Se resetea el identifier");
            Value = OriginalValue;
            done = false;
        }


        public Identifier(object Value, int Position) : base(ExpressionType.Identifier)
        {
            this.Value = Value;
            this.Position = Position;
        }
         public Identifier(object Value, object Index ,int Position) : base(ExpressionType.Identifier)
        {
            this.Value = Value;
            this.Position = Position;
        }

    }
}