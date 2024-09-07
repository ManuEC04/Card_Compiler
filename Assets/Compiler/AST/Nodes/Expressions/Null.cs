using System.Linq.Expressions;

namespace Compiler
{
    public class Null : Expression
    {
        public override object Value {get;set;}

        public Null() : base(ExpressionType.Null)
        {
            Value = "";
        }
          public override void ResetValues()
        {
            
        }
    }
}