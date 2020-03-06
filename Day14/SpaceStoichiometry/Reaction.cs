using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStoichiometry
{
  public class Reaction
  {
    private List<Chemical> _inputs;
    private Chemical _output;
    private int[] _formula;

    public List<Chemical> Inputs { get => _inputs; set => _inputs = value; }
    public Chemical Output { get => _output; set => _output = value; }

    public int[] Formula { get => _formula; set => _formula = value; }

    public Reaction()
    {

    }
  }
}
