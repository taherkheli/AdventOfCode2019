using System;

namespace UniversalOrbitMap
{
  class Program
  {
    static void Main()
    {
      string path = "input.txt";
      Tree tree = new Tree(path);
      Console.WriteLine("\nTotal orbits direct and indirect are: {0}", tree.GetTotalOrbits());
      Console.WriteLine("\nTotal orbital transfers needed to get to Santa are: {0}", tree.GetOrbitalTransfersToSanta());
    }
  }
}