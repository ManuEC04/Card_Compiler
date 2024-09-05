using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Compiler;
using UnityEditor;

public class CardDatabase 
{
    public List<UnityCard> Cards { get; set; }
    static CardDatabase instance;
    static bool Created;
    public List<UnityCard> PlayerDeck {get;set;}
    public static CardDatabase Instance
    {
        get
        {
            if (!Created)
            {
                instance = new CardDatabase();
                Created = true;
            }
            
            return instance;
        }
    }
    private CardDatabase()
    {
        Cards = new List<UnityCard>();
        PlayerDeck = new List<UnityCard>();
    }


    //MYDECKS

    private List<UnityCard> nordickdeck = new List<UnityCard>();
    public void CreateNordickDeck()
    {
    Cards.Add(new UnityCard("Plata","Vali","Nordics" , 5 , new string[]{"Melee" , "Ranged"} , Resources.Load<Sprite>("Vali")));
    Cards.Add(new UnityCard("Plata","Vali","Nordics" , 5 , new string[]{"Melee" , "Ranged"} , Resources.Load<Sprite>("Vali")));
    Cards.Add(new UnityCard("Oro","Thor","Nordics" , 7 , new string[]{"Melee"} , Resources.Load<Sprite>("Thor")));
    Cards.Add(new UnityCard("Plata","Señuelo de Loki","Nordics" , 0 , new string[]{"Melee" , "Ranged" , "Siege"} , Resources.Load<Sprite>("Lokislure")));
    Cards.Add(new UnityCard("Oro","Odin","Nordics" , 5 , new string[]{"FactionLeader"} , Resources.Load<Sprite>("Odin")));
    Cards.Add(new UnityCard("Plata","Mimir","Nordics" , 3 , new string[]{"Ranged"} , Resources.Load<Sprite>("Mimir")));
    Cards.Add(new UnityCard("Oro","Loki","Nordics" , 6 , new string[]{"Ranged"} , Resources.Load<Sprite>("Loki")));
    Cards.Add(new UnityCard("Gold","Catapulta de Odin","Nordics" , 9 , new string[]{"Siege"} , Resources.Load<Sprite>("catapultadeodin")));
    Cards.Add(new UnityCard("Plata","Hela","Nordics" , 4 , new string[]{"Ranged"} , Resources.Load<Sprite>("Hela")));
    Cards.Add(new UnityCard("Oro","","Heimdall" , 5 , new string[]{"Ranged","Melee"} , Resources.Load<Sprite>("Heimdall")));
    Cards.Add(new UnityCard("Oro","Loki","Nordics" , 6 , new string[]{"Ranged"} , Resources.Load<Sprite>("Loki")));
    Cards.Add(new UnityCard("Oro","Gungnir","Nordics" , 0 , new string[]{"Increase"} , Resources.Load<Sprite>("Odinsweapon")));
    Cards.Add(new UnityCard("Oro","Gajallahorn","Nordics" , 0 , new string[]{"Increase"} , Resources.Load<Sprite>("Gajallahorn")));
    Cards.Add(new UnityCard("Plata","Frost","Nordics" , 6 , new string[]{"Siege"} , Resources.Load<Sprite>("Frost")));
    Cards.Add(new UnityCard("Plata","Frost","Nordics" , 6 , new string[]{"Siege"} , Resources.Load<Sprite>("Frost")));
    Cards.Add(new UnityCard("Plata","Freya","Nordics" , 6 , new string[]{"Ranged"} , Resources.Load<Sprite>("Freya")));
    Cards.Add(new UnityCard("Plata","Fenrir","Nordics" , 4 , new string[]{"Ranged" , "Melee"} , Resources.Load<Sprite>("Fenrir")));
    Cards.Add(new UnityCard("Plata","Fenrir","Nordics" , 4 , new string[]{"Ranged" , "Melee"} , Resources.Load<Sprite>("Fenrir")));
    Cards.Add(new UnityCard("Plata","Fenrir","Nordics" , 4 , new string[]{"Ranged" , "Melee"} , Resources.Load<Sprite>("Fenrir")));
    Cards.Add(new UnityCard("Plata","Drakkar","Nordics" , 5 , new string[]{"Siege"} , Resources.Load<Sprite>("Drakkar")));
    Cards.Add(new UnityCard("Silver","Berserker","Nordics" , 9 , new string[]{"Siege"} , Resources.Load<Sprite>("Berserker")));
    Cards.Add(new UnityCard("Oro","Bendicion de Baldur","Nordics" , 0 , new string[]{"Weather"} , Resources.Load<Sprite>("Bendiciondebaldur")));
    Cards.Add(new UnityCard("Oro","Supertormenta","Nordics" , 0 , new string[]{"Weather"} , Resources.Load<Sprite>("Tormenta")));
    Cards.Add(new UnityCard("Oro","Nilfheim","Nordics" , 0 , new string[]{"Weather"} , Resources.Load<Sprite>("Nilfheim")));
    Cards.Add(new UnityCard("Plata","Frost","Nordics" , 0 , new string[]{"Weather"} , Resources.Load<Sprite>("Alfheim")));
    }
    
    

    public List<UnityCard> NordickDeck {get{return nordickdeck;}}

}
