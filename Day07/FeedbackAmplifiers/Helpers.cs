using IntCode;
using System;

namespace FeedbackAmplifiers
{
  public class Helpers
  {
    public static long GetHighestPartI(long[] program, int[] sequence)
    {
      long highest = -1;
      var permutations = PermutationCalculator.GetCombinations(sequence);

      foreach (var permutation in permutations)
      {
        var temp = CalculatePartI(program, permutation.ToArray());
        if (temp > highest)
          highest = temp;
      }

      return highest;
    }

    private static long CalculatePartI(long[] program, int[] pattern)
    {
      long[] pattern2 = new long[pattern.Length];
      for (int i = 0; i < pattern.Length; i++)
      {
        pattern2[i] = (long)pattern[i];
      }

      Executor amp1 = new Executor(program);
      amp1.InputQueue.Enqueue(pattern2[0]);
      amp1.InputQueue.Enqueue((long)0);
      amp1.Execute();

      Executor amp2 = new Executor(program);
      amp2.InputQueue.Enqueue(pattern2[1]);
      amp2.InputQueue.Enqueue((long)amp1.OutputQueue.Dequeue());
      amp2.Execute();

      Executor amp3 = new Executor(program);
      amp3.InputQueue.Enqueue(pattern2[2]);
      amp3.InputQueue.Enqueue((long)amp2.OutputQueue.Dequeue());
      amp3.Execute();

      Executor amp4 = new Executor(program);
      amp4.InputQueue.Enqueue(pattern2[3]);
      amp4.InputQueue.Enqueue((long)amp3.OutputQueue.Dequeue());
      amp4.Execute();

      Executor amp5 = new Executor(program);
      amp5.InputQueue.Enqueue(pattern2[4]);
      amp5.InputQueue.Enqueue((long)amp4.OutputQueue.Dequeue());
      amp5.Execute();


      return (long)amp5.OutputQueue.Dequeue();
    }
    
    public static long GetHighest(long[] program, int[] sequence)
    {
      long highest = -1;
      var permutations = PermutationCalculator.GetCombinations(sequence);

      foreach (var permutation in permutations)
      {
        var temp = Calculate(program, permutation.ToArray());
        if (temp > highest)
          highest = temp;
      }

      return highest;
    }

    private static long Calculate(long[] input, int[] pattern)
    {
      long[] pattern2 = new long[pattern.Length];
      for (int i = 0; i < pattern.Length; i++)
      {
        pattern2[i] = (long)pattern[i];
      }

      Executor amp1 = new Executor(input);
      amp1.InputQueue.Enqueue(pattern2[0]);
      amp1.InputQueue.Enqueue((long)0);
      amp1.Execute();

      Executor amp2 = new Executor(input);
      amp2.InputQueue.Enqueue(pattern2[1]);
      amp2.InputQueue.Enqueue((long)amp1.OutputQueue.Dequeue());
      amp2.Execute();

      Executor amp3 = new Executor(input);
      amp3.InputQueue.Enqueue(pattern2[2]);
      amp3.InputQueue.Enqueue((long)amp2.OutputQueue.Dequeue());
      amp3.Execute();

      Executor amp4 = new Executor(input);
      amp4.InputQueue.Enqueue(pattern2[3]);
      amp4.InputQueue.Enqueue((long)amp3.OutputQueue.Dequeue());
      amp4.Execute();

      Executor amp5 = new Executor(input);
      amp5.InputQueue.Enqueue(pattern2[4]);
      amp5.InputQueue.Enqueue((long)amp4.OutputQueue.Dequeue());
      amp5.Execute();

      while (true)
      {
        amp1.InputQueue.Enqueue((long)amp5.OutputQueue.Dequeue());
        amp1.ResumeExecution();
        amp2.InputQueue.Enqueue((long)amp1.OutputQueue.Dequeue());
        amp2.ResumeExecution();
        amp3.InputQueue.Enqueue((long)amp2.OutputQueue.Dequeue());
        amp3.ResumeExecution();
        amp4.InputQueue.Enqueue((long)amp3.OutputQueue.Dequeue());
        amp4.ResumeExecution();
        amp5.InputQueue.Enqueue((long)amp4.OutputQueue.Dequeue());
        amp5.ResumeExecution();

        if (!amp5.AwaitingInput)
          break;
      }

      return (long)amp5.OutputQueue.Dequeue();
    }
  }
}