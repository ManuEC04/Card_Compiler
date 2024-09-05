using System.Collections.Generic;
using System.Diagnostics;

namespace Compiler
{
    public class Effect : ASTNode
    {
        public Expression? Name { get; set; }
        public List<Expression>? Params { get; set; }
        public List<Expression>? Action { get; set; }

        public override void Evaluate()
        {
            if (Action != null)
            {
                foreach (Expression expr in Action)
                {
                    UnityEngine.Debug.Log("Se ejecuta un action");
                    UnityEngine.Debug.Log(Action.Count);
                    expr.Evaluate();
                }
            }
            else 
            {
                UnityEngine.Debug.Log("Actions da null");
            }

        }
        public override bool CheckSemantic(Context Context, List<CompilingError> Errors, Scope scope)
        {
            return true;
        }
    }
}