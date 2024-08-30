
using System.Collections.Generic;
namespace Compiler
{
    public class Identifier : Expression
    {
        public override object Value {get; set;}
        public Expression? Expression {get; set;}

        public override void Evaluate()
        {
           
        }
        public override bool CheckSemantic(Context Context , List<CompilingError> Errors , Scope scope)
        {
           return true;
        }

        public Identifier(object Value , Expression Expression , ExpressionType Type , int Position) : base(Value , ExpressionType.Identifier , Position)
        {
            this.Value = Value;
            this.Position = Position;
            this.Expression = Expression;
        }
        public Identifier(object Value , int Position) : base(ExpressionType.Identifier)
        {
            this.Value = Value;
            this.Position = Position;

        }

    }
}