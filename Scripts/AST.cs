namespace Compiler
{
    public abstract class ASTNode
    {
        abstract public void Execute();
    }
    public class CardCreation : ASTNode
    {
        public string Type_Assignation { get; set; }
        public string Name_Assignation { get; set; }
        public string Faction_Assignation { get; set; }
        public string Power_Assignation { get; set; }
        public string Range_Assignation { get; set; }

        public CardCreation ()
        {
            Type_Assignation = "";
            Name_Assignation = "";
            Faction_Assignation = "";
            Power_Assignation = "";
            Range_Assignation = "";
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}