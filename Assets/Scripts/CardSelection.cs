using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardSelection : MonoBehaviour
{
    CardDatabase Data;
    [SerializeField]GameObject prefab;

    void Start()
    {
        Data = CardDatabase.Instance;

        prefab = Resources.Load<GameObject>("Prefabs/Card");
        UnityEngine.Vector3 Position = new UnityEngine.Vector3(0,0,0);
        Data.CreateNordickDeck();
        foreach(UnityCard unityCard in Data.Cards)
        {
            GameObject card = Instantiate(prefab , Position , UnityEngine.Quaternion.identity , gameObject.transform);
            SelectCard selectCard = card.AddComponent<SelectCard>();
            
            card.GetComponent<RectTransform>().localScale = new UnityEngine.Vector2(0.30f , 0.30f);
            card.GetComponent<CardOutput>().SetValues(unityCard);
        }
    }
    public void Next()
    {
        if(Data.PlayerDeck.Count >= 24)
        {
            SceneManager.LoadScene("Main");
        }
        else 
        {
           Debug.Log("Su deck debe tener minimo 25 cartas");
        }
        
    }
    
    
}

