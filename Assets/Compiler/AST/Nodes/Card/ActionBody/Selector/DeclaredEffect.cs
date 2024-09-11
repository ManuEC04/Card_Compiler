
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Compiler
{
    public class DeclaredEffect : ASTNode
    {
        public Expression Name { get; set; }
        public List <Expression> Params { get; set; }
        public Selector? Selector { get; set; }
        public PostAction? PostAction { get; set; }


        public override void Evaluate()
        {
            if(Selector !=null)
            {
              Selector.Evaluate();
            }
            Name.Evaluate();
            Effect effect = Context.Instance.Effects[(string)Name.Value];
            effect.Evaluate();
            if(PostAction !=null)
            {
                PostAction.Parent = this;
                PostAction.Evaluate();
            }              
        }
        public override bool CheckSemantic(Context Context, List<CompilingError> Errors , Scope scope)
        {
            if (Name == null)
            {
                return false;
            }
            if (!Context.Effects.ContainsKey((string)Name.Value))
            {
                Errors.Add(new CompilingError(Position, ErrorCode.Invalid, "The effect is not declared"));
                return false;
            }
            foreach(Expression param in Params)
            {
                //Here we verify if the type of the param is the declarated in the effect
                
            }
            return true;  
        }
        public DeclaredEffect()
        {
            Params = new List<Expression>();
        }
    }
}