namespace Compiler
{
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
            if (!((string)Type.Value == "Oro") || !((string)Type.Value == "Silver"))
            {
                Errors.Add(new CompilingError(Position , ErrorCode.Invalid , "Invalid Type"));
                return false;
            }
            foreach (Expression expr in Range)
            {
                if ((string)expr.Value == "Melee" || (string)expr.Value == "Ranged" || (string)expr.Value == "Siege")
                {
                    continue;
                }
                else
                {
                    Errors.Add(new CompilingError(Position , ErrorCode.Invalid , "Invalid Range"));
                    return false;
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
            OnActivation.Evaluate();
        }
        public Card()
        {
            OnActivation = new OnActivation();
        }
    }

}
