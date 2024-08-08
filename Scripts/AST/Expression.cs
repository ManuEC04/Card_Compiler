namespace Compiler
{
      public class Expression : ASTNode
    {
        public object Value { get; set; }
        public ExpressionType Type {get; protected set;}
        public virtual void Evaluate(){}
        public override bool CheckSemantic()
        {
            return true;
        }
        public Expression(object value , ExpressionType type)
        {
            Value = value;
            Type = type;
        }


    }
}