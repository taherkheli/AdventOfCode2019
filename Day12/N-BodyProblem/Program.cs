using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace N_BodyProblem
{


  class Program
  {
    static void Main()
    {
      string path = "input.txt";
      Position[] positions = LoadInput(path);
      Moon[] moons = new Moon[positions.Length];

      List<int[]> Snapshots = new List<int[]> { };

      //List<int[]> SnapshotsX = new List<int[]> { };
      //List<int[]> SnapshotsY = new List<int[]> { };
      //List<int[]> SnapshotsZ = new List<int[]> { };

      int[] vector;

      //int[] vectorX;
      //int[] vectorY;
      //int[] vectorZ;

      for (int i = 0; i < moons.Length; i++)
        moons[i] = new Moon() { Position = positions[i] };

      Snapshots.Add(GetState(moons));

      //SnapshotsX.Add(GetStateX(moons));
      //SnapshotsY.Add(GetStateY(moons));
      //SnapshotsZ.Add(GetStateZ(moons));

      //int xMatched = -1;
      //int yMatched = -1;
      //int zMatched = -1;


      long count = 0;

      while (true)
      {
        Console.WriteLine("step# {0}", ++count);

        //update velocity of each moon
        for (int j = 0; j < moons.Length; j++)
          for (int k = 0; k < moons.Length; k++)
            moons[j].ApplyGravity(moons[k]);

        //update position of each moon as per new velocity
        for (int k = 0; k < moons.Length; k++)
          moons[k].ApplyVelocity();

        //take snapshot
        vector = GetState(moons);

        ////takesnapshot X
        //vectorX = GetStateX(moons);
        //vectorY = GetStateY(moons);
        //vectorZ = GetStateZ(moons);


        //check if its bein repeated
        if (Repeated(vector, Snapshots))
          break;
        else
          Snapshots.Add(vector);

        //xMatched = RepeatedX(vectorX, SnapshotsX);

        //if (xMatched > -1)
        //  yMatched = RepeatedY(vectorY, SnapshotsY);


        //if (yMatched > -1)
        //  zMatched = RepeatedZ(vectorZ, SnapshotsZ);


        //if ((xMatched > -1) && (yMatched > -1) && (zMatched > -1) && (xMatched == yMatched) && (yMatched == zMatched))
        //  break;
        //else
        //{
        //  SnapshotsX.Add(vectorX);
        //  SnapshotsY.Add(vectorY);
        //  SnapshotsZ.Add(vectorZ);
        //}

      }

      Console.WriteLine("\nRepetition detected after {0} steps", count);
    }

    private static int[] GetState(Moon[] moons)
    {
      int[] result = new int[24];
      result.Initialize();

      for (int k = 0; k < moons.Length; k++)
      {
        result[k * 6 + 0] = moons[k].Position.X;
        result[k * 6 + 1] = moons[k].Position.Y;
        result[k * 6 + 2] = moons[k].Position.Z;
        result[k * 6 + 3] = moons[k].Velocity.X;
        result[k * 6 + 4] = moons[k].Velocity.Y;
        result[k * 6 + 5] = moons[k].Velocity.Z;
      }

      return result;
    }

    //private static int[] GetStateX(Moon[] moons)
    //{
    //  int[] result = new int[8];
    //  result.Initialize();

    //  for (int k = 0; k < moons.Length; k++)
    //  {
    //    result[k * 2 + 0] = moons[k].Position.X;
    //    result[k * 2 + 1] = moons[k].Velocity.X;
    //  }

    //  return result;
    //}
    //private static int[] GetStateY(Moon[] moons)
    //{
    //  int[] result = new int[8];
    //  result.Initialize();

    //  for (int k = 0; k < moons.Length; k++)
    //  {
    //    result[k * 2 + 0] = moons[k].Position.Y;
    //    result[k * 2 + 1] = moons[k].Velocity.Y;
    //  }

    //  return result;
    //}
    //private static int[] GetStateZ(Moon[] moons)
    //{
    //  int[] result = new int[8];
    //  result.Initialize();

    //  for (int k = 0; k < moons.Length; k++)
    //  {
    //    result[k * 2 + 0] = moons[k].Position.Z;
    //    result[k * 2 + 1] = moons[k].Velocity.Z;
    //  }

    //  return result;
    //}

    private static bool Repeated(int[] snapshot, List<int[]> snapshots)
    {
      bool matchFound = false;

      for (int i = 0; i < snapshots.Count; i++)
      {
        if (snapshot.SequenceEqual(snapshots[i]))
        {
          matchFound = true;
          Console.WriteLine("snapshots {0} and {1} were identical", i, snapshots.Count + 1);
        }
      }

      return matchFound;
    }

    //private static int RepeatedX(int[] snapshot, List<int[]> snapshotsX)
    //{
    //  int result = -1;

    //  for (int i = 0; i < snapshotsX.Count; i++)
    //  {
    //    if (snapshot.SequenceEqual(snapshotsX[i]))
    //    {
    //      result = i;
    //      Console.WriteLine("snapshotsX {0} and {1} were identical", i, snapshotsX.Count + 1);
    //      break;
    //    }
    //  }

    //  return result;
    //}
    //private static int RepeatedY(int[] snapshot, List<int[]> snapshotsY)
    //{
    //  int result = -1;

    //  for (int i = 0; i < snapshotsY.Count; i++)
    //  {
    //    if (snapshot.SequenceEqual(snapshotsY[i]))
    //    {
    //      result = i;
    //      Console.WriteLine("snapshotsY {0} and {1} were identical", i, snapshotsY.Count + 1);
    //      break;
    //    }
    //  }

    //  return result;
    //}
    //private static int RepeatedZ(int[] snapshot, List<int[]> snapshotsZ)
    //{
    //  int result = -1;

    //  for (int i = 0; i < snapshotsZ.Count; i++)
    //  {
    //    if (snapshot.SequenceEqual(snapshotsZ[i]))
    //    {
    //      result = i;
    //      Console.WriteLine("snapshotsZ {0} and {1} were identical", i, snapshotsZ.Count + 1);
    //      break;
    //    }
    //  }

    //  return result;
    //}

    private static Position[] LoadInput(string path)
    {
      Position[] result;

      var file = new StreamReader(path).ReadToEnd();
      var lines = file.Split(Environment.NewLine);
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
      end = s.IndexOf(',', end + 1);
      int y = Int32.Parse(s[start..end]);

      start = (s.IndexOf('z')) + 2;
      end = (s.IndexOf('>', end + 1));
      int z = Int32.Parse(s[start..end]);

      return new Position(x, y, z);
    }
  }


  //class Program
  //{
  //  static void Main()
  //  {
  //    int timeSteps = 1001;
  //    string path = "input.txt";
  //    Position[] positions = LoadInput(path);
  //    Moon[] moons = new Moon[positions.Length];

  //    for (int i = 0; i < moons.Length; i++)
  //      moons[i] = new Moon() { Position = positions[i] };

  //    for (int i = 0; i < timeSteps; i++)
  //    {
  //      Console.WriteLine("\nAfter {0} steps:", i);
  //      for (int k = 0; k < moons.Length; k++)
  //        Console.WriteLine("pos=<   x={0, 4},   y={1,4},    z={2,4}   >,      vel=<   x={3, 4},    y={4, 4},    z={5, 4}   >", moons[k].Position.X, moons[k].Position.Y, moons[k].Position.Z,
  //                                                                                                                              moons[k].Velocity.X, moons[k].Velocity.Y, moons[k].Velocity.Z);
  //      if (i == (timeSteps - 1))
  //        break;

  //      //update velocity of each moon
  //      for (int j = 0; j < moons.Length; j++)
  //        for (int k = 0; k < moons.Length; k++)
  //        {
  //          moons[j].ApplyGravityX(moons[k].Position.X);
  //          moons[j].ApplyGravityY(moons[k].Position.Y);
  //          moons[j].ApplyGravityZ(moons[k].Position.Z);
  //        }


  //      //update position of each moon as per new velocity
  //      for (int k = 0; k < moons.Length; k++)
  //      {
  //        moons[k].ApplyVelocityX();
  //        moons[k].ApplyVelocityY();
  //        moons[k].ApplyVelocityZ();
  //      }
  //    }

  //    int totalEnergy = 0;
  //    for (int i = 0; i < moons.Length; i++)
  //      totalEnergy += moons[i].GetTotalEnergy();

  //    Console.WriteLine("\nSum of total energ after {0} time steps: {1}", timeSteps - 1, totalEnergy);
  //  }

  //  private static Position[] LoadInput(string path)
  //  {
  //    Position[] result;

  //    var file = new StreamReader(path).ReadToEnd();
  //    var lines = file.Split(Environment.NewLine);
  //    result = new Position[lines.Length];

  //    for (int i = 0; i < lines.Length; i++)
  //      result[i] = ParseIt(lines[i]);

  //    return result;
  //  }

  //  private static Position ParseIt(string s)
  //  {
  //    var start = s.IndexOf('x') + 2;
  //    var end = s.IndexOf(',');
  //    var x = Int32.Parse(s[start..end]);

  //    start = s.IndexOf('y') + 2;
  //    end = s.IndexOf(',', end + 1);
  //    int y = Int32.Parse(s[start..end]);

  //    start = (s.IndexOf('z')) + 2;
  //    end = (s.IndexOf('>', end + 1));
  //    int z = Int32.Parse(s[start..end]);

  //    return new Position(x, y, z);
  //  }
  //}
}
