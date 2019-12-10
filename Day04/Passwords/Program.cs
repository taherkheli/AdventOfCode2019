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
      bool doubleExistsAndMeetsCriteria = false;

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

      if (digitsNeverDecrease)
      {
        for (int i = 0; i < digits.Length - 1; i++)
        {
          if (digits[i] == digits[i + 1])
          {
            if (CheckDoubles(digits[i], digits))
            {
              doubleExistsAndMeetsCriteria = true;
              break;
            }
            else
              continue;
          }
        }
      }

      return (doubleExistsAndMeetsCriteria && digitsNeverDecrease);
    }

    //will only be called when a double is found
    private static bool CheckDoubles(int digit, int[] digits)
    {
      int adjacancies = 2;   //or method won't be called
      int startIndex = 0;
      bool result = false;

      for (int i = 0; i < digits.Length; i++)
      {
        if ((digits[i] == digit) && (i<5))   //no point beyond index 4 as it means double is at index 5 and 6
        {
          startIndex = i + 2;
          break;
        }
      }
     
      for (int i = startIndex; i < digits.Length; i++)
      {
        if (digits[i] == digit)
          adjacancies++;
      }

      switch (adjacancies)
      {
        case 2:
          result = true;
          break;

        case 4:
          if ((digits[4] > digit) && (digits[4] == digits[5]))
          {
            result = true;
            break;
          }
          else
          {
            result = false;
            break;
          }

        case 3:
        case 5:
        default:
          result = false;
          break;
      }

      return result;
    }
  }
}
