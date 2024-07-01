namespace Compiler
{
    public class Parser
    {


        public void CheckCardCreation(List<Token> tokens, int i)
        {
            CardCreation Card = new CardCreation();
            TokenValues CardChecker = new TokenValues();
            if (tokens[i].Value == CardChecker.Card)
            {
                Console.WriteLine("Se ejecuta la primera evaluacion");
                if (tokens[LookAhead(i)].Value == CardChecker.OpenCurlyBraces)
                {
                    Console.WriteLine("Se encuentra {");
                    if (tokens[LookAhead(i)].Value == CardChecker.Type)
                    {
                        Console.WriteLine("Se encuentra la palabra reservada type");
                        if (tokens[LookAhead(i)].Value == CardChecker.Points)
                        {
                            Console.WriteLine("Se encuentran dos puntos");
                            if (tokens[LookAhead(i)].Type == "IDENTIFIER")
                            {
                                Console.WriteLine("Se encuentra un identificador");
                                if (tokens[LookAhead(i)].Type == CardChecker.Comma)
                                {
                                    Console.WriteLine("Se encuentra la coma");
                                    Card.Type_Assignation = tokens[LookAhead(i)].Value;
                                    Console.WriteLine("Se ha hecho la asignacion");
                                }
                                else { Console.WriteLine("$ , Was expected at line" + tokens[i].Position); }
                            }
                            else
                            {
                                Console.WriteLine("$An Identifier Was expected at line" + tokens[i].Position);
                            }
                        }
                        else
                        {
                            Console.WriteLine(": Was expected at line" + tokens[i].Position);
                        }
                    }


                    else if (tokens[LookAhead(i)].Value == CardChecker.Name)
                    {
                        if (tokens[LookAhead(i)].Value == CardChecker.Points)
                        {
                            if (tokens[LookAhead(i)].Type == "IDENTIFIER")
                            {
                                Card.Name_Assignation = tokens[LookAhead(i)].Value;
                            }
                            else
                            {
                                Console.WriteLine("$An Identifier Was expected at line" + tokens[i].Position);
                            }
                        }


                        else if (tokens[LookAhead(i)].Value == CardChecker.Faction)
                        {
                            if (tokens[LookAhead(i)].Value == CardChecker.Points)
                            {
                                if (tokens[LookAhead(i)].Type == "IDENTIFIER")
                                {
                                    Card.Faction_Assignation = tokens[i].Value;
                                }
                                else
                                {
                                    Console.WriteLine("$An Identifier Was expected at line" + tokens[i].Position);
                                }
                            }


                            else if (tokens[LookAhead(i)].Value == CardChecker.Power)
                            {
                               throw new NotImplementedException();
                            }


                            else if (tokens[LookAhead(i)].Value == CardChecker.Range)
                            {
                                if (tokens[LookAhead(i)].Value == CardChecker.Points)
                                {
                                    if (tokens[LookAhead(i)].Value == CardChecker.OpenSquareBracket)
                                    {
                                        if (tokens[LookAhead(i)].Type == "IDENTIFIER")
                                        {
                                            if (tokens[LookAhead(i)].Type == CardChecker.ClosedSquareBracket)
                                            {
                                                if (tokens[LookAhead(i)].Type == CardChecker.Comma)
                                                {
                                                    Card.Range_Assignation = tokens[LookBack(i, 2)].Value;
                                                }
                                                else
                                                {
                                                    Console.WriteLine(", was expected at line" + tokens[i].Position);
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("] was expected at line" + tokens[i].Position);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("An identifier was expected at line" + tokens[i].Position);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("[ Was expected at line" + tokens[i].Position);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(": Was expected at line" + tokens[i].Position);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine(": Was expected at line" + tokens[i].Position);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("${ Was expected at line" + tokens[i].Position);
                }
            }
        }
        /* bool CanLookAhead(List<Token> tokens, int i)
         {
             if (i != tokens.Count - 1)
             {
                 return true;
             }
             return false;
         }
         */
        int LookAhead(int position)
        {
            return ++position;
        }
        int LookBack(int position, int i)
        {
            return position - i;
        }
    }

}


