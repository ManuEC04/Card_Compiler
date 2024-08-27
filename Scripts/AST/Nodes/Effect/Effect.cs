namespace Compiler
{
    public class Effect : ASTNode
    {
        public Expression? Name {get; set;}
        public List<Expression>? Params {get; set;}
        public List<Expression>? Action {get; set;}

        public override void Evaluate()
        {
            throw new NotImplementedException();
        }
        public override bool CheckSemantic(Context Context , List<CompilingError> Erros)
        {
            throw new NotImplementedException();
        }
    }
}