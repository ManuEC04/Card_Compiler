namespace Compiler
{
    public class Parenthesis : Expression
    {
        public override object Value { get; set; }
        public Expression Term { get; set; }
        public override void Evaluate()
        {
            Term.Evaluate();
            Value = Term.Value;
        }
        public override bool CheckSemantic(Context Context , List<CompilingError> Errors , Scope scope)
        {
            return true;
        }
        public Parenthesis(object value, ExpressionType type, Expression term, int position) : base(value, ExpressionType.Merge, position)
        {
            Value = value;
            Term = term;
        }
    }
}