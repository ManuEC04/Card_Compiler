namespace Compiler
{
    public abstract class AtomExpression : Expression
    {
        public AtomExpression(object value , ExpressionType type ):base(value , type){}
    }
}