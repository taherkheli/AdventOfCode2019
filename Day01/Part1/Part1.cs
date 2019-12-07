using System;
using System.IO;

namespace Part1
{
  class Part1
  {
    static void Main()
    {
      string inputFile = Path.GetFullPath(@" ..\..\..\..\..\..\input.txt");
      Calculate(inputFile);
    }

    private static void Calculate(string inputFile)
    {
      string line;
      ulong fuelNeeded = 0;
      ulong sum = 0;

      StreamReader file = new StreamReader(inputFile);

      while ((line = file.ReadLine()) != null)
      {
        fuelNeeded = CalculateFuel(ulong.Parse(line));
        sum += fuelNeeded;
      }

      Console.WriteLine("\ntotal fuel needed was =  {0}", sum);
      file.Close();
    }

    private static ulong CalculateFuel(ulong val)
    {
      ulong result = 0;

      result = val/3;

      if (result > 2)   //avoid -ve
        result -= 2;

      if (result == 1)  //special case; avoid returning 1
        result = 0;

      return result;
    }
  }
}