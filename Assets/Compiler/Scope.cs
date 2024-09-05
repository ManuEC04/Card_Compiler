using System.Collections.Generic;
namespace Compiler
{
    
   public class Scope
    {
        public Scope Parent ;

        public Dictionary<string , Expression> Declaration;

        public Scope()
        {
            Declaration = new Dictionary<string, Expression>();   
        }

        public Scope CreateChild()
        {
            Scope child = new Scope();
            child.Parent = this;
               
            return child;
        }

    }
}