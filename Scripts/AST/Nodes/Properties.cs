
using System.Net.Http.Headers;
using System.Reflection.Metadata;

namespace Compiler
{
    public class Property : Expression
    {
        public Expression? Argument {get; set;}
        public string Sintaxys{get; set;}
        public override object Value { get; set; }
        public override void Evaluate()
        {
            if(Argument !=null)
            {
                 Argument.Evaluate();
            }
            
            FunctionContainer function = new FunctionContainer();
            Value = function.Functions[Sintaxys];
            
           
        }
        public override bool CheckSemantic(Context Context , List<CompilingError> Errors , Scope scope)
        {
            return true;
        }

        public Property( string Sintaxys, int Position) : base("", ExpressionType.Property, Position)
        {
            Value= "";
            this.Sintaxys = Sintaxys;
            this.Position = Position;
        }
         public Property( string Sintaxys, Expression Argument ,int Position) : base(".", ExpressionType.Property, Position)
        {
            Value = "";
            this.Argument = Argument;
            this.Sintaxys = Sintaxys;
            this.Position = Position;
        }

    }
}