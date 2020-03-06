using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStoichiometry
{
  public static class Parser
  {
    public static List<Reaction> GetReactions(string[] strings)
    {
      var result = new List<Reaction> { };

      for (int i = 0; i < strings.Length; i++)
        result.Add(ParseIt(strings[i]));  

      return result;
    }


    private static Reaction ParseIt(string s)
    {
      //var start = s.IndexOf('x') + 2;
      //var end = s.IndexOf(',');
      //var x = Int32.Parse(s[start..end]);

      //start = s.IndexOf('y') + 2;
      //end = s.IndexOf(',', end + 1);
      //int y = Int32.Parse(s[start..end]);

      //start = (s.IndexOf('z')) + 2;
      //end = (s.IndexOf('>', end + 1));
      //int z = Int32.Parse(s[start..end]);

      //return new Position(x, y, z);

      return new Reaction();
    }
  }
}
