
using System.Collections.Generic;
using System.Diagnostics;
namespace Compiler
{
  public class PostAction : DeclaredEffect
  {
    public DeclaredEffect Parent { get; set; }
    public override void Evaluate()
    {
      if (Selector != null)
      {
        if (Selector.Source.Value.Equals("parent"))
        {
          Selector.Source.Value = Parent.Selector.Source.Value;
        }
      }
      else
      {
        Selector = Parent.Selector;
      }
      Selector.Evaluate();
      Name.Evaluate();
      Effect effect = Context.Instance.Effects[(string)Name.Value];
      effect.Evaluate();
      if (PostAction != null)
      {
        PostAction.Parent = this;
        PostAction.Evaluate();
      }
    }
    public override bool CheckSemantic(Context Context, List<CompilingError> Errors, Scope scope)
    {
      return true;
    }

  }

}