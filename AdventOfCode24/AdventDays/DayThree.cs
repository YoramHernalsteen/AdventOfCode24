using System.Text.RegularExpressions;

namespace AdventOfCode24.AdventDays;

public static class DayThree
{
    public static int Solve(bool checkForEnableInstructions = false)
    {
        const string multiplicationRegex = @"mul\((\d+),(\d+)\)";
        const string enableInstructionRegex = @"do\(\)|don't\(\)";
        var text = Core.ConvertFileToText("DayThree");
        var sum = 0;
        
        var instructions = Regex.Matches(text, enableInstructionRegex);
        var firstDisableInstruction = instructions.OrderBy(x => x.Index).First(x => x.Value == "don't()");
        
        foreach (Match match in Regex.Matches(text, multiplicationRegex))
        {
            if (!checkForEnableInstructions)
            {
                sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                continue;
            }
                
            if (match.Index < firstDisableInstruction.Index)
            {
                sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                continue;
            }
                
            var lastDisableInstruction = instructions
                                         .Where(x => x.Value == "don't()" && x.Index < match.Index)
                                         .OrderByDescending(x => x.Index)
                                         .FirstOrDefault()?.Index ?? int.MinValue;
            
            var lastEnableInstruction = instructions
                                        .Where(x => x.Value == "do()" && x.Index < match.Index)
                                        .OrderByDescending(x => x.Index)
                                        .FirstOrDefault()?.Index ?? int.MinValue ;
            
            if (lastEnableInstruction > lastDisableInstruction)
            {
                sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                continue;
            }
                
        }

        return sum;
    }

    public static int SolveExtra()
    {
        return Solve(checkForEnableInstructions: true);
    }
}