using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

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
                        expr.Evaluate();
                    }
                     foreach (Expression expr in Action)
                    {
                        expr.ResetValues();
                    }
            }
            else
            {
                UnityEngine.Debug.Log("Actions da null");
            }

        }
        public override bool CheckSemantic(Context Context, List<CompilingError> Errors, Scope scope)
        {
            if(Name == null)
            {
                Errors.Add(new CompilingError(Position , ErrorCode.None , "The name of the effect cannot be null"));
                return false;
            }
            foreach(Expression expr in Action)
            {
                expr.CheckSemantic(Context , Errors , scope);
            }
            return true;
        }
    }
}