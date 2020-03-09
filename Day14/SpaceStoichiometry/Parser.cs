using System;
using System.Collections.Generic;

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
      Reaction result = new Reaction();

      var step1 = s.Split(" => ");

      for (int i = 0; i < step1.Length; i++)
        step1[i] = step1[i].Trim();

      //parse RHS
      var step2 = (step1[step1.Length - 1]).Split(' ');
      result.Output = new Chemical(step2[1], Int32.Parse(step2[0]));    

      //parse LHS
      var step3 = step1[0].Split(", ");

      for (int i = 0; i < step3.Length; i++)
        step3[i] = step3[i].Trim();            

      for (int i = 0; i < step3.Length; i++)
      {
        var temp = step3[i].Split(' ');
        result.Inputs.Add(new Chemical(temp[1], Int32.Parse(temp[0])));
      }

      return result;
    }
  }
}