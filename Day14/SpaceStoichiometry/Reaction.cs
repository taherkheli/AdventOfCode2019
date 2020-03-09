using System;
using System.Collections.Generic;
using System.Text;

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

    public Reaction Substitute(List<Reaction> reactions)
    {
      List<int> IndicesToRemove = new List<int>();
      List<int> IndicesToReplace = new List<int>();

      while (true)
      {
        foreach (var chemical in this.Inputs)    //for every input chemical of THIS reaction
        {
          var index = reactions.FindIndex(r => r.Output.Name == chemical.Name);    //look for a recipe reaction

          if (index != -1)   //if found
          {
            var r = reactions[index];

            if (r.Inputs.Count > 1)   //not ORE ... 1 input = ORE = simplified already
            {
              IndicesToRemove.Add(index);
              IndicesToReplace.Add(index);
            }
          }
        }

        //REPLACE

        //int needed = chemical.Multiple;
        //int recipeOffers = r.Output.Multiple;

        ////Find LCM/Common ground

        ////add
        //foreach (var item in r.Inputs)
        //{
        //  var existingIndex = this.Inputs.FindIndex(i => i.Name == item.Name);    //check if it exists in inputs already

        //  if (existingIndex != -1)   //if it does, just add multiples              
        //    this.Inputs[existingIndex].Multiple += item.Multiple;
        //  else
        //    this.Inputs.Add(item);
        //}






        //prepare for next iteration by removing all items in indicesToRemove
        for (int i = 0; i < IndicesToRemove.Count; i++)
          reactions.RemoveAt(i);

        //reset
        IndicesToRemove = new List<int>();
        IndicesToReplace = new List<int>();


        int count = 0;
        
        foreach (var r in reactions)
        {
          if (r.Inputs.Count > 1)
            count++;
        }

        if (count == 0)     // all reactions are reduded to just 1 input
          break;
      }



      //now count ORES and return

      return null;
    }
  }
}
