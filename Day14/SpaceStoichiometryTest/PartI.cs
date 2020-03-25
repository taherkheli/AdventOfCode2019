using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceStoichiometry;

namespace SpaceStoichiometryTest
{
  [TestClass]
  public class PartI
  {
    [TestMethod]
    public void TC01()
    { 
      int expected = 31;
      string[] input = new string[]
      {
        "10 ORE => 10 A",
        "1 ORE => 1 B",
        "7 A, 1 B => 1 C",
        "7 A, 1 C => 1 D",
        "7 A, 1 D => 1 E",
        "7 A, 1 E => 1 FUEL"
      };

      var reactions = Parser.GetReactions(input);
      var fuelReaction = reactions.Find(r => r.Output.Name == "FUEL");
      if (fuelReaction != null)
        reactions.Remove(fuelReaction);

      int actual = fuelReaction.CalculateNeededOre(reactions);

      Assert.AreEqual(expected, actual, "OK!");
    }
  }
}
