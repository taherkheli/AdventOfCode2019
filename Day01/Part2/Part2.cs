using System;
using System.IO;

namespace Part2
{
  class Part2
  {
    static void Main()
    {
      string inputFile = "input.txt";
      Calculate(inputFile);
    }

    private static void Calculate(string inputFile)
    {
      string line;
      long fuelNeeded = 0;
      long sum = 0;

      StreamReader file = new StreamReader(inputFile);

      while ((line = file.ReadLine()) != null)
      {
        fuelNeeded = CalculateFuelCumulatively(long.Parse(line));
        sum += fuelNeeded;
      }

      Console.WriteLine("\ntotal fuel needed was =  {0}", sum);
      file.Close();
    }

    private static long CalculateFuelCumulatively(long val)
    {
      long sum = 0;
      long next = val;

      while (true)
      {
        next = CalculateFuel(next);
        sum += next;

        if (next < 6)  //no point botherin
          break;
      }

      return sum;
    }

    private static long CalculateFuel(long val)
    {
      return ((val / 3) - 2);
    }
  }
}
