using System;
using System.IO;

namespace N_BodyProblem
{
  class Program
  {
    static void Main()
    {
      int timeSteps = 1001;
      string path = "input.txt";
      Position[] positions = LoadInput(path); 
      Moon[] moons = new Moon[positions.Length];

      for (int i = 0; i < moons.Length; i++)
        moons[i]= new Moon() { Position = positions[i] } ;

      for (int i = 0; i < timeSteps; i++)
      {
        Console.WriteLine("\nAfter {0} steps:", i);
        for (int k = 0; k < moons.Length; k++)
          Console.WriteLine("pos=<   x={0, 4},   y={1,4},    z={2,4}   >,      vel=<   x={3, 4},    y={4, 4},    z={5, 4}   >", moons[k].Position.X, moons[k].Position.Y, moons[k].Position.Z,
                                                                                                                                moons[k].Velocity.X, moons[k].Velocity.Y, moons[k].Velocity.Z);
        if (i == (timeSteps - 1))
          break;

        //update velocity of each moon
        for (int j = 0; j < moons.Length; j++)
          for (int k = 0; k < moons.Length; k++)
            moons[j].ApplyGravity(moons[k]);
        
        //update position of each moon as per new velocity
        for (int k = 0; k < moons.Length; k++)
          moons[k].ApplyVelocity();
      }

      int totalEnergy = 0;
      for (int i = 0; i < moons.Length; i++)
        totalEnergy += moons[i].GetTotalEnergy();

      Console.WriteLine("\nSum of total energ after {0} time steps: {1}", timeSteps - 1, totalEnergy);
    }

    private static Position[] LoadInput(string path)
    {
      Position[] result;

      var file = new StreamReader(path).ReadToEnd();
      var lines = file.Split(new char[] { '\n' });
      result = new Position[lines.Length];

      for (int i = 0; i < lines.Length; i++)
        result[i] = ParseIt(lines[i]);

      return result;
    }

    private static Position ParseIt(string s)
    {
      var start = s.IndexOf('x') + 2;
      var end = s.IndexOf(',');
      var x = Int32.Parse(s[start..end]);

      start = s.IndexOf('y') + 2;
      end = s.IndexOf(',', end +1);
      int y = Int32.Parse(s[start..end]);

      start = (s.IndexOf('z')) + 2;
      end = (s.IndexOf('>', end + 1));
      int z = Int32.Parse(s[start..end]);

      return new Position(x, y, z);
    }
  }
}
