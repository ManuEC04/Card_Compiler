namespace Compiler
{
    public class Parser
    {
        public CardCreation CheckCardCreation(List<Token> tokens, ref int i)
        {
            CardCreation Card = new CardCreation();
            TokenValues CardChecker = new TokenValues();
            CheckCardType(Card, CardChecker, tokens, ref i);
            CheckCardName(Card, CardChecker, tokens, ref i);
            CheckCardFaction(Card, CardChecker, tokens, ref i);
            CheckCardRange(Card, CardChecker, tokens, ref i);
            CheckCardPower(Card, CardChecker, tokens, ref i);
            return Card;
        }
        public void PrintValues(List<Token> tokens, int i)
        {
            CardCreation MyCard = CheckCardCreation(tokens, ref i);
            Console.WriteLine(MyCard.Name_Assignation);
            Console.WriteLine(MyCard.Type_Assignation);
            Console.WriteLine(MyCard.Faction_Assignation);
            Console.WriteLine(MyCard.Range_Assignation);
            Console.WriteLine(MyCard.Power_Assignation);
        }
        void CheckCardType(CardCreation Card, TokenValues CardChecker, List<Token> tokens, ref int i)
        {
            if (Card.Type_Assignation == "")
            {
                if (tokens[i].Value == CardChecker.Card)
                {
                    i++;
                    if (tokens[i].Value == CardChecker.OpenCurlyBraces)
                    {
                        i++;
                        if (tokens[i].Value == CardChecker.Type)
                        {
                            i++;
                            if (tokens[i].Value == CardChecker.Points)
                            {
                                i++;
                                if (tokens[i].Value == CardChecker.QuotationMark)
                                {
                                    i++;

                                    if (tokens[i].Type == "IDENTIFIER")
                                    {
                                        CardCreation Temp = new CardCreation();
                                        Temp.Type_Assignation = tokens[i].Value;
                                        i++;
                                        if (tokens[i].Value == CardChecker.QuotationMark)
                                        {
                                            i++;

                                            if (tokens[i].Value == CardChecker.Comma)
                                            {
                                                Card.Type_Assignation = Temp.Type_Assignation;
                                                i++;
                                            }
                                            else
                                            {
                                                Console.WriteLine("$ , Was expected at line" + tokens[i].Position);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("$An Identifier Was expected at line" + tokens[i].Position);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine(": Was expected at line" + tokens[i].Position);
                            }
                        }
                    }

                }
                else
                {
                    Console.WriteLine("A type is already declared");
                }
            }
        }
        void CheckCardFaction(CardCreation Card, TokenValues CardChecker, List<Token> tokens, ref int i)
        {
            if (Card.Faction_Assignation == "")
            {
                if (tokens[i].Value == CardChecker.Faction)
                {
                    i++;
                    if (tokens[i].Value == CardChecker.Points)
                    {
                        i++;
                        if (tokens[i].Value == CardChecker.QuotationMark)
                        {

                            i++;

                            if (tokens[i].Type == "IDENTIFIER")
                            {
                                CardCreation Temp = new CardCreation();
                                Temp.Faction_Assignation = tokens[i].Value;
                                i++;
                                if (tokens[i].Value == CardChecker.QuotationMark)
                                {
                                    i++;
                                    if (tokens[i].Value == CardChecker.Comma)
                                    {
                                        Card.Faction_Assignation = Temp.Faction_Assignation;
                                        i++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("$ , Was expected at line" + tokens[i].Position);
                                    }
                                }

                            }
                            else
                            {
                                Console.WriteLine("$An Identifier Was expected at line" + tokens[i].Position);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine(": Was expected at line" + tokens[i].Position);
                    }
                }
            }
        }
        void CheckCardPower(CardCreation Card, TokenValues CardChecker, List<Token> tokens, ref int i)
        {
            if (Card.Power_Assignation == "")
            {
                if (tokens[i].Value == CardChecker.Power)
                {
                    i++;
                    if (tokens[i].Value == CardChecker.Points)
                    {
                        i++;
                        if (tokens[i].Type == "NUMBER")
                        {
                            CardCreation Temp = new CardCreation();
                            Temp.Power_Assignation = tokens[i].Value;
                            i++;

                            if (tokens[i].Type == "OPERATOR")
                            {

                            }

                            else if (tokens[i].Value == CardChecker.Comma)
                            {
                                Card.Power_Assignation = Temp.Power_Assignation;
                                i++;
                            }
                            else
                            {
                                Console.WriteLine("$ , Was expected at line" + tokens[i].Position);
                            }

                        }
                    }
                }
            }
        }
        void CheckCardName(CardCreation Card, TokenValues CardChecker, List<Token> tokens, ref int i)
        {
            if (Card.Name_Assignation == "")
            {
                if (tokens[i].Value == CardChecker.Name)
                {
                    i++;
                    if (tokens[i].Value == CardChecker.Points)
                    {
                        i++;
                        if (tokens[i].Value == CardChecker.QuotationMark)
                        {
                            i++;
                            if (tokens[i].Type == "IDENTIFIER")
                            {
                                CardCreation Temp = new CardCreation();
                                Temp.Name_Assignation = tokens[i].Value;
                                i++;
                                if (tokens[i].Value == CardChecker.QuotationMark)
                                {
                                    i++;
                                    if (tokens[i].Value == CardChecker.Comma)
                                    {

                                        Card.Name_Assignation = Temp.Name_Assignation;
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("$An Identifier Was expected at line" + tokens[i].Position);
                            }
                        }
                    }
                }
            }
            else { return; }
        }
        void CheckCardRange(CardCreation Card, TokenValues CardChecker, List<Token> tokens, ref int i)
        {
            if (Card.Range_Assignation == "")
            {

                if (tokens[i].Value == CardChecker.Range)
                {
                    i++;
                    if (tokens[i].Value == CardChecker.Points)
                    {
                        i++;
                        if (tokens[i].Value == CardChecker.OpenSquareBracket)
                        {
                            i++;
                            if (tokens[i].Value == CardChecker.QuotationMark)
                            {
                                i++;

                                if (tokens[i].Type == "IDENTIFIER")
                                {
                                    CardCreation Temp = new CardCreation();
                                    Temp.Range_Assignation = tokens[i].Value;
                                    i++;
                                    if (tokens[i].Value == CardChecker.QuotationMark)
                                    {
                                        i++;

                                        if (tokens[i].Value == CardChecker.ClosedSquareBracket)
                                        {
                                            i++;
                                            if (tokens[i].Value == CardChecker.Comma)
                                            {

                                                Card.Range_Assignation = Temp.Range_Assignation;
                                                i++;
                                            }
                                            else
                                            {
                                                Console.WriteLine(", was expected at line" + tokens[i].Position);
                                            }
                                        }
                                        else if (tokens[i].Value == CardChecker.Comma)
                                        {
                                            i++;
                                            if (tokens[i].Value == CardChecker.QuotationMark)
                                            {
                                                if (tokens[i].Type == "IDENTIFIER")
                                                {
                                                    CardCreation Temp1 = new CardCreation();
                                                    Temp1.Range_Assignation = tokens[i].Value;
                                                    i++;
                                                    if (tokens[i].Value == CardChecker.QuotationMark)
                                                    {
                                                        i++;
                                                        if (tokens[i].Value == CardChecker.ClosedSquareBracket)
                                                        {
                                                            i++;
                                                            if (tokens[i].Value == CardChecker.Comma)
                                                            {
                                                                Card.Range_Assignation = Temp.Range_Assignation + "," + Temp1.Range_Assignation;
                                                                i++;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("An identifier was expected at line" + tokens[i].Position);
                                }
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
                else
                {
                    Console.WriteLine(": Was expected at line" + tokens[i].Position);
                }
            }
        }
    }
}

