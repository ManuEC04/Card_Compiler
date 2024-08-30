using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Row : MonoBehaviour
{
    [SerializeField] public string Rowtag;
    public List<GameObject> Cards;
    public void OnTriggerEnter2D(Collider2D other) 
    {
        UnityCard card = other.GetComponent<CardOutput>().Card;
        foreach(string range in card.Range)
        {
            if(range.Equals(Rowtag))
            {
                Cards.Add(other.gameObject);
                other.transform.SetParent(gameObject.transform , true);
                other.GetComponent<DragAndDrop>().IsOverDropZone = true;
                other.GetComponent<CardOutput>().OnHand = false;
                GameZone gamezone = gameObject.GetComponentInParent<GameZone>();
                gamezone.GetComponentInParent<Player>().Turn.StartTurn = false;
                gamezone.GetComponentInParent<Player>().Turn.PlayMade = true;
                gamezone.GetComponentInParent<Player>().Turn.EndTurn = true;
            }
        }
    }
}
