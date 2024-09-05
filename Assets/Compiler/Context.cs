using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Compiler
{
    public class Context
    {
        Game game = Game.Instance;
        public Game MyGame { get { return game; }}
        public Scope scope {get;set;}
        public Dictionary<string, Card> Cards { get; set; }
        public Dictionary<string, Effect> Effects { get; set; }
        public List<GameObject> targets {get;set;}
        static Context instance;
        static bool created;
        public static Context Instance
        {
            get
            {
                if (!created)
                {
                    instance = new Context();
                    created = true;
                }
                return instance;
            }
        }
        private Context()
        {
            scope = new Scope();
            targets = new List<GameObject>();
            Cards = new Dictionary<string, Card>();
            Effects = new Dictionary<string, Effect>();
        }
        public Player GetPlayer(string id)
        {
            return game.Players[id];
        }
        //Some Methods of the context
        public List<GameObject> HandOfPlayer(string id)
        {
            Player player = game.Players[id];
            return player.Hand.GetCardList();
        }
        public List<GameObject> DeckOfPlayer(string id)
        {
            Player player = game.Players[id];
            return player.Deck.GetCardList();
        }
        public string TriggerPlayer()
        {
            if (MyGame.Player1.GetComponentInChildren<Turn>().StartTurn)
            {
                return MyGame.Player1.GetComponent<Player>().Id;
            }
            return MyGame.Player2.GetComponent<Player>().Id;
        }

    }
}