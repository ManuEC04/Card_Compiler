using System.Collections.Generic;
using Compiler;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class Compiling : MonoBehaviour
{
    string path = @"e:\Yo\Manuel Universidad\Clases\Segundo Semestre\Compilador\Gwent++\script.txt";
    CardDatabase Database;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        Database = CardDatabase.Instance;
        Debug.Log(Database.Cards.Count);
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Card");

    }


    public void CompilingProcess()
    {
        List<CompilingError> Errors = new List<CompilingError>();
        Lexer lexer = new Lexer();
        List<Token> tokens = lexer.Tokenize(path, Errors);

        foreach (Token token in tokens)
        {
            Debug.Log(token.Type + " " + token.Value);
        }
        Parser parser = new Parser(tokens, Errors);
        AST tree = parser.ParseProgram();

        foreach(CompilingError error in Errors)
        {
           Debug.Log(error.Argument);
        }

        foreach(ASTNode node in tree.Nodes)
        {
            node.Evaluate();
        }
        //Showing the card on display
        Vector2 Position = new Vector2(1438, 0);
        prefab.GetComponent<CardOutput>().SetValues(Database.Cards[0]);
        GameObject newprefab = Instantiate(prefab, Position, Quaternion.identity);
        newprefab.GetComponent<RectTransform>().transform.position = new Vector2(1441,524);
        newprefab.GetComponent<RectTransform>().localScale = new Vector2(0.7f,0.7f);
        GameObject Canvas = GameObject.Find("Canvas");
        newprefab.transform.SetParent(Canvas.transform , true);
        SceneManager.LoadScene("Main");
        

    }

}
