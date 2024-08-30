using System.Collections;
using System.Collections.Generic;
using Compiler;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardOutput : MonoBehaviour
{
    public UnityCard Card;
    public Image Picture;
    public UnityEngine.UI.Text Nametext;
    public UnityEngine.UI.Text Description;
    public UnityEngine.UI.Text Power;
    public double PowerValue;
    public Image Type;
    public Image Range;
    public bool OnHand;

    public void SetValues(UnityCard card)
    {
        Card = card;
        Range.sprite = Card.Type == "Oro" ? Resources.Load<Sprite>("Pictures/Cards/gold") : Card.Type == "Silver" ? Resources.Load<Sprite>("Pictures/Cards/silver") : null;
        Type.sprite = Resources.Load<Sprite>("Pictures/Cards/melee");
        Nametext.text = Card.Name;
        Description.text = "";
        PowerValue = Card.Power;
        Power.text = PowerValue.ToString();
    }
}
