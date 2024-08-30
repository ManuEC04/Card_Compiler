
using System.Collections.Generic;
namespace Compiler
{
    public class PostAction : ASTNode
    {

        public Expression? Type {get; set;}
        public Selector? Selector {get; set;}
        public PostAction? Post {get; set;}
        
        public override void Evaluate()
        {
            if(Post != null)
            {
              Post.Evaluate();
            }
            if(Selector !=null)
            {
              Selector.Evaluate();
            }
        }
        public override bool CheckSemantic(Context Context, List<CompilingError> Errors , Scope scope)
        {
            return true;
        }

    }
   
}