using System;
using System.IO;

namespace SpaceStoichiometry
{
  class Program
  {
    static void Main()
    {
      string path = "input.txt";      
      var reactions = Parser.GetReactions(LoadInput(path));
      var reaction = reactions.Find(r => r.Output.Name == "FUEL");
      if (reaction != null)
        reactions.Remove(reaction);

      reaction.Substitute(reactions);





    }


    private static string[] LoadInput(string path)
    {
      var file = new StreamReader(path).ReadToEnd();
      var lines = file.Split(Environment.NewLine);
      return lines;
    }




  }
}
