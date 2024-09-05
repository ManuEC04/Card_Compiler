using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public bool StartTurn;
    public bool DrawExecuted;
    public bool PlayMade;
    public bool Passed;
    public bool EndTurn;
    

    public void StartMyTurn()
    {
        StartTurn = true;
        DrawExecuted = false;
        PlayMade = false;
        Passed = false;
        EndTurn = false;
    }
    public void PassTurn()
    {
          StartTurn = false;
          EndTurn = true;
    }
}
