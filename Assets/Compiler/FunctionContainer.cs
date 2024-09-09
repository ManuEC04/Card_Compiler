using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace Compiler
{
    public class FunctionContainer
    {
        public Dictionary<string, Delegate> Functions = new Dictionary<string, Delegate> { };
        Context context = Context.Instance;
        public GameObject Pop(List<GameObject> List)
        {
            GameObject temp = List[0];
            List.RemoveAt(0);
            return temp;
        }
        public void Shuffle<T>(List<T> list)
        {
           
        }
        public void Remove<T>(List<T> list, T value)
        {
            list.Remove(value);
        }
        public void SendBottom<T>(List<T> list, T value)
        {
            list.Add(value);
        }
        public void Push<T>(List<T> list, T value)
        {
            T temp = list[0];
            int index = list.Count - 1;
            list.Add(value);
            list[0] = list[index];
            list[index] = value;
        }
        public List<T> Find<T>(Predicate predicate, List<T> list)
        {
            List<T> returnlist = new List<T>();

            foreach (T value in list)
            {
                predicate.Comparation.Evaluate();
                if (predicate.Comparation.Right.Value.Equals(value))
                {
                    returnlist.Add(value);
                }
            }
            return returnlist;
        }


    }
}
