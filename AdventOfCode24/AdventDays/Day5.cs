namespace AdventOfCode24.AdventDays;

public static class Day5
{
    public static int Solve(bool part2 = false)
    {
        var lines = Core.ConvertFileToLines();
        var rules = GenerateRules(lines);
        var manuals = GenerateManuals(lines);
        var result = 0;
        
        foreach (var manual in manuals)
        {
            if (IsValidManual(manual, rules) && !part2)
            {
                result += manual[(manual.Count / 2)];
            } else if (!IsValidManual(manual, rules) && part2)
            {
                var validatedManual = ValidateManual(manual, rules);
                result += validatedManual[(validatedManual.Count / 2)];
            }
        }
        return result;
    }

    public static int SolveExtra()
    {
        return Solve(true);
    }

    private static bool IsValidManual(List<int> manual, List<ManualRule> rules)
    {
        foreach (var rule in rules)
        {
            var numberOneIndex = manual.IndexOf(rule.First);
            var numberTwoIndex = manual.IndexOf(rule.Second);

            if (numberOneIndex > numberTwoIndex && numberTwoIndex != -1)
            {
                return false;
            }
        }
        return true;
    }

    private static List<int> ValidateManual(List<int> manual, List<ManualRule> rules)
    {
        while (!IsValidManual(manual, rules))
        {
            foreach (var rule in rules)
            {
                var numberOneIndex = manual.IndexOf(rule.First);
                var numberTwoIndex = manual.IndexOf(rule.Second);

                if (numberOneIndex > numberTwoIndex && numberTwoIndex != -1)
                {
                    manual.RemoveAt(numberOneIndex);
                    manual.Insert(numberTwoIndex, rule.First);
                }
            }
        }
        return manual;
    }

    public static List<List<int>> GenerateManuals(List<string> lines)
    {
        var manuals = new List<List<int>>();
        foreach (var line in lines)
        {
            if(!line.Contains(",")) continue;
            var manual = line.Split(",").Select(int.Parse).ToList();
            manuals.Add(manual);
        }
        
        return manuals;
    }

    public static List<ManualRule> GenerateRules(List<string> lines)
    {
        var rules = new List<ManualRule>();
        foreach (var line in lines)
        {
            if (!line.Contains("|")) continue;
            var numbers = line.Split("|").Select(int.Parse).ToList();
            rules.Add(new ManualRule(numbers[0], numbers[1]));
        }
        return rules;
    }
}

public class ManualRule
{
    public int First;
    public int Second;

    public ManualRule(int first, int second)
    {
        First = first;
        Second = second;
    }
}