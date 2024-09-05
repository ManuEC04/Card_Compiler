
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
        Context context = Context.Instance;
        public override void Evaluate()
        {
            UnityEngine.Debug.Log(Sintaxys + "." + CardContainer + Method);
            if (Identifier != null)
            {
                UnityEngine.Debug.Log("El identificador no es null");
                string temp = Sintaxys;
                Value = Identifier.Value;
            }
            if (Value.Equals("context"))
            {
                switch (CardContainer)
                {
                    case "TriggerPlayer": Value = context.TriggerPlayer(); break;

                    case "HandOfPlayer":

                        if (Argument == null)
                        {
                            UnityEngine.Debug.Log("El argument aqui no puede ser null");
                            return;
                        }
                        Argument.Evaluate();
                        Value = context.HandOfPlayer((string)Argument.Value); break;

                    case "hand":
                        string id = context.TriggerPlayer();
                        Value = context.HandOfPlayer(id); break;

                    case "DeckOfPlayer":

                        if (Argument == null)
                        {
                            UnityEngine.Debug.Log("El argument aqui no puede ser null");
                            return;
                        }
                        Argument.Evaluate();
                        Value = context.DeckOfPlayer((string)Argument.Value); break;

                    case "deck":
                        string playerid = context.TriggerPlayer();
                        Value = context.DeckOfPlayer(playerid); break;

                    case "field": break;
                    case "FieldOfPlayer": break;
                    case "graveyard": break;
                    case "GraveyardOfPlayer": break;
                }
                if (Method != null)
                {
                    switch (Method)
                    {
                        case "Shuffle": functions.Shuffle((List<GameObject>)Value);break;
                        case "Remove": functions.Remove((List<GameObject>)Value , (GameObject)Argument.Value);break;
                        case "Pop": Value = functions.Pop((List<GameObject>)Value);break;
                        case "SendBottom": functions.SendBottom((List<GameObject>)Value , (GameObject)Argument.Value);break;
                        case "Push": functions.Push((List<GameObject>)Value , (GameObject)Argument.Value);break;
                        case "Find": UnityEngine.Debug.Log("No implementado"); break;
                    }
                }

            }
            else if (Sintaxys == "target")
            {
                Value = Context.Instance.targets[0];
                switch(CardContainer)
                {
                    case"Power": UnityEngine.Debug.Log("Detecto que la propiedad es el power");break;
                }
            }
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
        public Property(string Sintaxys, string CardContainer, string Method, Identifier identifier, Expression Argument, int Position) : base("", ExpressionType.Property, Position)
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