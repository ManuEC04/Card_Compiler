namespace Compiler
{
    public class Context
    {

        public Dictionary<string , Card> Cards {get; set;}
        public Dictionary<string , Effect> Effects {get; set;}
        public Context()
        {
            Cards = new Dictionary<string, Card>();
            Effects = new Dictionary<string, Effect>();
        }
    }
}