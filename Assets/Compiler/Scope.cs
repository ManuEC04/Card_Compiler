using System.Collections.Generic;
namespace Compiler
{
    
   public class Scope
    {
        public Scope Parent ;

        public List<Declaration> Declaration;

        public Scope()
        {
            Declaration = new List<Declaration>();   
        }

        public Scope CreateChild()
        {
            Scope child = new Scope();
            child.Parent = this;
               
            return child;
        }

    }
}