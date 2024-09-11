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
        if (Card.OnActivation != null)
        {

            Card.OnActivation.Evaluate();
            Game game = Game.Instance;
            game.Player1.GetComponent<Player>().GetComponentInChildren<GameZone>().UpdatePowerCounter();
            game.Player2.GetComponent<Player>().GetComponentInChildren<GameZone>().UpdatePowerCounter();

        }
    }
    public void ActivateEffect()
    {
        card.Effect.DynamicInvoke();
    }
}
