using System.Collections.Generic;
using UnityEngine;
namespace Compiler
{
    public class Selector : ASTNode
    {
        public Expression Source { get; set; }
        public Expression Single { get; set; }
        public Predicate Predicate { get; set; }


        public override void Evaluate()
        {
            Context context = Context.Instance;
            string id = context.TriggerPlayer();
            Player player = context.GetPlayer(id);

                 switch (Source.Value)
                {
                    case "board":
                    context.Selector = player.Board.gameObject;
                        context.targets = SelectTargets(player.Board.GetCardList());
                        break;
                    case "hand":
                    context.Selector = player.Hand.gameObject;
                        context.targets = SelectTargets(player.Hand.GetCardList());
                        break;
                    case "otherhand":
                    context.Selector = player.Otherhand.gameObject;
                        context.targets = SelectTargets(player.Otherhand.GetCardList());
                        break;
                    case "deck":
                    context.Selector = player.Deck.gameObject;
                        context.targets = SelectTargets(player.Deck.GetCardList());
                        break;
                    case "otherdeck":
                    context.Selector = player.Otherdeck.gameObject;
                        context.targets = SelectTargets(player.Otherdeck.GetCardList());
                        break;
                    case "field":
                    context.Selector = player.Field.gameObject;
                        context.targets = SelectTargets(player.Field.GetCardList());
                        break;
                    case "otherfield":
                    context.Selector = player.Otherfield.gameObject;
                        context.targets = SelectTargets(player.Otherfield.GetCardList());
                        break;
                        case"graveyard":
                         context.Selector = player.Graveyard.gameObject;
                         context.targets = SelectTargets(player.Graveyard.GetCardList());
                         break;
                        case"othergraveyard":
                         context.Selector = player.OtherGraveyard.gameObject;
                         context.targets = SelectTargets(player.OtherGraveyard.GetCardList());break;
                }
            }
        public List<GameObject> SelectTargets(List<GameObject> List)
        {
            Predicate.Comparation.Right.Evaluate();
            List<GameObject> cards = new List<GameObject>();
            Single.Evaluate();
            if(Single.Value.Equals("true"))
            {
                  foreach (GameObject card in List)
                {
                    if (Predicate.VerifyPredicate(card.GetComponent<CardOutput>().Card))
                    {
                        cards.Add(card);
                        break;
                    }
                }
                return cards;
            }
            else
            {
                 foreach (GameObject card in List)
                {
                    if (Predicate.VerifyPredicate(card.GetComponent<CardOutput>().Card))
                    {
                        cards.Add(card);
                    }
                }
                return cards;
            }
                
           
        }
         public string GetSource()
            {
                return (string)Source.Value;
            }

        public override bool CheckSemantic(Context context, List<CompilingError> errors, Scope scope)
        {
            if (Source == null || Single == null || Predicate == null)
            {
                return false;
            }
            if (!((string)Source.Value == "parent") && !((string)Source.Value == "board") && !((string)Source.Value == "hand")
             && !((string)Source.Value == "otherhand") && !((string)Source.Value == "deck") && !((string)Source.Value == "otherdeck")
             && !((string)Source.Value == "field") && !((string)Source.Value == "otherfield"))
            {
                errors.Add(new CompilingError(Position, ErrorCode.Invalid, "Yo must declare a valid Source"));
                return false;
            }
            if (Single.Type != ExpressionType.Boolean)
            {
                errors.Add(new CompilingError(Position, ErrorCode.Invalid, "Yo must declare a valid Single"));
                return false;
            }

            return true && Predicate.CheckSemantic(context, errors, scope);
        }
        public Selector()
        {
            Source = null;
            Single = null;
            Predicate = null;
        }

    }
}