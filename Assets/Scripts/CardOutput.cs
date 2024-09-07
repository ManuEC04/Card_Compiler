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
    public double PowerValue { get { return powervalue; } set{powervalue = value;}}
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
    public void ActivateEffect()
    {

        Player player = gameObject.GetComponentInParent<Row>().GetComponentInParent<GameZone>().GetComponentInParent<Player>();

        if(Card.OnActivation !=null)
        {

        foreach (DeclaredEffect effect in Card.OnActivation.Effects)
        {
            effect.Evaluate();
            if (effect.Selector == null)
            {
                return;
            }
            Context context = Context.Instance;
            string identifier = (string)effect.Selector.Source.Value;
            switch (identifier)
            {
                case "board":
                    effect.Selector.SelectTargets(player.Field.Melee.GetCardList());
                    effect.Selector.SelectTargets(player.Field.Ranged.GetCardList());
                    effect.Selector.SelectTargets(player.Field.Siege.GetCardList());
                    effect.Selector.SelectTargets(player.Otherfield.Melee.GetCardList());
                    effect.Selector.SelectTargets(player.Otherfield.Ranged.GetCardList());
                    effect.Selector.SelectTargets(player.Otherfield.Siege.GetCardList());
                    break;
                case "hand":
                    Debug.Log("Coge q es la mano");
                    context.targets = effect.Selector.SelectTargets(player.Hand.GetCardList());
                    UnityEngine.Debug.Log(effect.Name.Value);
                    UnityEngine.Debug.Log(Context.Instance.Effects.Count);
                    Effect myeffect = Context.Instance.Effects[(string)effect.Name.Value];;
                    myeffect.Evaluate();
                    break;
                case "otherhand":
                    context.targets = effect.Selector.SelectTargets(player.Otherhand.GetCardList());
                    break;
                case "deck":
                    context.targets = effect.Selector.SelectTargets(player.Deck.GetCardList());
                    break;
                case "otherdeck":
                   context.targets =  effect.Selector.SelectTargets(player.Otherdeck.GetCardList());
                    break;
                case "field":
                    effect.Selector.SelectTargets(player.Field.Melee.GetCardList());
                    effect.Selector.SelectTargets(player.Field.Ranged.GetCardList());
                    effect.Selector.SelectTargets(player.Field.Siege.GetCardList());
                    break;
                case "otherfield":
                    effect.Selector.SelectTargets(player.Otherfield.Melee.GetCardList());
                    effect.Selector.SelectTargets(player.Otherfield.Ranged.GetCardList());
                    effect.Selector.SelectTargets(player.Otherfield.Siege.GetCardList());
                    break;
            }

        }
         }
    }

}
