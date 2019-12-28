using System.Collections.Generic;

namespace IntCodeExecutioner
{
  public static class PermutationCalculator
  {
    static List<List<int>> permutations;
    static bool[] used;

    public static List<List<int>> GetCombinations(int[] arr)
    {
      used = new bool[arr.Length];
      permutations = new List<List<int>>();
      List<int> c = new List<int>();
      GetPermutations(arr, 0, c);
      return permutations;
    }

    private static void GetPermutations(int[] arr, int colindex, List<int> c)
    {
      if (colindex >= arr.Length)
      {
        permutations.Add(new List<int>(c));
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
