using System;
using System.Collections.Generic;

namespace Passwords
{
  class Program
  {
    static void Main()
    {
      int lb = 359282;
      int ub = 820401;

      Console.WriteLine("Possible passwords : {0}", PossiblePasswords(lb, ub));
    }

    private static int PossiblePasswords(int lowerBound, int upperBound)
    {
      List<int> possible = new List<int>();

      for (int i = lowerBound+1; i < upperBound; i++)
      {
        if (ConditionsAreMet(i))
          possible.Add(i);
      }

      return possible.Count;
    }

    private static bool ConditionsAreMet(int number)
    {
      bool digitsNeverDecrease = false;
      bool doubleExists = false;

      char[] chars = number.ToString().ToCharArray();
      int[] digits = new int[chars.Length];

      for (int i = 0; i < chars.Length; i++)
        digits[i] = int.Parse(chars[i].ToString());
                
      for (int i = 0; i < digits.Length-1; i++)
      {
        digitsNeverDecrease = true;
        if (digits[i] > digits[i + 1])
        {
          digitsNeverDecrease = false;
          break;
        }
      }

      for (int i = 0; i < digits.Length - 1; i++)
      {
        if (digits[i] == digits[i + 1])
        {
          doubleExists = true;
          break;
        }
      }

      return (doubleExists && digitsNeverDecrease);
    }
  }
}
