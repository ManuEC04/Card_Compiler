namespace Compiler
{
    public class FunctionContainer
    {
        public Dictionary<string, Delegate> Functions = new Dictionary<string, Delegate> { };
        public object Pop<T>(List<T> List)
        {
            T temp = List[0];
            List.RemoveAt(0);
            return temp;
        }
        public void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public string TriggerPlayer(Card card)
        {
           return "player1";
        }

        public void CreateFunctions()
        {

            Functions.Add(".Pop", Pop<Card>);
            Functions.Add(".Shuffle", Shuffle<Card>);
            Functions.Add("context.TriggerPlayer" ,TriggerPlayer );
        }

    }
}
