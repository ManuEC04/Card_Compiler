
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
            UnityEngine.Debug.Log("VA A EVALUAR UNA PROPIEDAD");
            UnityEngine.Debug.Log(Sintaxys + CardContainer + Method);
            if (Identifier != null)
            {
                UnityEngine.Debug.Log("El identificador no es null");
                string temp = Sintaxys;
                Value = Identifier.Value;
            }
            if (Sintaxys.Equals("context"))
            {
                UnityEngine.Debug.Log("Aqui detecta el context");
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
                        Value = context.HandOfPlayer(id); 
                        SelectMethod(Method , context.HandOfPlayer(id) , (GameObject)Argument.Value);
                        break;

                    case "DeckOfPlayer":

                        if (Argument == null)
                        {
                            UnityEngine.Debug.Log("El argument aqui no puede ser null");
                            return;
                        }
                        Argument.Evaluate();
                        Value = context.DeckOfPlayer((string)Argument.Value); break;

                    case "deck":
                    UnityEngine.Debug.Log("Detecta el deck");
                        string playerid = context.TriggerPlayer();
                        Value = context.DeckOfPlayer(playerid); 
                        break;
                        

                    case "field": break;
                    case "FieldOfPlayer": break;
                    case "graveyard": break;
                    case "GraveyardOfPlayer": break;
                }
                if (Method != null)
                {
                    switch (Method)
                    {
                        case "Add":
                        case "Shuffle": functions.Shuffle((List<GameObject>)Value);break;
                        case "Remove": functions.Remove((List<GameObject>)Value , (GameObject)Argument.Value);break;
                        case "Pop": UnityEngine.Debug.Log("Detecta el pop");Value = functions.Pop((List<GameObject>)Value);break;
                        case "SendBottom": functions.SendBottom((List<GameObject>)Value , (GameObject)Argument.Value);break;
                        case "Push": functions.Push((List<GameObject>)Value , (GameObject)Argument.Value);break;
                        case "Find": UnityEngine.Debug.Log("No implementado"); break;
                    }
                }

            }
            else if (Sintaxys == "target")
            {
                if(!evaluated)
                {
                    Value = context.targets[0].GetComponent<CardOutput>();
                }
                
                switch(CardContainer)
                {
                    case"Power": UnityEngine.Debug.Log("Detecto que la propiedad es el power");
                    
                    if(!evaluated)
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
                }
            }
        }
        void SelectMethod(string Method , List<GameObject>list , GameObject myobject)
        {
         switch (Method)
                    {
                        case "Add": list.Add((GameObject)Argument.Value);
                        GameObject objeto = (GameObject)Argument.Value;
                        objeto.transform.SetParent(objeto.transform , true);
                        break;
                        case "Shuffle": functions.Shuffle(list);break;
                        case "Remove": functions.Remove(list, (GameObject)Argument.Value);break;
                        case "Pop": Value = functions.Pop(list);break;
                        case "SendBottom": functions.SendBottom(list , (GameObject)Argument.Value);break;
                        case "Push": functions.Push(list, (GameObject)Argument.Value);break;
                        case "Find": UnityEngine.Debug.Log("No implementado"); break;
                    }
        }
        public override void ResetValues()
        {
            evaluated = false;
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