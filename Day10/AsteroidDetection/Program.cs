using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AsteroidDetection
{
  class Program
  {
    static void Main()
    {
      string path = "input.txt";
      var lines = File.ReadAllLines(path).ToList<string>();


      var grid = new Grid(lines);

    }


  }
}
