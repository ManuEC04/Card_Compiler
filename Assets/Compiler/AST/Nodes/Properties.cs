
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
        public Expression? Index { get; set; }
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

            if (Identifier != null && Identifier.Value !=null)
            {
                if (Identifier.Value.Equals("target") || Identifier.Value.Equals(context))
                {
                    Sintaxys = (string)Identifier.Value;
                }
                else
                {
                    Identifier.Evaluate();
                    if (Identifier.Value is List<GameObject>)
                    {
                        UnityEngine.Debug.Log("Es una lista");
                        UnityEngine.Debug.Log(CardContainer);
                        SelectMethod(CardContainer, (List<GameObject>)Identifier.Value);
                        return;
                    }

                }

            }
            UnityEngine.Debug.Log(Sintaxys + CardContainer + Method);
            if (Sintaxys.Equals("context"))
            {
                switch (CardContainer)
                {
                    case "TriggerPlayer": Value = context.TriggerPlayer(); break;

                    case "HandOfPlayer":

                        if (Argument == null)
                        {
                            return;
                        }
                        Argument.Evaluate();
                        Value = context.HandOfPlayer((string)Argument.Value);break;

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
                        UnityEngine.Debug.Log("Este es el deck de "+(string)Argument.Value);
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


                    case "field":
                        string myplayerid = context.TriggerPlayer();
                        Value = context.FieldOfPlayer(myplayerid);
                        if (Argument != null && Argument.Type != ExpressionType.Predicate)
                        {
                            Argument.Evaluate();
                        }
                        SelectMethod(Method, context.DeckOfPlayer(myplayerid));
                        break;

                    case "FieldOfPlayer":
                        if (Argument == null)
                        {
                            return;
                        }
                        Argument.Evaluate();
                        Value = context.FieldOfPlayer((string)Argument.Value);
                        SelectMethod(Method, context.FieldOfPlayer((string)Argument.Value));
                        break;

                    case "graveyard":
                        Value = context.FieldOfPlayer(context.TriggerPlayer());
                        if (Argument != null && Argument.Type != ExpressionType.Predicate)
                        {
                            Argument.Evaluate();
                        }
                        SelectMethod(Method, context.FieldOfPlayer(context.TriggerPlayer()));
                        break;

                    case "GraveyardOfPlayer":
                        if (Argument == null)
                        {
                            return;
                        }
                        Argument.Evaluate();
                        Value = context.GraveyardOfPlayer((string)Argument.Value);
                        SelectMethod(Method, context.GraveyardOfPlayer((string)Argument.Value));
                        break;
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
                        if (!evaluated)
                        {
                            Value = context.targets[0].GetComponent<CardOutput>().PowerValue;
                            evaluated = true;
                        }
                        else
                        {
                            context.targets[0].GetComponent<CardOutput>().PowerValue = (double)Value;
                        }
                        break;

                    case "Owner":
                        Value = context.targets[0].GetComponent<CardOutput>().PlayerId;
                        UnityEngine.Debug.Log("Esta es la ide que devuelve el target.Owner"+ Value);
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
                    GameObject myobject1 = (GameObject)Argument.Value; list.Remove(myobject1); myobject1.transform.SetParent(game.gameObject.transform, true); break;
                case "Pop":
                    Value = functions.Pop(list);
                    break;

                case "SendBottom": functions.SendBottom(list, (GameObject)Argument.Value); break;

                case "Push": Argument.Evaluate(); functions.Push(list, (GameObject)Argument.Value); break;

                case "Find": UnityEngine.Debug.Log("Entramos en el find"); functions.Find((Predicate)Argument, list); break;
            }
            if (Index != null && Value is List<GameObject>)
            {
                Index.Evaluate();
                List<GameObject> finallist = (List<GameObject>)Value;
                int index = (int)Index.Value;
                Value = finallist[index];
            }
        }
        public override void ResetValues()
        {
            evaluated = false;
            if (Argument != null)
            {
                Argument.ResetValues();
            }
            if(Identifier !=null){
                Identifier.ResetValues();
            }
            Value = "";
        }
        public override bool CheckSemantic(Context Context, List<CompilingError> Errors, Scope scope)
        {
            if (Identifier != null)
            {
                Identifier.CheckSemantic(Context, Errors, scope);

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
        public Property(string Sintaxys, string CardContainer, string Method, Identifier Identifier, Expression Argument, Expression Index, int Position) : base("", ExpressionType.Property, Position)
        {
            Value = "";
            this.Sintaxys = Sintaxys;
            this.Position = Position;
            this.CardContainer = CardContainer;
            this.Method = Method;
            this.Argument = Argument;
            this.Identifier = Identifier;
            this.Index = Index;
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