namespace IntCodeExecutorNs
{
  public class Helpers
  {
    public static int GetHighest(int[] program, int[] sequence)
    {
      int highest = -1;
      var permutations = PermutationCalculator.GetCombinations(sequence);

      foreach (var permutation in permutations)
      {
        var temp = Calculate(program, permutation.ToArray());
        if (temp > highest)
          highest = temp;
      }

      return highest;
    }

    private static int Calculate(int[] program, int[] pattern)
    {
      IntCodeExecutor amp1 = new IntCodeExecutor(program, new int[] { pattern[0], 0 });
      amp1.Execute();
      IntCodeExecutor amp2 = new IntCodeExecutor(program, new int[] { pattern[1], (int)amp1.Output });
      amp2.Execute();
      IntCodeExecutor amp3 = new IntCodeExecutor(program, new int[] { pattern[2], (int)amp2.Output });
      amp3.Execute();
      IntCodeExecutor amp4 = new IntCodeExecutor(program, new int[] { pattern[3], (int)amp3.Output });
      amp4.Execute();
      IntCodeExecutor amp5 = new IntCodeExecutor(program, new int[] { pattern[4], (int)amp4.Output });
      amp5.Execute();

      return (int)amp5.Output;
    }
  }
}
