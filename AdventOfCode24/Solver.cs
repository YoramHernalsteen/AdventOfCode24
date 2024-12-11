using AdventOfCode24.AdventDays;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode24;

public class Solver
{
    [Benchmark]
    public void PartOne()
    {
        Console.WriteLine($"EASY: {Day11.Solve()}");
    }
    
    [Benchmark]
    public void PartTwo()
    {
        Console.WriteLine($"EXTRA: {Day11.SolveExtra()}");
    }
}