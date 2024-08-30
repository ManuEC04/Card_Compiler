using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
   public List<GameObject> PlayerHand;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckHandCount()
    {
        if(PlayerHand.Count > 9)
        {
            //Here we send the cards to the graveyard
        }
    }
}
