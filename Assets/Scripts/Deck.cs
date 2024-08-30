using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<GameObject> PlayerDeck;
    [SerializeField]private GameObject prefab;


    void Awake()
    {
        CardDatabase Data = CardDatabase.Instance;
        Data.CreateNordickDeck();
        UnityEngine.Vector3 Position = new UnityEngine.Vector3(0,0,0);
        foreach(UnityCard unityCard in Data.NordickDeck)
        {
            GameObject card = Instantiate(prefab , Position , UnityEngine.Quaternion.identity);
            card.GetComponent<RectTransform>().localScale = new UnityEngine.Vector2(0.16f , 0.16f);
            card.GetComponent<CardOutput>().SetValues(unityCard);
            card.transform.SetParent(gameObject.transform,true);
            PlayerDeck.Add(card);
        }
        
        
    }
}
