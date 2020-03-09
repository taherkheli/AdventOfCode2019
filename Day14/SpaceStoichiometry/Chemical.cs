using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStoichiometry
{
  public class Chemical
  {
    private string _name;
    private int _multiple;

    public string Name { get => _name; set => _name = value; }
    public int Multiple { get => _multiple; set => _multiple = value; }

    public Chemical(string name, int multiple)
    {
      _name = name;
      _multiple = multiple;
    }
  }
}
