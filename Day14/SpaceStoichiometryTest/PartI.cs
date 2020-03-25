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

    [TestMethod]
    public void TC02()
    {
      int expected = 165;
      string[] input = new string[]
      {
        "9 ORE => 2 A",
        "8 ORE => 3 B",
        "7 ORE => 5 C",
        "3 A, 4 B => 1 AB",
        "5 B, 7 C => 1 BC",
        "4 C, 1 A => 1 CA",
        "2 AB, 3 BC, 4 CA => 1 FUEL"
      };

      var reactions = Parser.GetReactions(input);
      var fuelReaction = reactions.Find(r => r.Output.Name == "FUEL");
      if (fuelReaction != null)
        reactions.Remove(fuelReaction);

      int actual = fuelReaction.CalculateNeededOre(reactions);

      Assert.AreEqual(expected, actual, "OK!");
    }

    [TestMethod]
    public void TC03()
    {
      int expected = 13312;
      string[] input = new string[]
      {
        "157 ORE => 5 NZVS",
        "165 ORE => 6 DCFZ",
        "44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL",
        "12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ",
        "179 ORE => 7 PSHF",
        "177 ORE => 5 HKGWZ",
        "7 DCFZ, 7 PSHF => 2 XJWVT",
        "165 ORE => 2 GPVTF",
        "3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT"
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
