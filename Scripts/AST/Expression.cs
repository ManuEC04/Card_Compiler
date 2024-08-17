namespace Compiler
{
    public abstract class  Expression : ASTNode
    {
        public abstract object Value { get; protected set; }
        public ExpressionType Type { get; protected set; }
        public override void Evaluate() { }
        public override bool CheckSemantic()
        {
            return true;
        }
        public Expression(object value, ExpressionType type)
        {
            Value = value;
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
        Merge
    }
}