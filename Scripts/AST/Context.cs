namespace Compiler
{
    public class Context
    {
        public Dictionary <string , Declaration> Declaration {get; set;}
        public Context()
        {
            Declaration = new Dictionary <string , Declaration>();
        }
    }
}