
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Compiler;

public interface ICardContainer
{
    public List<GameObject> GetCardList();
    public void RemoveCard(GameObject value);

}
