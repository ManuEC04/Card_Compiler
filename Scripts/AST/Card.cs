namespace Compiler
{    public class Card : ASTNode
    {
        public Expression? Type {get;set;}
        public Expression? Name {get;set;}
        public Expression? Faction {get;set;}
        public Expression? Power {get;set;}
        public List<Expression>? Range {get;set;}
        public override bool CheckSemantic()
        {
            if(Type !=null && Name !=null && Faction !=null && Power !=null && Range !=null)
            {
                return true;
            }
            return false;
        }
        public override void Evaluate()
        {
            Power.Evaluate();  
        }
    }

}
