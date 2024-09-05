
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
namespace Compiler

{
    public class While : Expression
    {
        public override object Value { get; set; }
        public Expression? Argument { get; set; }
        List<Expression>? Instructions { get; set; }
        public override void Evaluate()
        {
            UnityEngine.Debug.Log("Va a entrar en el while");
            Argument.Evaluate();
            while ((bool)Argument.Value)
            {
                foreach (Expression expr in Instructions)
                {
                    expr.Evaluate();
                }
            }
        }
        public override bool CheckSemantic(Context Context, List<CompilingError> Errors, Scope scope)
        {
            return true;
        }
        public While(Expression Argument, int Position) : base(ExpressionType.Loop)
        {
            Value = "while";
            this.Argument = Argument;
        }
        public While(Expression Argument, List<Expression> Instructions, int Position) : base(ExpressionType.Loop)
        {
            Value = "while";
            this.Argument = Argument;
            this.Instructions = Instructions;
            this.Position = Position;
        }
    }
    public class For : Expression
    {
        public override object Value { get; set; }
        List<Expression> Instructions { get; set; }
        public override void Evaluate()
        {
            Context context = Context.Instance;
            foreach (GameObject target in context.targets)
            {
                foreach (Expression ins in Instructions)
                {
                    ins.Evaluate();
                }
            }
        }
        public override bool CheckSemantic(Context Context, List<CompilingError> Errors, Scope scope)
        {
            return true;
        }
        public For(List<Expression> Instructions, int Position) : base(ExpressionType.Loop)
        {
            Value = "for";
            this.Instructions = Instructions;
        }
    }

}