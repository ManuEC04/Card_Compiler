using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class SelectCard : MonoBehaviour , IPointerClickHandler
{
    CardDatabase cards;

   public void OnPointerClick(PointerEventData eventData)
   {
    GameObject Right = GameObject.Find("Right");
    cards = CardDatabase.Instance;
    cards.PlayerDeck.Add(gameObject.GetComponent<CardOutput>().Card);
    gameObject.transform.SetParent(Right.transform , true);
   }

}
