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
    }


    //MYDECKS

    private List<UnityCard> nordickdeck = new List<UnityCard>();
    


    public void CreateNordickDeck()
    {
    nordickdeck.Add(new UnityCard("Plata","Vali","Nordics" , 5 , new List<string>(){"Melee" , "Ranged"} , Resources.Load<Sprite>("Pictures/Cards/Vali")));
    nordickdeck.Add(new UnityCard("Plata","Vali","Nordics" , 5 , new List<string>(){"Melee" , "Ranged"} , Resources.Load<Sprite>("Pictures/Cards/Vali")));
    nordickdeck.Add(new UnityCard("Oro","Thor","Nordics" , 7 , new List<string>(){"Melee"} , Resources.Load<Sprite>("Pictures/Cards/Thor")));
    nordickdeck.Add(new UnityCard("Plata","Señuelo de Loki","Nordics" , 0 , new List<string>(){"Melee" , "Ranged" , "Siege"} , Resources.Load<Sprite>("Pictures/Cards/Lokislure")));
    nordickdeck.Add(new UnityCard("Oro","Odin","Nordics" , 5 , new List<string>(){"FactionLeader"} , Resources.Load<Sprite>("Pictures/Cards/Odin")));
    nordickdeck.Add(new UnityCard("Plata","Mimir","Nordics" , 3 , new List<string>(){"Ranged"} , Resources.Load<Sprite>("Pictures/Cards/Mimir")));
    nordickdeck.Add(new UnityCard("Oro","Loki","Nordics" , 6 , new List<string>(){"Ranged"} , Resources.Load<Sprite>("Pictures/Cards/Loki")));
    nordickdeck.Add(new UnityCard("Gold","Catapulta de Odin","Nordics" , 9 , new List<string>(){"Siege"} , Resources.Load<Sprite>("Pictures/Cards/catapultadeodin")));
    nordickdeck.Add(new UnityCard("Plata","Hela","Nordics" , 4 , new List<string>(){"Ranged"} , Resources.Load<Sprite>("Pictures/Cards/Hela")));
    nordickdeck.Add(new UnityCard("Oro","","Heimdall" , 5 , new List<string>(){"Ranged","Melee"} , Resources.Load<Sprite>("Pictures/Cards/Heimdall")));
    nordickdeck.Add(new UnityCard("Oro","Loki","Nordics" , 6 , new List<string>(){"Ranged"} , Resources.Load<Sprite>("Pictures/Cards/Loki")));
    nordickdeck.Add(new UnityCard("Oro","Gungnir","Nordics" , 0 , new List<string>(){"Increase"} , Resources.Load<Sprite>("Pictures/Cards/Odinsweapon")));
    nordickdeck.Add(new UnityCard("Oro","Gajallahorn","Nordics" , 0 , new List<string>(){"Increase"} , Resources.Load<Sprite>("Pictures/Cards/Gajallahorn")));
    nordickdeck.Add(new UnityCard("Plata","Frost","Nordics" , 6 , new List<string>(){"Siege"} , Resources.Load<Sprite>("Pictures/Cards/Frost")));
    nordickdeck.Add(new UnityCard("Plata","Frost","Nordics" , 6 , new List<string>(){"Siege"} , Resources.Load<Sprite>("Pictures/Cards/Frost")));
    nordickdeck.Add(new UnityCard("Plata","Freya","Nordics" , 6 , new List<string>(){"Ranged"} , Resources.Load<Sprite>("Pictures/Cards/Freya")));
    nordickdeck.Add(new UnityCard("Plata","Fenrir","Nordics" , 4 , new List<string>(){"Ranged" , "Melee"} , Resources.Load<Sprite>("Pictures/Cards/Fenrir")));
    nordickdeck.Add(new UnityCard("Plata","Fenrir","Nordics" , 4 , new List<string>(){"Ranged" , "Melee"} , Resources.Load<Sprite>("Pictures/Cards/Fenrir")));
    nordickdeck.Add(new UnityCard("Plata","Fenrir","Nordics" , 4 , new List<string>(){"Ranged" , "Melee"} , Resources.Load<Sprite>("Pictures/Cards/Fenrir")));
    nordickdeck.Add(new UnityCard("Plata","Drakkar","Nordics" , 5 , new List<string>(){"Siege"} , Resources.Load<Sprite>("Pictures/Cards/Drakkar")));
    nordickdeck.Add(new UnityCard("Silver","Berserker","Nordics" , 9 , new List<string>(){"Siege"} , Resources.Load<Sprite>("Pictures/Cards/Berserker")));
    nordickdeck.Add(new UnityCard("Oro","Bendicion de Baldur","Nordics" , 0 , new List<string>(){"Weather"} , Resources.Load<Sprite>("Pictures/Cards/Bendiciondebaldur")));
    nordickdeck.Add(new UnityCard("Oro","Supertormenta","Nordics" , 0 , new List<string>(){"Weather"} , Resources.Load<Sprite>("Pictures/Cards/Supertormenta")));
    nordickdeck.Add(new UnityCard("Oro","Nilfheim","Nordics" , 0 , new List<string>(){"Weather"} , Resources.Load<Sprite>("Pictures/Cards/Nilfheim")));
    nordickdeck.Add(new UnityCard("Plata","Frost","Nordics" , 0 , new List<string>(){"Weather"} , Resources.Load<Sprite>("Pictures/Cards/Alfheim")));
    }
    
    

    public List<UnityCard> NordickDeck {get{return nordickdeck;}}

}
