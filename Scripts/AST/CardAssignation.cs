namespace Compiler
{    public class CardAssignation : ASTNode
    {
        public Expression? Type_Assignation {get;set;}
        public Expression? Name_Assignation {get;set;}
        public Expression? Faction_Assignation {get;set;}
        public Expression? Power_Assignation {get;set;}
        public Expression? Range_Assignation {get;set;}
        public override bool CheckSemantic()
        {
            return true;
        }

        public void CheckAssignations()
        {

        }
    }

}
