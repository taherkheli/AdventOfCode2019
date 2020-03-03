using IntCode;

namespace FeedbackAmplifiers
{
  public class Helpers
  {
    public static long GetHighest(long[] program, long[] sequence, int topology)
    {
      long highest = -1;
      var permutations = PermutationCalculator.GetCombinations(sequence);

      foreach (var permutation in permutations)
      {
        long temp;

        if (topology == 1)
          temp = CalculateSeriesTopology(program, permutation.ToArray());
        else
          temp = CalculateFeedbackTopology(program, permutation.ToArray());

        if (temp > highest)
          highest = temp;
      }

      return highest;
    }

    private static long CalculateSeriesTopology(long[] program, long[] pattern)
    {
      Executor amp1 = new Executor(program);
      amp1.InputQueue.Enqueue(pattern[0]);
      amp1.InputQueue.Enqueue((long)0);
      amp1.Execute();

      Executor amp2 = new Executor(program);
      amp2.InputQueue.Enqueue(pattern[1]);
      amp2.InputQueue.Enqueue((long)amp1.OutputQueue.Dequeue());
      amp2.Execute();

      Executor amp3 = new Executor(program);
      amp3.InputQueue.Enqueue(pattern[2]);
      amp3.InputQueue.Enqueue((long)amp2.OutputQueue.Dequeue());
      amp3.Execute();

      Executor amp4 = new Executor(program);
      amp4.InputQueue.Enqueue(pattern[3]);
      amp4.InputQueue.Enqueue((long)amp3.OutputQueue.Dequeue());
      amp4.Execute();

      Executor amp5 = new Executor(program);
      amp5.InputQueue.Enqueue(pattern[4]);
      amp5.InputQueue.Enqueue((long)amp4.OutputQueue.Dequeue());
      amp5.Execute();

      return (long)amp5.OutputQueue.Dequeue();
    }
       
    private static long CalculateFeedbackTopology(long[] input, long[] pattern)
    {
      Executor amp1 = new Executor(input);
      amp1.InputQueue.Enqueue(pattern[0]);
      amp1.InputQueue.Enqueue((long)0);
      amp1.Execute();

      Executor amp2 = new Executor(input);
      amp2.InputQueue.Enqueue(pattern[1]);
      amp2.InputQueue.Enqueue((long)amp1.OutputQueue.Dequeue());
      amp2.Execute();

      Executor amp3 = new Executor(input);
      amp3.InputQueue.Enqueue(pattern[2]);
      amp3.InputQueue.Enqueue((long)amp2.OutputQueue.Dequeue());
      amp3.Execute();

      Executor amp4 = new Executor(input);
      amp4.InputQueue.Enqueue(pattern[3]);
      amp4.InputQueue.Enqueue((long)amp3.OutputQueue.Dequeue());
      amp4.Execute();

      Executor amp5 = new Executor(input);
      amp5.InputQueue.Enqueue(pattern[4]);
      amp5.InputQueue.Enqueue((long)amp4.OutputQueue.Dequeue());
      amp5.Execute();

      if (amp5.AwaitingInput == false)
        return (long)amp5.OutputQueue.Dequeue();
      else 
      {
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
      }

      return (long)amp5.OutputQueue.Dequeue();
    }
  }
}