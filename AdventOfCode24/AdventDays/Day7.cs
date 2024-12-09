namespace AdventOfCode24.AdventDays;

public static class Day7
{
    // probably could be faster if working backwards and using recursion iso calculating possibilities ahead of time...
    // total must be divisible by next
    // total and next must be concatable
    // total minus next must be positive
    public static long Solve(bool solvePartTwo = false)
    {
        var data = Core.ConvertFileToLines();
        var equations = GetEquations(data);
        var result = 0L;
        foreach (var (equationResult, numbers) in equations)
        {
            var possibilites = new List<List<char>>();
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                if (possibilites.Count == 0)
                {
                    possibilites.Add(new List<char>(){'+'});
                    possibilites.Add(new List<char>(){'*'});
                    if (solvePartTwo)
                    {
                        possibilites.Add(new List<char>(){'|'});
                    }
                }
                else
                {
                    // we add the plus operator to the existing possibilities
                    // for the concat and multiplication operator, we copy the already existing possibilities
                    // reason is with every new number, we add 2(solve) or 3 (SolveExtra) more possibilities
                    // we have a theoretical amount of 2^N possibilities for part one
                    // we have a theoretical amount of 3^N possibilities for part two
                    
                    var newPossibilities = new List<List<char>>(){};
                    var newPossibilities2 = new List<List<char>>(){};
                    foreach (var possibility in possibilites)
                    {
                        var newPossibility = new List<char>(possibility);
                        newPossibility.Add('*');
                        newPossibilities.Add(newPossibility);
                        
                        if (solvePartTwo)
                        {
                            var newPossibility2 = new List<char>(possibility);
                            newPossibility2.Add('|');
                            newPossibilities2.Add(newPossibility2);
                        }
                        
                        possibility.Add('+');
                    }
                    possibilites.AddRange(newPossibilities);
                    if(solvePartTwo) possibilites.AddRange(newPossibilities2);
                }
            }
            if (HasCorrectEquation(possibilites, numbers, equationResult))
            {
                result += equationResult;
            }
            
        }
        return result;
    }

    public static long SolveExtra()
    {
        return Solve(true);
    }

    private static List<(long, List<int>)> GetEquations(List<string> lines)
    {
        var equations = new List<(long, List<int>)>();
        foreach (var line in lines)
        {
            var splitLine = line.Split(":");
            var result = long.Parse(splitLine[0]);
            var numbers = splitLine[1].TrimStart().Split(" ").Select(x => x.Trim()).Select(int.Parse).ToList();
            equations.Add((result, numbers));
        }
        return equations;
    }

    private static long Calculate(long number1, long number2, char operatorChar)
    {
        return operatorChar switch
        {
            '+' => number1 + number2,
            '*' => number1 * number2,
            '|' => (number1 * RoundDownToNearestBase10(number2)) + number2,
            _ => 0
        };
    }

    private static long RoundDownToNearestBase10(long number)
    {
        if (number < 10) return 10;
        if (number < 100) return 100;
        if (number < 1000) return 1000;
        if (number < 10000) return 10000;
        if (number < 100000) return 100000;
        if (number < 1000000) return 1000000;
        if (number < 10000000) return 10000000;
        if (number < 100000000) return 100000000;
        if (number < 10000000000) return 10000000000;
        if (number < 1000000000000) return 1000000000000;
        if (number < 10000000000000) return 10000000000000;
        else return 0;
    }

    private static bool HasCorrectEquation(List<List<char>> equations, List<int> numbers, long expectedNumber)
    {
        foreach (var equation in equations)
        {
            var result = 0L;
            for (var i = 0; i < equation.Count; i++)
            {
                var operatorChar = equation[i];
                long number = numbers[i + 1];

                if (i == 0)
                {
                    long firstNumber = numbers[i];
                    result = Calculate(firstNumber, number, operatorChar);
                }
                else
                {
                    result = Calculate(result, number, operatorChar);
                }
                
                if(result > expectedNumber) break;
            }
            
            if (result == expectedNumber) return true;
        }
        return false;
    }
}