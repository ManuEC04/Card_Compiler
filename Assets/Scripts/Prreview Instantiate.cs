using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Compiler;
using UnityEditor;
using UnityEngine;

public class PrreviewInstantiate : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    CardDatabase Database;
    void Start()
    {
        Debug.Log("Estamos en el start");
        Database = CardDatabase.Instance;
        Debug.Log(Database.Cards.Count);
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Card");

        if (prefab != null)
        {
            Debug.Log("El prefab no es null");
        }
    }

    // Update is called once per frame
    public void Instantiate()
    {
        if(Database.Cards.Count > 0)
        {
            UnityEngine.Vector2 Position = new UnityEngine.Vector2 (0,0);
            Debug.Log(Database.Cards[0].Name);
            UnityCard card = Database.Cards[0];
            prefab.GetComponent<RectTransform>().sizeDelta = new UnityEngine.Vector2(10, 10);
           prefab.GetComponent<CardOutput>().SetValues(card);
           
        }
        else
        {
            Debug.Log("No hay cartas en la database");
        }
 
    }
}
