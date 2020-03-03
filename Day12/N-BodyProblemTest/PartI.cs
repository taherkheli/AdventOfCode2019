using Microsoft.VisualStudio.TestTools.UnitTesting;
using N_BodyProblem;
using System;

namespace N_BodyProblemTest
{
  [TestClass]
  public class PartI
  {
    [TestMethod]
    public void TotalEnergyAfter_10_StepsShouldBe_179()
    {
      int timeSteps = 11;   //must be 1 more than needed steps
      int totalEnergyExpected = 179;

      Position[] positions = new Position[4] { new Position( -1, 0, 2),
                                               new Position( 2, -10, -7),
                                               new Position( 4, -8, 8),
                                               new Position( 3, 5, -1)};

      Position[] expectedPositions = new Position[4] { new Position( 2, 1, -3),
                                                       new Position( 1, -8, 0),
                                                       new Position( 3, -6, 1),
                                                       new Position( 2, 0, 4)};

      Velocity[] expectedVelocities = new Velocity[4] { new Velocity( -3, -2, 1),
                                                        new Velocity( -1, 1, 3),
                                                        new Velocity( 3, 2, -3),
                                                        new Velocity( 1, -1, -1)};

      Moon[] moons = new Moon[positions.Length];
      for (int i = 0; i < moons.Length; i++)
        moons[i] = new Moon() { Position = positions[i] };

      for (int i = 0; i < timeSteps; i++)
      {       
        if (i == (timeSteps - 1))
          break;

        for (int j = 0; j < moons.Length; j++)
          for (int k = 0; k < moons.Length; k++)
            moons[j].ApplyGravity(moons[k]);

        foreach (var moon in moons)
          moon.ApplyVelocity();
      }

      int totalEnergyActual = 0;
      for (int i = 0; i < moons.Length; i++)
        totalEnergyActual += moons[i].GetTotalEnergy();

      Console.WriteLine("\nSum of total energ after {0} time steps: {1}", timeSteps - 1, totalEnergyActual);

      Position[] actualPositions = new Position[4] { moons[0].Position,
                                                     moons[1].Position,
                                                     moons[2].Position,
                                                     moons[3].Position };

      Velocity[] actualVelocities = new Velocity[4] {moons[0].Velocity,
                                                     moons[1].Velocity,
                                                     moons[2].Velocity,
                                                     moons[3].Velocity };

      CollectionAssert.AreEqual(expectedPositions, actualPositions, "OK!");
      CollectionAssert.AreEqual(expectedVelocities, actualVelocities, "OK!");
      Assert.AreEqual(totalEnergyExpected, totalEnergyActual, "OK!");
    }

    [TestMethod]
    public void TotalEnergyAfter_100_StepsShouldBe_1940()
    {
      int timeSteps = 101;  
      int totalEnergyExpected = 1940;

      Position[] positions = new Position[4] { new Position( -8, -10, 0),
                                               new Position( 5, 5, 10),
                                               new Position( 2, -7, 3),
                                               new Position( 9, -8, -3)};

      Position[] expectedPositions = new Position[4] { new Position( 8, -12, -9),
                                                       new Position( 13, 16, -3),
                                                       new Position( -29, -11, -1),
                                                       new Position( 16, -13, 23)};

      Velocity[] expectedVelocities = new Velocity[4] { new Velocity( -7, 3, 0),
                                                        new Velocity( 3, -11, -5),
                                                        new Velocity( -3, 7, 4),
                                                        new Velocity( 7, 1, 1)};

      Moon[] moons = new Moon[positions.Length];
      for (int i = 0; i < moons.Length; i++)
        moons[i] = new Moon() { Position = positions[i] };

      for (int i = 0; i < timeSteps; i++)
      {
        if (i == (timeSteps - 1))
          break;

        for (int j = 0; j < moons.Length; j++)
          for (int k = 0; k < moons.Length; k++)
            moons[j].ApplyGravity(moons[k]);

        foreach (var moon in moons)
          moon.ApplyVelocity();
      }

      int totalEnergyActual = 0;
      for (int i = 0; i < moons.Length; i++)
        totalEnergyActual += moons[i].GetTotalEnergy();

      Console.WriteLine("\nSum of total energ after {0} time steps: {1}", timeSteps - 1, totalEnergyActual);

      Position[] actualPositions = new Position[4] { moons[0].Position,
                                                     moons[1].Position,
                                                     moons[2].Position,
                                                     moons[3].Position };

      Velocity[] actualVelocities = new Velocity[4] {moons[0].Velocity,
                                                     moons[1].Velocity,
                                                     moons[2].Velocity,
                                                     moons[3].Velocity };

      CollectionAssert.AreEqual(expectedPositions, actualPositions, "OK!");
      CollectionAssert.AreEqual(expectedVelocities, actualVelocities, "OK!");
      Assert.AreEqual(totalEnergyExpected, totalEnergyActual, "OK!");
    }
  }
}
