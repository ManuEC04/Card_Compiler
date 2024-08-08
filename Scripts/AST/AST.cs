namespace Compiler
{
    public class AST
    {
        List<ASTNode> Nodes = new List<ASTNode>();
    }
    public abstract class ASTNode
    {
        public int Position {get; set;}
        public abstract bool CheckSemantic();
        
    }

}
