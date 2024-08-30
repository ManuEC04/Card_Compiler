using System.Collections.Generic;
namespace Compiler
{
    public class Selector : ASTNode
    {
        public Expression? Source { get; set; }
        public Expression? Single { get; set; }
        public Predicate? Predicate { get; set; }


        public override void Evaluate()
        {
       
        }

        public override bool CheckSemantic(Context context, List<CompilingError> errors, Scope scope)
        {
                if(Source == null || Single == null || Predicate == null)
            {
                return false;
            }
            if (!((string)Source.Value == "parent") && !((string)Source.Value == "board") && !((string)Source.Value == "hand")
             && !((string)Source.Value == "otherhand") && !((string)Source.Value == "deck") && !((string)Source.Value == "otherdeck")
             && !((string)Source.Value == "field") && !((string)Source.Value == "otherfield"))
            {
                   errors.Add(new CompilingError(Position , ErrorCode.Invalid , "Yo must declare a valid Source"));
                   return false;
            }
            if(Single.Type != ExpressionType.Boolean)
            {
                errors.Add(new CompilingError(Position , ErrorCode.Invalid , "Yo must declare a valid Single"));
                return false;
            }

            return true && Predicate.CheckSemantic(context , errors , scope);
        }
        public Selector()
        {
            Source = null;
            Single = null;
            Predicate = null;
        }

    }
}