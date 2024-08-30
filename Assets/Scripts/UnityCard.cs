using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityCard 
{
    public string Type {get; set;}
    public string Name {get; set;}
    public string Faction {get;set;}
    public double Power {get;set;}
    public List<string> Range {get;set;}
    public Sprite Picture;
    public UnityCard(string Type , string Name , string Faction , double Power , List<string>Range , Sprite Picture)
    {
        this.Type = Type;
        this.Name = Name;
        this.Faction = Faction;
        this.Power = Power;
        this.Range = Range;
        this.Picture = Picture;
        CardDatabase Database = CardDatabase.Instance;
        Database.Cards.Add(this);
    }
    public UnityCard(){}

}
