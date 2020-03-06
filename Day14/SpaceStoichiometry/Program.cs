using System;
using System.IO;

namespace SpaceStoichiometry
{
  class Program
  {
    static void Main()
    {

      //https://stackoverflow.com/questions/769/solving-a-linear-equation

      string path = "input.txt";      
      var x = Parser.GetReactions(LoadInput(path));

    }


    private static string[] LoadInput(string path)
    {
      var file = new StreamReader(path).ReadToEnd();
      var lines = file.Split(Environment.NewLine);
      return lines;
    }




  }
}
