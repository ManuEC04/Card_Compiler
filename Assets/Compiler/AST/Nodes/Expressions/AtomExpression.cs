using System.Collections.Generic;
namespace Compiler
{
    public abstract class AtomExpression : Expression
    {
        public override bool CheckSemantic(Context Context , List<CompilingError> Errors , Scope scope)
        {
            return true;
        }
          public override void ResetValues()
        {
            
        }
        public AtomExpression(object value, ExpressionType type, int position) : base(value, type, position) { }
    }
}