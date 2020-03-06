using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStoichiometry
{
  public class Chemical
  {
    private string _name;

    public string Name { get => _name; set => _name = value; }
     
    public Chemical(string name)
    {
      _name = name;

    }
  }
}
