using System;
using System.Collections;
using System.Collections.Generic;
using Compiler;
using UnityEngine;

public class UnityCard 
{
    public string Type {get; private set;}
    public string Name {get; private set;}
    public string Faction {get; private set;}
    public double Power {get; private set;}
    public string[] Range {get;private set;}
    public Delegate Effect {get;private set;}
    public OnActivation OnActivation {get;private set;}
    public Sprite Picture;
    public UnityCard(string Type , string Name , string Faction , double Power , string[]Range , Sprite Picture , OnActivation OnActivation)
    {
        this.Type = Type;
        this.Name = Name;
        this.Faction = Faction;
        this.Power = Power;
        this.Range = Range;
        this.Picture = Picture;
        this.OnActivation = OnActivation;
        Context context = Context.Instance;
        CardDatabase Database = CardDatabase.Instance;
        Database.Cards.Add(this);
    }
    public UnityCard(string Type , string Name , string Faction , double Power , string[]Range , Sprite Picture , Delegate Effect)
    {
        this.Type = Type;
        this.Name = Name;
        this.Faction = Faction;
        this.Power = Power;
        this.Range = Range;
        this.Picture = Picture;
        this.Effect = Effect;
        Context context = Context.Instance;
        CardDatabase Database = CardDatabase.Instance;
        Database.Cards.Add(this);
    }
        public UnityCard(string Type , string Name , string Faction , double Power , string[]Range , Sprite Picture)
    {
        this.Type = Type;
        this.Name = Name;
        this.Faction = Faction;
        this.Power = Power;
        this.Range = Range;
        this.Picture = Picture;
        Context context = Context.Instance;
        CardDatabase Database = CardDatabase.Instance;
        Database.Cards.Add(this);
    }

}
