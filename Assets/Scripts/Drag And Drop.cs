using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour , IDragHandler, IDropHandler
{
  public bool IsOverDropZone {get; set;}
  Canvas canvas;
  RectTransform rectTransform;

  void Start() 
  {
   rectTransform = GetComponent<RectTransform>();
   canvas = GetComponentInParent<Canvas>();
  }
  public void OnDrag(PointerEventData eventData)
  {
     if(!IsOverDropZone && gameObject.GetComponent<CardOutput>().OnHand)
     {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
     }
     else{return;}
  }
  public void OnDrop(PointerEventData eventData)
  {
     if(!IsOverDropZone)
     {
        Hand hand = GetComponentInParent<Hand>();
        GameObject canvas = GameObject.Find("Canvas");
        gameObject.transform.SetParent(canvas.transform , false); // Here we change the parent of the card to reset de grid component in the hand
        gameObject.transform.SetParent(hand.transform , true);
     }
  }
}
