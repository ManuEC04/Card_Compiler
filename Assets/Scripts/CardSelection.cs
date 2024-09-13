using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            if( CheckLeader(Data.PlayerDeck))
            {
                SceneManager.LoadScene("Main");
            }
            
        }
        else 
        {
           Debug.Log("Su deck debe tener minimo 25 cartas");
        }
        
    }
    public bool CheckLeader(List<UnityCard> cards)
    {
        int count = 0;
        foreach(UnityCard card in cards)
        {
           if(card.Range[0] == "FactionLeader")
           {
            count++;
           }
        }
        if(count > 1)
        {
            Debug.Log("Solo puede tener un lider en el deck");
            return false;
        }
        if(count == 0)
        {
            UnityEngine.Debug.Log("Debe tener una carta lider en el mazo");
            return false;
        }
        return true;
    }
    
    
}

