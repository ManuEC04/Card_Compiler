
using System.Data;
using System.Net.Http.Headers;
using System.Numerics;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Compiler
{
    public class Property : Expression
    {
        public Expression? Argument { get; set; }
        public string Sintaxys { get; set; }
        public string? CardContainer { get; set; }
        public string? Method { get; set; }
        public Identifier? Identifier { get; set; }
        public override object Value { get; set; }
        FunctionContainer functions = new FunctionContainer();
        bool evaluated;
        public override void Evaluate()
        {
            Context context = Context.Instance;
           
            if (Identifier != null)
            {
                UnityEngine.Debug.Log("SE evalua el identifier de la proper");
                if(Identifier.Value.Equals("target") || Identifier.Value.Equals(context))
                {
                    UnityEngine.Debug.Log("Es target o context");
                 Sintaxys = (string)Identifier.Value;
                }
                else
                {
                    UnityEngine.Debug.Log("Es otro identifier");
                    Identifier.Evaluate();
                    if(Identifier.Value is GameObject)
                    {
                       UnityEngine.Debug.Log("Es un GameObject");
                    }
                    else if (Identifier.Value is List<GameObject>)
                    {
                        UnityEngine.Debug.Log("Es una lista");
                        UnityEngine.Debug.Log(CardContainer);
                    SelectMethod(CardContainer , (List<GameObject>)Identifier.Value);
                    return;
                    }

                }
                
            }
             UnityEngine.Debug.Log(Sintaxys + CardContainer + Method);
            if (Sintaxys.Equals("context"))
            {
                GameObject Container;
                switch (CardContainer)
                {
                    case "TriggerPlayer": Value = context.TriggerPlayer(); break;

                    case "HandOfPlayer":

                        if (Argument == null)
                        {

                            return;
                        }
                        Argument.Evaluate();
                        Value = context.HandOfPlayer((string)Argument.Value); Container = context.GetPlayer((string)Argument.Value).Hand.gameObject; break;

                    case "hand":
                        string id = context.TriggerPlayer();
                        Value = context.HandOfPlayer(id);
                        if (Argument != null && Argument.Type != ExpressionType.Predicate)
                        {
                            Argument.Evaluate();
                        }
                        SelectMethod(Method, context.HandOfPlayer(id));
                        break;

                    case "DeckOfPlayer":

                        if (Argument == null)
                        {
                            return;
                        }
                        Argument.Evaluate();
                        Value = context.DeckOfPlayer((string)Argument.Value);
                        SelectMethod(Method, context.DeckOfPlayer((string)Argument.Value));
                        break;

                    case "deck":
                        string playerid = context.TriggerPlayer();
                        Value = context.DeckOfPlayer(playerid);
                        if (Argument != null && Argument.Type != ExpressionType.Predicate)
                        {
                            Argument.Evaluate();
                        }
                        SelectMethod(Method, context.DeckOfPlayer(playerid));
                        break;


                    case "field": break;
                    case "FieldOfPlayer": break;
                    case "graveyard": break;
                    case "GraveyardOfPlayer": break;
                }


            }
            else if (Sintaxys == "target")
            {
                if (!evaluated)
                {
                    Value = context.targets[0].GetComponent<CardOutput>();
                }

                switch (CardContainer)
                {
                    case "Power":
                        UnityEngine.Debug.Log("Detecto que la propiedad es el power");

                        if (!evaluated)
                        {
                            Value = context.targets[0].GetComponent<CardOutput>().PowerValue;
                            evaluated = true;
                        }
                        else
                        {
                            context.targets[0].GetComponent<CardOutput>().PowerValue = (double)Value;
                        }
                        UnityEngine.Debug.Log("Aqui te imprimo el power" + Value);
                        break;

                    case "Owner":
                        Value = context.targets[0].GetComponent<CardOutput>().PlayerId;
                        break;
                }
            }
        }
        void SelectMethod(string Method, List<GameObject> list)
        {
            switch (Method)
            {
                case "Add":
                    GameObject myobject = (GameObject)Argument.Value;
                    list.Add(myobject);
 break;

                case "Shuffle": functions.Shuffle(list); break;

                case "Remove":
                UnityEngine.Debug.Log("Vamos a ejecutar el remove");
                Game game = Game.Instance;
                GameObject myobject1 = (GameObject)Argument.Value; list.Remove(myobject1);myobject1.transform.SetParent(game.gameObject.transform , true);break;
                case "Pop":
                    Value = functions.Pop(list);
                    break;

                case "SendBottom": functions.SendBottom(list, (GameObject)Argument.Value); break;

                case "Push": Argument.Evaluate();functions.Push(list, (GameObject)Argument.Value); break;

                case "Find": UnityEngine.Debug.Log("Entramos en el find");functions.Find((Predicate)Argument , list); break;
            }
        }
        public override void ResetValues()
        {
            evaluated = false;
            if (Argument != null)
            {
                Argument.ResetValues();
            }
            Value = "";
        }
        public override bool CheckSemantic(Context Context, List<CompilingError> Errors, Scope scope)
        {
            if (Identifier != null)
            {
                Identifier.CheckSemantic(Context, Errors, scope);
                Identifier.Evaluate();
            }
            return true;
        }
        public Property(string Sintaxys, Identifier Identifier, int Position) : base("", ExpressionType.Property, Position)
        {
            Value = "";
            this.Sintaxys = Sintaxys;
            this.Position = Position;
            this.Identifier = Identifier;
        }
        public Property(string Sintaxys, string CardContainer, string Method, Identifier Identifier, Expression Argument, int Position) : base("", ExpressionType.Property, Position)
        {
            Value = "";
            this.Sintaxys = Sintaxys;
            this.Position = Position;
            this.CardContainer = CardContainer;
            this.Method = Method;
            this.Argument = Argument;
            this.Identifier = Identifier;
        }
        public Property(string Sintaxys, Identifier Identifier, Expression Argument, int Position) : base(".", ExpressionType.Property, Position)
        {
            Value = "";
            this.Argument = Argument;
            this.Sintaxys = Sintaxys;
            this.Position = Position;
            this.Identifier = Identifier;
        }

    }
}