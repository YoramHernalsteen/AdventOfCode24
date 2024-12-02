using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode24.AdventDays
{
    public static class DayTwo
    {
        public static int Solve()
        {
            var data = Core.ConvertFileTo2dListInt("DayTwo");
            var safeLines = 0;
            foreach (var numberList in data)
            {
                if (IsNumberListSafe(numberList)) safeLines++;

            }
            return safeLines;
        }

        public static int SolveExtra()
        {
            var data = Core.ConvertFileTo2dListInt("DayTwo");
            var safeLines = 0;
            var failedNumberLists = new Dictionary<int, List<int>>();
            foreach (var numberList in data)
            {
                if (IsNumberListSafe(numberList))
                {
                    safeLines++;
                    continue;
                }
                for(var index = 0; index < numberList.Count; index++)
                {
                    var retryNumberList = numberList.ToList();
                    retryNumberList.RemoveAt(index);
                    if (IsNumberListSafe(retryNumberList))
                    {
                        safeLines++;
                        break;
                    }

                }
            }
            return safeLines;
        }

        private static bool IsNumberListSafe(List<int> numberList)
        {
            var isSafe = true;
            var errorIndex = -1;
            var movement = Movement.Increasing;
            for (int i = 0; i < numberList.Count - 1; i++)
            {
                var currentNumber = numberList[i];
                var nextNumber = numberList[i + 1];

                if (Math.Abs(currentNumber - nextNumber) > 3 || currentNumber == nextNumber)
                {
                    isSafe = false;
                    errorIndex = i;
                    break;
                }

                if (i == 0)
                {
                    if (currentNumber > nextNumber) movement = Movement.Decreasing;
                }
                else
                {
                    var nextMovement = currentNumber > nextNumber ? Movement.Decreasing : Movement.Increasing;
                    if (movement != nextMovement) { isSafe = false; errorIndex = i; break; }
                    movement = nextMovement;
                }
            }

            return isSafe;
        }

        private static void PrintResult(List<int> data, bool success, List<int> errorindexes, bool retry)
        {
            var str = string.Join(" ", data);
            if (success)
            {
                str = "SUCCESS " + str;
            } else
            {
                str = "FAILED " + str;
            }

            str = str + " ERRORS AT [" + string.Join(", ", errorindexes) + "]";

            if (retry)
            {
                str += " RETRY";
            }
            Console.WriteLine(str);
        }
    }


    public enum Movement
    {
        Increasing,
        Decreasing,
    }
}
