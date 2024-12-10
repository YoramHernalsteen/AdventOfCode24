using AdventOfCode24.AdventDays;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode24;

public class Solver
{
    [Benchmark]
    public void PartOne()
    {
        Console.WriteLine($"EASY: {Day6Optimized.Solve()}");
    }
    
    [Benchmark]
    public void PartTwo()
    {
        Console.WriteLine($"EXTRA: {Day6Optimized.SolveExtra()}");
    }
}