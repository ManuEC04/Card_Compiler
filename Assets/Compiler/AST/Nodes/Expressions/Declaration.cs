using System.Collections.Generic;
namespace Compiler
{
    public class Declaration: Expression
    {
    public Identifier Identifier {get; set;}
    public override object Value {get;set;}
        public Expression? Expression {get; set;}

        public override void Evaluate()
        {
            if(Expression !=null)
            {
                Expression.Evaluate();
                Value = Expression.Value;
            }
        }
        public override bool CheckSemantic(Context Context, List<CompilingError> Errors , Scope scope)
        {
           if(scope.Declaration.Contains(this) || Expression == null)
           {
              Errors.Add(new CompilingError (Position , ErrorCode.Invalid , "You must assign a value for this identifier first"));
              return false;
           }
           if(!scope.Declaration.Contains(this) || Expression == null)
           { 
              scope.Declaration.Add(this);
           }
           return true;
        }

        public Declaration(Identifier Identifier , Expression Expression , int Position) : base("", ExpressionType.Declaration , Position)
        {
            Value= "";
            this.Identifier = Identifier;
            this.Expression = Expression;
        }
        public Declaration(Identifier Identifier , int Position) : base(ExpressionType.Identifier)
        {
            Value = "";
            this.Identifier = Identifier;

        }

    }
}