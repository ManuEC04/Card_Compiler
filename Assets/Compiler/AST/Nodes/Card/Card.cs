using System;
using System.Collections.Generic;
namespace Compiler
{
    using System.Collections.Generic;
    using UnityEngine;
    public class Card : ASTNode
    {
        public Expression? Type { get; set; }
        public Expression? Name { get; set; }
        public Expression? Faction { get; set; }
        public Expression? Power { get; set; }
        public List<Expression>? Range { get; set; }
        public OnActivation OnActivation { get; set; }
        public override bool CheckSemantic(Context Context, List<CompilingError> Errors, Scope scope)
        {
            if (Type == null || Name == null || Faction == null || Power == null || Range == null)
            {
                return false;
            }
            if (Context.Cards.ContainsKey((string)Name.Value))
            {

                Errors.Add(new CompilingError(Position, ErrorCode.Invalid, "This Card is already declared"));
                return false;
            }
            if (Power.Type != ExpressionType.Number)
            {
                Console.WriteLine("El poder no es numerico");
                Errors.Add(new CompilingError(Position, ErrorCode.Invalid, "The Power must be numerical"));
                return false;
            }
            if (!((string)Type.Value == "Oro") && !((string)Type.Value == "Plata"))
            {
                Errors.Add(new CompilingError(Position, ErrorCode.Invalid, "Invalid Type"));
                return false;
            }
            if (Range.Count == 1)
            {
                if (!((string)Range[0].Value == "FactionLeader" || (string)Range[0].Value == "Weather" || (string)Range[0].Value == "Increase"
                || (string)Range[0].Value == "Decoy" || (string)Range[0].Value == "Melee" || (string)Range[0].Value == "Ranged" ||
                (string)Range[0].Value == "Siege"))
                {
                    Errors.Add(new CompilingError(Position, ErrorCode.Invalid, "Invalid Range"));
                    return false;
                }
            }
            else
            {
                foreach (Expression expr in Range)
                {
                    if ((string)expr.Value == "Melee" || (string)expr.Value == "Ranged" || (string)expr.Value == "Siege")
                    {
                        continue;
                    }
                    else
                    {
                        Errors.Add(new CompilingError(Position, ErrorCode.Invalid, "Invalid Range"));
                        return false;
                    }
                }
            }
            Context.Cards.Add((string)Name.Value, this);
            return true;
        }
        public override void Evaluate()
        {
            if (Power != null)
            {
                Power.Evaluate();
            }
            List<string> RangeValues = new List<string>();
            foreach (Expression expr in Range)
            {
                RangeValues.Add((string)expr.Value);
            }
            Debug.Log(OnActivation.Effects[0].Selector.Single.Value);
            new UnityCard((string)Type.Value, (string)Name.Value, (string)Faction.Value, (double)Power.Value, RangeValues.ToArray(), Resources.Load<Sprite>("default"), OnActivation);
        }
        public Card(int Position)
        {
            OnActivation = new OnActivation();
        }
    }

}
