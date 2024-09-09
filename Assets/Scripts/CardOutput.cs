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
        UnityEngine.Debug.Log("OnACtivationON");

        Player player = gameObject.GetComponentInParent<Row>().GetComponentInParent<GameZone>().GetComponentInParent<Player>();

        if (Card.OnActivation != null)
        {
           UnityEngine.Debug.Log("OnActivation no es null");
           UnityEngine.Debug.Log(Card.OnActivation.Effects.Count);
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
                UnityEngine.Debug.Log("Aqui tengo el effect");
                Effect myeffect = Context.Instance.Effects[(string)effect.Name.Value];

                if(myeffect !=null)
                {
                    UnityEngine.Debug.Log("NO DEVOLVIO NULL");
                }
                string identifier = (string)effect.Selector.Source.Value;
                switch (identifier)
                {
                    case "board":
                        context.targets = effect.Selector.SelectTargets(player.Field.Melee.GetCardList());
                        List<GameObject> mylist = effect.Selector.SelectTargets(player.Field.Ranged.GetCardList());
                        Copy(context.targets, mylist);
                        List<GameObject> mylist2 = effect.Selector.SelectTargets(player.Field.Siege.GetCardList());
                        Copy(context.targets, mylist2);
                        List<GameObject> mylist3 = effect.Selector.SelectTargets(player.Otherfield.Ranged.GetCardList());
                        Copy(context.targets, mylist3);
                        List<GameObject> mylist4 = effect.Selector.SelectTargets(player.Otherfield.Siege.GetCardList());
                        Copy(context.targets, mylist4);
                        List<GameObject> mylist5 = effect.Selector.SelectTargets(player.Otherfield.Melee.GetCardList());
                        Copy(context.targets, mylist5);

                        break;
                    case "hand":
                        context.targets = effect.Selector.SelectTargets(player.Hand.GetCardList());
                        break;
                    case "otherhand":
                        context.targets = effect.Selector.SelectTargets(player.Otherhand.GetCardList());
                        break;
                    case "deck":
                        context.targets = effect.Selector.SelectTargets(player.Deck.GetCardList());
                        break;
                    case "otherdeck":
                        context.targets = effect.Selector.SelectTargets(player.Otherdeck.GetCardList());
                        break;
                    case "field":
                        context.targets = effect.Selector.SelectTargets(player.Field.Melee.GetCardList());
                        List<GameObject> list = effect.Selector.SelectTargets(player.Field.Ranged.GetCardList());
                        Copy(context.targets, list);
                        List<GameObject> list2 = effect.Selector.SelectTargets(player.Field.Siege.GetCardList());
                        Copy(context.targets, list2);
                        break;
                    case "otherfield":
                        context.targets = effect.Selector.SelectTargets(player.Otherfield.Melee.GetCardList());
                        List<GameObject> list3 = effect.Selector.SelectTargets(player.Otherfield.Ranged.GetCardList());
                        Copy(context.targets, list3);
                        List<GameObject> list4 = effect.Selector.SelectTargets(player.Otherfield.Siege.GetCardList());
                        Copy(context.targets, list4);
                        break;
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
        UnityEngine.Debug.Log("Se activa el efecto");
    }
    void Copy(List<GameObject> list1, List<GameObject> list2)
    {
        for (int i = 0; i < list2.Count; i++)
        {
            list1.Add(list2[i]);
        }
    }

}
