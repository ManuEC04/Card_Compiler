using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Compiler
{
    public class Context
    {
        public List<CompilingError> Errors {get;set;}
        public Scope scope {get;set;}
        public Dictionary<string, Card> Cards { get; set; }
        public Dictionary<string, Effect> Effects { get; set; }
        public List<GameObject> targets {get;set;}
        public GameObject Selector {get;set;}
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
            Errors = new List<CompilingError>();
            targets = new List<GameObject>();
            Cards = new Dictionary<string, Card>();
            Effects = new Dictionary<string, Effect>();
        }
        public Player GetPlayer(string id)
        {
            Game game = Game.Instance;
            return game.Players[id];
        }
        //Some Methods of the context
        public List<GameObject> HandOfPlayer(string id)
        {
            Game game = Game.Instance;
            Player player = game.Players[id];
            return player.Hand.GetCardList();
        }
        public List<GameObject> DeckOfPlayer(string id)
        {
            Game game = Game.Instance;
            Player player = game.Players[id];
            return player.Deck.GetCardList();
        }
        public List<GameObject> GraveyardOfPlayer(string id)
        {
            Game game = Game.Instance;
            Player player = game.Players[id];
            return player.Graveyard.GetCardList();
        }
        public List<GameObject> FieldOfPlayer(string id)
        {
            Game game = Game.Instance;
            Player player = game.Players[id];
            return player.Field.GetCardList();
        }
        public string TriggerPlayer()
        {
            Game MyGame = Game.Instance;
            if (MyGame.Player1.GetComponent<Player>().GetComponentInChildren<Turn>().StartTurn)
            {
                return MyGame.Player1.GetComponent<Player>().Id;
            }
            else if(MyGame.Player2.GetComponent<Player>().GetComponentInChildren<Turn>().StartTurn)
            {
                return MyGame.Player2.GetComponent<Player>().GetComponent<Player>().Id;
            }
            throw new Exception("No es el turno de ningunp de los dos jugadores");
        }

    }
}