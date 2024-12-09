using AdventOfCode24.AdventDays;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode24;

public class Solver
{
    [Benchmark]
    public void PartOne()
    {
        Console.WriteLine($"EASY: {Day7.Solve()}");
    }
    
    [Benchmark]
    public void PartTwo()
    {
        Console.WriteLine($"EXTRA: {Day7.SolveExtra()}");
    }
}