using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace Compiler
{
    public class FunctionContainer
    {

        public GameObject Pop(List<GameObject> List)
        {
            GameObject temp = List[0];
            List.RemoveAt(0);
            return temp;
        }
        public void Shuffle(List<GameObject> list)
        {
            System.Random rand = new System.Random();
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);
                GameObject temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
        public void Remove(List<GameObject> list, GameObject value)
        {
            Context context = Context.Instance;
            if (context.Selector != null)
            {
                ICardContainer Container = context.Selector.GetComponent<ICardContainer>();
                Container.RemoveCard(value);
            }
            list.Remove(value);
        }
        public void SendBottom(List<GameObject> list, GameObject value)
        {
            Context context = Context.Instance;
            if (context.Selector != null)
            {
                ICardContainer Container = context.Selector.GetComponent<ICardContainer>();
                Container.RemoveCard(value);
            }
            list.Add(value);
        }
        public void Push(List<GameObject> list, GameObject value)
        {
            Context context = Context.Instance;
            int index = list.Count - 1;
            list.Add(value);
            GameObject temp = list[0];
            list[0] = list[index];
            list[index] = temp;
            context.Selector.GetComponent<ICardContainer>().RemoveCard(value);
            value.transform.SetParent(context.Selector.transform, false);
        }
        public List<GameObject> Find(Predicate predicate, List<GameObject> list)
        {
            List<GameObject> returnlist = new List<GameObject>();

            foreach (GameObject value in list)
            {
                if (predicate.VerifyPredicate(value.GetComponent<CardOutput>().Card))
                {
                    returnlist.Add(value);
                }
            }
            foreach(GameObject value in returnlist)
            {
                Debug.Log(value.GetComponent<CardOutput>().Card.Name);
            }
            return returnlist;
        }


    }
}
