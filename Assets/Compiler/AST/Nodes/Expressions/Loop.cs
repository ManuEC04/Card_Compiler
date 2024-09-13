
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
                UnityEngine.Debug.Log("Estamos en el while y esto vale el argument " + " " + Argument.Value);
                foreach (Expression expr in Instructions)
                {
                    expr.Evaluate();
                }
                Argument.Evaluate();
                UnityEngine.Debug.Log("Cierra un while");
                UnityEngine.Debug.Log(Argument.Value);
            }
            
        }
        public override void ResetValues()
        {
            Argument.ResetValues();
            foreach (Expression expr in Instructions)
            {
                expr.ResetValues();
                
            }
            UnityEngine.Debug.Log("Vamos a resetear el argument del while");
            
        }
        public override bool CheckSemantic(Context Context, List<CompilingError> Errors, Scope scope)
        {
            UnityEngine.Debug.Log("Se chequea semanticamente el while");
             foreach(Expression ins in Instructions)
            {
                ins.CheckSemantic(Context , Errors , scope);
            }
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
            while(context.targets.Count > 0)
            {
                UnityEngine.Debug.Log("Este es el count de los targets:" + " " +context.targets.Count);
               
                foreach (Expression ins in Instructions)
                {
                    ins.Evaluate();
                }
                ResetValues();
                context.targets[0].GetComponent<CardOutput>().UpdateProperties();
                context.targets.RemoveAt(0);
            }
        }
        public override void ResetValues()
        {
            foreach (Expression ins in Instructions)
            {
                ins.ResetValues();
            }
        }
        public override bool CheckSemantic(Context Context, List<CompilingError> Errors, Scope scope)
        {
            UnityEngine.Debug.Log("Se chequea semanticamente el for");
            foreach(Expression ins in Instructions)
            {
                ins.CheckSemantic(Context , Errors , scope);
            }
            return true;
        }
        public For(List<Expression> Instructions, int Position) : base(ExpressionType.Loop)
        {
            Value = "for";
            this.Instructions = Instructions;
        }
    }

}