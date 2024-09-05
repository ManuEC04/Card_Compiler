using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]GameZone player1zone;
    [SerializeField]GameZone player2zone;
    public GameZone Player1Zone {get {return player1zone;}}
    public GameZone Player2Zone {get{return player2zone;}}

    GameZone[] PlayersZones;

    void Start()
    {
        PlayersZones = new GameZone[2];
        PlayersZones[0] = player1zone;
        PlayersZones[1] = player2zone;
    }


}
