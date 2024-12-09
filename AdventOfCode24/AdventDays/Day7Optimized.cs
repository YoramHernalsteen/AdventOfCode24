namespace AdventOfCode24.AdventDays;

public static class Day7Optimized
{
    // Divide, Subtract and Deconcat both try to find N-2 from calling the function (N, N-1)
    // If we keep on doing this and the end result is 0, we have a match.
    // We return -1 in these function if the operation is not possible, since this allows us to prune the possibilities
    
    
    public static long Solve(bool part2 = false)
    {
        var equations = GetEquations(Core.ConvertFileToLines());
        var result = 0L;
        
        var operations = new List<Func<long, long, long>>() { Divide, Subtract };
        if (part2) operations.Add(Deconcat);
        
        foreach (var (equationResult, equation) in equations)
        {
            if (IsEquationValid(equation, equationResult, operations)) result += equationResult;
        }
        
        return result;
    }

    public static long SolveExtra()
    {
        return Solve(part2:true);
    }
    
    private static long Divide(long a, long b)
    {
        if (a % b != 0) return -1;
        return a / b;
    }

    private static long Subtract(long a, long b)
    {
        return a - b;
    }

    private static long Deconcat(long a, long b)
    {
        var result = a - b;
        if(result < 0) return -1;
        return Divide(result, RoundDownToNearestBase10(b));
    }
    
    private static List<(long, List<long>)> GetEquations(List<string> lines)
    {
        var equations = new List<(long, List<long>)>();
        foreach (var line in lines)
        {
            var splitLine = line.Split(":");
            var result = long.Parse(splitLine[0]);
            var numbers = splitLine[1].TrimStart().Split(" ").Select(x => x.Trim()).Select(long.Parse).ToList();
            equations.Add((result, numbers));
        }
        return equations;
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
        else return 100000000000000;
    }
    
    private static bool IsEquationValid(List<long> equation, long expectedResult, List<Func<long, long, long>> operations )
    {
        var toCalculate = new Stack<(long, int)>();
        toCalculate.Push((expectedResult, equation.Count - 1));
        while (toCalculate.Count > 0)
        {
            var (result, nextIndex) = toCalculate.Pop();
            var nextValue = equation[nextIndex];
            foreach (var operation in operations)
            {
                var operationResult = operation(result, nextValue);
                
                if (nextIndex == 0 && operationResult == 0) return true;

                if (operationResult > 0 && nextIndex > 0)
                {
                    toCalculate.Push((operationResult, nextIndex - 1));
                }
            }
        }
        
        return false;
    }
    
}