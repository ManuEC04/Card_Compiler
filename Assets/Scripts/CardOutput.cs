using System.Collections;
using System.Collections.Generic;
using Compiler;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardOutput : MonoBehaviour
{
    [SerializeField] UnityCard card;
    public UnityCard Card { get { return card; } }
    string playerid = "";
    public string PlayerId { get { return playerid; } set { playerid = value; } }
    [SerializeField] Image Picture;
    [SerializeField] UnityEngine.UI.Text Nametext;
    [SerializeField] UnityEngine.UI.Text Description;
    [SerializeField] UnityEngine.UI.Text Power;
    double powervalue;
    public double PowerValue
    {
        get { return powervalue; }
        set
        {
            if (PowerValue < 0)
            {
                powervalue = 0;
            }
            else { powervalue = value; }
        }
    }
    [SerializeField] Image Type;
    [SerializeField] Image Range;
    public bool OnHand { get; set; }
    bool setdone;
    public void SetValues(UnityCard Set)
    {
        card = Set;
        Picture.sprite = Set.Picture;
        Range.sprite = card.Type == "Oro" ? Resources.Load<Sprite>("Pictures/Cards/gold") : card.Type == "Silver" ? Resources.Load<Sprite>("Pictures/Cards/silver") : null;
        Type.sprite = Resources.Load<Sprite>("Pictures/Cards/melee");
        Nametext.text = card.Name;
        Description.text = "";
        powervalue = card.Power;
        Power.text = PowerValue.ToString();
    }
    public void UpdateProperties()
    {
        Power.text = PowerValue.ToString();
    }
    public void ResetPower()
    {
        PowerValue = Card.Power;
        Power.text = PowerValue.ToString();
    }
    public void ActivateOnActivation()
    {
        Player player = gameObject.GetComponentInParent<Row>().GetComponentInParent<GameZone>().GetComponentInParent<Player>();
        if (Card.OnActivation != null)
        {
            foreach (DeclaredEffect effect in Card.OnActivation.Effects)
            {
                effect.Evaluate();
                if (effect.Selector == null)
                {
                    Effect declaredeffect = Context.Instance.Effects[(string)effect.Name.Value];
                    declaredeffect.Evaluate();
                    return;
                }
                Context context = Context.Instance;
                Effect myeffect = Context.Instance.Effects[(string)effect.Name.Value];
                string identifier = (string)effect.Selector.Source.Value;
                switch (identifier)
                {
                    case "board":
                    context.Selector = player.Board.gameObject;
                        context.targets = effect.Selector.SelectTargets(player.Board.GetCardList());
                        break;
                    case "hand":
                    context.Selector = player.Hand.gameObject;
                        context.targets = effect.Selector.SelectTargets(player.Hand.GetCardList());
                        break;
                    case "otherhand":
                    context.Selector = player.Otherhand.gameObject;
                        context.targets = effect.Selector.SelectTargets(player.Otherhand.GetCardList());
                        break;
                    case "deck":
                    context.Selector = player.Deck.gameObject;
                        context.targets = effect.Selector.SelectTargets(player.Deck.GetCardList());
                        break;
                    case "otherdeck":
                    context.Selector = player.Otherdeck.gameObject;
                        context.targets = effect.Selector.SelectTargets(player.Otherdeck.GetCardList());
                        break;
                    case "field":
                    context.Selector = player.Field.gameObject;
                        context.targets = effect.Selector.SelectTargets(player.Field.GetCardList());
                        break;
                    case "otherfield":
                    context.Selector = player.Otherfield.gameObject;
                        context.targets = effect.Selector.SelectTargets(player.Otherfield.GetCardList());
                        break;
                        case"graveyard":break;
                        case"othergraveyard":break;
                }
                myeffect.Evaluate();
                Game game = Game.Instance;
                game.Player1.GetComponent<Player>().GetComponentInChildren<GameZone>().UpdatePowerCounter();
                game.Player2.GetComponent<Player>().GetComponentInChildren<GameZone>().UpdatePowerCounter();

            }
        }
    }
    public void ActivateEffect()
    {
        card.Effect.DynamicInvoke();
    }
}
