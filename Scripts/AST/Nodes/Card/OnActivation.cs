namespace Compiler
{
    public class OnActivation : ASTNode
    {
        public List<DeclaredEffect> Effects { get; set; }

        public OnActivation()
        {
            Effects = new List<DeclaredEffect>();
        }
        public override bool CheckSemantic(Context context, List<CompilingError> errors)
        {
            foreach (DeclaredEffect effect in Effects)
            {
                if (!context.Effects.ContainsKey((string)effect.Name.Value))
                {
                    return false;
                }
                if (!effect.CheckSemantic(context, errors))
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