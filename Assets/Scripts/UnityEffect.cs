
using System;
using System.Collections.Generic;
using UnityEngine;
public class EffectsContainer
{
    public delegate void Effect();
    public Effect MeleeW { get; private set; }
    public Effect RangedW { get; private set; }
    public Effect SiegeW { get; private set; }
    public EffectsContainer()
    {
        MeleeW  = MeleeWeather;
        RangedW = RangedWeather;
        SiegeW = SiegeWeather;
    }
    public void MeleeWeather()
    {
    Game game = Game.Instance;
     WeatherFunction( game.Player1.GetComponent<Player>().Field.Melee.GetCardList() , game.Player2.GetComponent<Player>().Field.Melee.GetCardList() , 3);
    }
    public void RangedWeather()
    {
        Game game = Game.Instance;
        WeatherFunction( game.Player1.GetComponent<Player>().Field.Ranged.GetCardList() , game.Player2.GetComponent<Player>().Field.Ranged.GetCardList() , 2);
    }
    public void SiegeWeather()
    {
        Game game = Game.Instance;
        WeatherFunction( game.Player1.GetComponent<Player>().Field.Siege.GetCardList() , game.Player2.GetComponent<Player>().Field.Siege.GetCardList() , 4);
    }
    public void WeatherFunction(List<GameObject> list1, List<GameObject>list2 ,int n)
    {
        Game game = Game.Instance;
        foreach (GameObject card in list1)
        {
            card.GetComponent<CardOutput>().PowerValue -= n;
            card.GetComponent<CardOutput>().UpdateProperties();
            
        } 
        foreach (GameObject card in list2)
        {
            card.GetComponent<CardOutput>().PowerValue -= n;
            card.GetComponent<CardOutput>().UpdateProperties();
        } 
        game.Player1.GetComponent<Player>().Field.UpdatePowerCounter();
        game.Player2.GetComponent<Player>().Field.UpdatePowerCounter();
    }
    void IncreaseEffect(List<GameObject> list, int n)
    {
        foreach (GameObject card in list)
        {
            card.GetComponent<CardOutput>().PowerValue += n;
            card.GetComponent<CardOutput>().UpdateProperties();
        }
    }
}

