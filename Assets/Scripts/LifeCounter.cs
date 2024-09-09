using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    [SerializeField]GameObject Life1;
    [SerializeField]GameObject Life2;
    public int Life {get;set;}
    void Start()
    {
        Life = 2;
    }
    public void Destroylife()
    {
        if(Life1 !=null)
        {
            Destroy(Life1);
        }
        else if (Life2 !=null)
        {
            Destroy(Life2);
        }
    }
}
