using AdventOfCode24.AdventDays;

namespace AdventOfCode24;

class Program
{
    static void Main(string[] args)
    {
        var loaded = DotNetEnv.Env.Load();

        Console.WriteLine($"EASY: {DayTwo.Solve()}");
        Console.WriteLine($"EXTRA: {DayTwo.SolveExtra()}");
    }
}