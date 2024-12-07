using System.Diagnostics;
using AdventOfCode24.AdventDays;

namespace AdventOfCode24;

class Program
{
    static void Main(string[] args)
    {
        var loaded = DotNetEnv.Env.Load();
        
        var watch = Stopwatch.StartNew();
        Console.WriteLine($"EASY: {Day7.Solve()}");
        watch.Stop();
        Console.WriteLine($"Took {watch.Elapsed.TotalMilliseconds} milliseconds");
        var watch2 = Stopwatch.StartNew();
        Console.WriteLine($"EXTRA: {Day7.SolveExtra()}");
        watch2.Stop();
        Console.WriteLine($"Took {watch2.Elapsed.TotalMilliseconds} milliseconds");
    }
}