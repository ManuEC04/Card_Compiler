namespace Compiler
{
    public class Declaration : ASTNode
    {
        public string Name { get; private set; }
        public Expression Expression { get; private set; }
        public object? Value { get; private set; }
        public override bool CheckSemantic()
        {
            return true;
        }
        public override void Evaluate()
        {
            Expression.Evaluate();
            Value = Expression.Value;

        }
        public Declaration(string name, Expression expression)
        {
            Name = name;
            Expression = expression;
        }
        public Declaration() { }
    }
}