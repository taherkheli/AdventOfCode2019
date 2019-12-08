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
      long fuelNeeded;
      long sum = 0;

      StreamReader file = new StreamReader(inputFile);

      while ((line = file.ReadLine()) != null)
      {
        fuelNeeded = CalculateFuel(long.Parse(line));
        sum += fuelNeeded;
      }

      Console.WriteLine("\ntotal fuel needed was =  {0}", sum);
      file.Close();
    }

    private static long CalculateFuel(long val)
    {
      long result = 0;

      if (val > 6)
        result = (val/3)-2;

      return result;
    }
  }
}