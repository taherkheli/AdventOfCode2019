using System;
using System.Collections;
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
      Reaction result;

      var x = s.Split(" => ");

      for (int i = 0; i < x.Length; i++)
        x[i] = x[i].Trim();

      //parse output
      var output = x[x.Length - 1];
      var z = output.Split(' ');
      
      var mutipleOut = Int32.Parse(z[0]);
      
      result = new Reaction();

      result.Output = new Chemical(z[1]);
      

      //parse input
      var input = x[0];

      var zArray = input.Split(", ");
      for (int i = 0; i < zArray.Length; i++)
        zArray[i] = zArray[i].Trim();
            
      List<int> multiples =  new List<int>();

      for (int i = 0; i < zArray.Length; i++)
      {
        var t = zArray[i].Split(" ");
        result.Inputs.Add(new Chemical(t[1]));
        multiples.Add(Int32.Parse(t[0]));

      }

      multiples.Add(mutipleOut);

      result.Formula = multiples.ToArray();


      //var start = s.IndexOf(('x') + 2;
      //var end = s.IndexOf(',');
      //var x = Int32.Parse(s[start..end]);

      //start = s.IndexOf('y') + 2;
      //end = s.IndexOf(',', end + 1);
      //int y = Int32.Parse(s[start..end]);

      //start = (s.IndexOf('z')) + 2;
      //end = (s.IndexOf('>', end + 1));
      //int z = Int32.Parse(s[start..end]);

      //return new Position(x, y, z);

      return result;
    }
  }
}
