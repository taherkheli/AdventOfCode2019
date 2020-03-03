using System.Collections.Generic;

namespace FeedbackAmplifiers
{
  public static class PermutationCalculator
  {
    static List<List<long>> permutations;
    static bool[] used;

    public static List<List<long>> GetCombinations(long[] arr)
    {
      used = new bool[arr.Length];
      permutations = new List<List<long>>();
      List<long> c = new List<long>();
      GetPermutations(arr, 0, c);
      return permutations;
    }

    private static void GetPermutations(long[] arr, int colindex, List<long> c)
    {
      if (colindex >= arr.Length)
      {
        permutations.Add(new List<long>(c));
        return;
      }
      for (int i = 0; i < arr.Length; i++)
      {
        if (!used[i])
        {
          used[i] = true;
          c.Add(arr[i]);
          GetPermutations(arr, colindex + 1, c);
          c.RemoveAt(c.Count - 1);
          used[i] = false;
        }
      }
    }
  }
}
