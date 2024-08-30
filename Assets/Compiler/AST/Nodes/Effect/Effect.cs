using System.Collections.Generic;namespace Compiler
{
    public class Effect : ASTNode
    {
        public Expression? Name {get; set;}
        public List<Expression>? Params {get; set;}
        public List<Expression>? Action {get; set;}

        public override void Evaluate()
        {
           
        }
        public override bool CheckSemantic(Context Context , List<CompilingError> Errors , Scope scope)
        {
            return true;
        }
    }
}