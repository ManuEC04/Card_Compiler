namespace Compiler
{
    public class Parenthesis : Expression
    {
        public override object Value{get ; protected set;}
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
        public Parenthesis (object value , ExpressionType type , Expression term) : base (value , ExpressionType.Merge)
        {
            Value = value;
             Term = term;
        }
    }
}