using System.Collections.Generic;
namespace Compiler
{
    using System.Collections.Generic;
    public abstract class  Expression : ASTNode
    {
        public abstract object Value { get; set; }
        public ExpressionType Type { get; protected set; }
        public override void Evaluate() { }
        public override bool CheckSemantic(Context Context , List<CompilingError> Errors , Scope scope)
        {
            return true;
        }
        public Expression(object value, ExpressionType type , int position)
        {
            Value = value;
            Type = type;
            Position = position;
        }
        public Expression(ExpressionType type)
        {
            Value = "";
            Type = type;
        }
    }
    public enum ExpressionType
    {
        Number,
        Text,
        Plus,
        Minus,
        Mul,
        Div,
        Elevate,
        Boolean,
        Unary,
        Concatenation,
        Identifier, 
        Property,
        Declaration,
        Merge,
        Loop,
        Null
    }
}