using System;

namespace IntCodeExecutorPartII
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
      int[] amp1Program = new int[program.Length];
      int[] amp2Program = new int[program.Length];
      int[] amp3Program = new int[program.Length];
      int[] amp4Program = new int[program.Length];
      int[] amp5Program = new int[program.Length];
      Array.Copy(program, amp1Program, program.Length);
      Array.Copy(program, amp2Program, program.Length);
      Array.Copy(program, amp3Program, program.Length);
      Array.Copy(program, amp4Program, program.Length);
      Array.Copy(program, amp5Program, program.Length);

      IntCodeExecutor amp1 = new IntCodeExecutor(amp1Program);
      amp1.InputQueue.Enqueue(pattern[0]);
      amp1.InputQueue.Enqueue(0);
      amp1.Execute();
      IntCodeExecutor amp2 = new IntCodeExecutor(amp2Program);
      amp2.InputQueue.Enqueue(pattern[1]);
      amp2.InputQueue.Enqueue(amp1.Output);
      amp2.Execute();
      IntCodeExecutor amp3 = new IntCodeExecutor(amp3Program);
      amp3.InputQueue.Enqueue(pattern[2]);
      amp3.InputQueue.Enqueue(amp2.Output);
      amp3.Execute();
      IntCodeExecutor amp4 = new IntCodeExecutor(amp4Program);
      amp4.InputQueue.Enqueue(pattern[3]);
      amp4.InputQueue.Enqueue(amp3.Output);
      amp4.Execute();
      IntCodeExecutor amp5 = new IntCodeExecutor(amp5Program);
      amp5.InputQueue.Enqueue(pattern[4]);
      amp5.InputQueue.Enqueue(amp4.Output);
      amp5.Execute();

      while (true)
      {
        amp1.InputQueue.Enqueue(amp5.Output);
        amp1.ResumeExecution();
        amp2.InputQueue.Enqueue(amp1.Output);
        amp2.ResumeExecution();
        amp3.InputQueue.Enqueue(amp2.Output);
        amp3.ResumeExecution();
        amp4.InputQueue.Enqueue(amp3.Output);
        amp4.ResumeExecution();
        amp5.InputQueue.Enqueue(amp4.Output);
        amp5.ResumeExecution();

        if (!amp5.AwaitingInput)
          break;
      }

      return (int)amp5.Output;
    }
  }
}
