using System;
using System.Collections.Generic;
using UnityEngine;
namespace Compiler
{
    public class DeclaredEffect : ASTNode
    {
        public Expression Name { get; set; }
        public List<Declaration> Params { get; set; }
        public Selector? Selector { get; set; }
        public PostAction? PostAction { get; set; }


        public override void Evaluate()
        {
            Context context = Context.Instance;
            foreach(Declaration param in Params)
            {
                context.scope.Declaration[(string)param.Identifier.Value] = param.Expression;
            }
            if (Selector != null)
            {
                Selector.Evaluate();
            }
            Name.Evaluate();
            Effect effect = Context.Instance.Effects[(string)Name.Value];
            effect.Evaluate();
            if (PostAction != null)
            {
                PostAction.Parent = this;
                PostAction.Evaluate();
            }
        }
        public override bool CheckSemantic(Context Context, List<CompilingError> Errors, Scope scope)
        {
            if (Name == null)
            {
                return false;
            }
            if (!Context.Effects.ContainsKey((string)Name.Value)) // Here we verify if there is a param declared with the given key
            {
                Errors.Add(new CompilingError(Position, ErrorCode.Invalid, "The effect is not declared"));
                return false;
            }
            Context context = Context.Instance;
            foreach (Declaration param in Params)
            {

                if (!context.scope.Declaration.ContainsKey((string)param.Identifier.Value))
                {
                    Errors.Add(new CompilingError(Position, ErrorCode.Invalid, "The param is not already declared"));
                    return false;
                }
                else
                {
                    Expression expr = context.scope.Declaration[(string)param.Identifier.Value]; 
                    if (expr.Equals("Number") && param.Expression.Type != ExpressionType.Number)
                    {
                        Errors.Add(new CompilingError(Position, ErrorCode.Invalid, "The param doesn't have the type declared"));
                        return false;
                    }
                    else if (expr.Equals("Text") && param.Expression.Type != ExpressionType.Text)
                    {
                        Errors.Add(new CompilingError(Position, ErrorCode.Invalid, "The param doesn't have the type declared"));
                        return false;
                    }
                    else if (expr.Equals("Boolean") && param.Expression.Type != ExpressionType.Boolean)
                    {
                        Errors.Add(new CompilingError(Position, ErrorCode.Invalid, "The param doesn't have the type declared"));
                        return false;
                    }
                }
                context.scope.Declaration[(string)param.Identifier.Value] = param.Expression; // Here we assing the param expression declared in the onactivation of the card to the action body of the effect
            }

            return true;
        }
        public DeclaredEffect()
        {
            Params = new List<Declaration>();
        }
    }
}