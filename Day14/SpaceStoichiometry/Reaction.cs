using System;
using System.Collections.Generic;

namespace SpaceStoichiometry
{
  public class Reaction
  {
    private List<Chemical> _inputs;
    private Chemical _output;

    public List<Chemical> Inputs { get => _inputs; set => _inputs = value; }
    public Chemical Output { get => _output; set => _output = value; }

    public Reaction()
    {
      _inputs = new List<Chemical>();
    }
    
    public int CalculateNeededOre(List<Reaction> reactions)
    {

      Reaction reaction = null;
      bool done = false;

      while (!done)
      {

        //find a candidate for substitution 
        for (int i = 0; i < this.Inputs.Count; i++)
        {
          reaction = reactions.Find(r => r.Output.Name == this.Inputs[i].Name);    //look for a recipe reaction

          if (reaction == null)
            throw new Exception("Something went wrong!");
          else
          {
            if (reaction.Inputs.Count > 1)   //not ORE ... 1 input = ORE = simplified already            
              break;
            else     //assuming 1 input is always ORE = simplified
            {
              reaction = null;
              continue;
            }
          }
        }

        if (reaction != null)    //a substitution is still needed
          this.Substitute(reaction);
        else
          done = true;
      }
      
      return CalculateOre(reactions);
    }

    //now just ORE subsitution remains
    private int CalculateOre(List<Reaction> reactions)
    {
      Reaction reaction = null;
      int result = 0;

      foreach (var item in this.Inputs)
      {
        reaction = reactions.Find(r => r.Output.Name == item.Name);    //look for a recipe reaction

        if (reaction == null)
          throw new Exception("Something went wrong!");
        else
        {
          decimal d = reaction.Output.Multiple;
          decimal factor = Math.Ceiling(item.Multiple / d);
          result += (reaction.Inputs[0].Multiple * (int)factor);
        }
      }

      return result;
    }

    private void Substitute(Reaction r)
    {
      var chemical = this.Inputs.Find(c => c.Name == r.Output.Name);

      if (chemical == null)
        throw new ArgumentException("{0} was not found among input chemicals", r.Output.Name);

      int lcm = GetLCM(chemical.Multiple, r.Output.Multiple);
      int f_r = lcm / r.Output.Multiple;
      int f_this = lcm /chemical.Multiple;

      r.Output.Multiple *= f_r;
      foreach (var i in r.Inputs)
        i.Multiple *= f_r;

      this.Output.Multiple *= f_this;
      foreach (var i in this.Inputs)
        i.Multiple *= f_this;

      this.Inputs.Remove(chemical);

      foreach (var item in r.Inputs)
      {
        var temp = this.Inputs.Find(c => c.Name == item.Name);

        if (temp == null)
          this.Inputs.Add(item);
        else
          temp.Multiple += item.Multiple;
      }
    }
    
    private int GetLCM(int num1, int num2)
    {
      int x = num1;
      int y = num2;

      while (num1 != num2)
      {
        if (num1 > num2)
          num1 -= num2;
        else
          num2 -= num1;
      }

      return (x * y) / num1;
    }
  }
}