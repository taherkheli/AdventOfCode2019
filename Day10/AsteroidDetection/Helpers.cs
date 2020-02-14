using System.Collections.Generic;

namespace AsteroidDetection
{
  public static class Helpers
  {
    public static List<T> RemoveDuplicates<T>(List<T> list)
    {
      var result = new List<T>();
      var set = new HashSet<T>();
     
      for (int i = 0; i < list.Count; i++)
      {
        if (!set.Contains(list[i]))
        {
          result.Add(list[i]);
          set.Add(list[i]);
        }
      }
      return result;
    }
  }
}