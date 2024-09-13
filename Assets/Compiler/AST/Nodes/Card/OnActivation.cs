using System.Collections.Generic;
using UnityEngine;
namespace Compiler
{
    public class OnActivation : ASTNode
    {
        public List<DeclaredEffect> Effects { get; set; }

        public OnActivation()
        {
            Effects = new List<DeclaredEffect>();
        }
        public override bool CheckSemantic(Context context, List<CompilingError> errors , Scope scope)
        {
            foreach (DeclaredEffect effect in Effects)
            {
                if (!context.Effects.ContainsKey((string)effect.Name.Value))
                {
                    errors.Add(new CompilingError(Position , ErrorCode.Invalid , "This effect is not declared"));
                    return false;
                }
                if (!effect.CheckSemantic(context, errors , scope))
                {
                    return false;
                }
            }
            return true;
        }
        public override void Evaluate()
        {
            foreach (DeclaredEffect effect in Effects)
            {
                effect.Evaluate();
            }
        }
    }
}