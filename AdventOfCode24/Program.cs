using System.Diagnostics;
using AdventOfCode24.AdventDays;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace AdventOfCode24;

public class Program
{
    public static void Main(string[] args)
    {
        var loaded = DotNetEnv.Env.Load();
        BenchmarkRunner.Run<Solver>();
        PartOne();
        PartTwo();
    }

    private static void PartOne()
    {
        new Solver().PartOne();
    }
    
    private static void PartTwo()
    {
        new Solver().PartTwo();
    }
}