namespace Compiler
{
    public class Parenthesis : Expression
    {
        public Expression Term {get;set;}
        public override void Evaluate()
        {
            Term.Evaluate();
            Value = Term.Value;
        }
        public override bool CheckSemantic()
        {
           return true;
        }
        public Parenthesis (object value , ExpressionType type , Expression term) : base (value , type)
        {
             Term = term;
        }
    }
}