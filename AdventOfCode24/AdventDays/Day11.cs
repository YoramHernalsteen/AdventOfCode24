using DotNetEnv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode24.AdventDays
{
    public static class Day11
    {
        public static ulong Solve(int blinks = 25)
        {
            var cache = new Dictionary<(ulong, int), ulong>();
            var numbers = Core.ConvertFileTo2dListInt(" ").First();
            var result = 0UL;
            foreach (var number in numbers)
            {
                result += CountForNumber((ulong)number, blinks, cache);
            }

            return result;
        }

        public static ulong SolveExtra()
        {
            return Solve(75);
        }

        private static ulong CountForNumber(ulong number, int blinks, Dictionary<(ulong, int), ulong> cache)
        {
            if (blinks == 0) return 1;

            if(cache.ContainsKey((number, blinks))) return cache[(number, blinks)];

            var result = 0UL;
            if (number == 0)
            {
                result =  CountForNumber(1, blinks - 1, cache);
                cache.Add((number, blinks), result);
                return result;
            }

            var evenDigitDivider = IsEvenAmountOfDigits(number);
            if (evenDigitDivider != 0)
            {
                result =  CountForNumber((number % evenDigitDivider), blinks - 1, cache) + CountForNumber((number / evenDigitDivider), blinks - 1, cache);
                cache.Add((number, blinks), result);
                return result;
            }
            result =  CountForNumber(number * 2024, blinks -1, cache);
            cache.Add((number, blinks), result);
            return result;
        }

        private static ulong IsEvenAmountOfDigits(ulong number)
        {
            switch (number)
            {
                case < 10: return 0;
                case < 100: return 10;
                case < 1000: return 0;
                case < 10000: return 100;
                case < 100000: return 0;
                case < 1000000: return 1000;
                case < 10000000: return 0 ;
                case < 100000000: return 10000;
                case < 1000000000: return 0;
                case < 10000000000: return 100000;
                case < 100000000000: return 0;
                case < 1000000000000: return 1000000;
                case < 10000000000000: return 0;
                case < 100000000000000: return 10000000;
                case < 1000000000000000: return 0;
                case < 10000000000000000: return 100000000;
                case < 100000000000000000: return 0;
                case < 1000000000000000000: return 1000000000;
                default: return 0; 
            }
        }
    }
}
