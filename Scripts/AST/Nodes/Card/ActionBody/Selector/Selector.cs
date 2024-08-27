namespace Compiler{
    public class Selector : ASTNode
    {
        public Expression? Source {get; set;}
        public Expression? Single {get; set;}
        public Predicate? Predicate {get; set;}


        public override void Evaluate()
        {

        }

        public override bool CheckSemantic(Context context , List<CompilingError> errors)
        {
            return true;
        }
        public Selector()
        {
            Source = null;
            Single = null;
            Predicate = null;
        }

    }
}