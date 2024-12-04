using AdventOfCode24.AdventDays;

namespace AdventOfCode24;

class Program
{
    static void Main(string[] args)
    {
        var loaded = DotNetEnv.Env.Load();

        Console.WriteLine($"EASY: {Day4.Solve()}");
        Console.WriteLine($"EXTRA: {Day4.SolveExtra()}");
    }
}