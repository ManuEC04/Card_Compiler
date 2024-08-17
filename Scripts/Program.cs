using System.Security.Principal;
using Compiler;

class Program
{   
    public static void Main(string[]args)
    {
        string input = File.ReadAllText("e:/preview.txt");
        
         Lexer lexer = new Lexer();
         
         List<Token> tokens = lexer.Tokenize(input);
         Parser parser = new Parser(tokens);

         parser.ParseProgram();
}
}
