using System.Collections.Generic;
using UnityEngine;
namespace Compiler
{
    public class Context : MonoBehaviour
    {
        public Dictionary<string, Card> Cards { get; set; }
        public Dictionary<string, Effect> Effects { get; set; }
        static Context instance;
        static bool created;
        public static Context Instance
        {
            get
            {
                if (!created)
                {
                    instance = new Context();
                }
                return instance;
            }
        }
        private Context()
        {
            Cards = new Dictionary<string, Card>();
            Effects = new Dictionary<string, Effect>();
        }


        //Link with Unity
        [SerializeField] Player player1;
        [SerializeField] Player player2;
        [SerializeField] Board board;
        [SerializeField]Animator animator;
         void Start()
         {
            animator = GetComponent<Animator>();
         }
        void Update()
        {
           CheckTurn();
           animator.SetBool("StartTurn" , player1.Turn.StartTurn);
           animator.SetBool("EndTurn", player1.Turn.EndTurn);
        }
        void PassTurn(Player player1, Player player2)
        {
            if (player1.Turn.PlayMade && !player2.Turn.Passed)
            {
                player1.Turn.StartTurn = false;
                player1.Turn.EndTurn = true;
                player2.Turn.StartTurn = true;
                player1.Turn.DrawExecuted = false;
            }
        }
        void CheckTurn()
        {
            PassTurn(player1, player2);
            PassTurn(player2, player1);
        }
    }
}