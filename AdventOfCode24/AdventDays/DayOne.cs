namespace AdventOfCode24.AdventDays;

public static class DayOne
{
    public static int Solve()
    {
        var inputLines = Core.ConvertFileToLines("DayOne");
        var firstList = GetFirstList(inputLines);
        var secondList = GetSecondList(inputLines);
        
        firstList.Sort();
        secondList.Sort();

        return firstList.Select((value, i) => Math.Abs(value - secondList[i])).Sum();
    }

    public static int SolveExtra()
    {
        var inputLines = Core.ConvertFileToLines("DayOne");
        var firstList = GetFirstList(inputLines);
        var secondList = GetSecondList(inputLines);
        
        return firstList.Select(number => secondList.Count(x => x == number) * number).Sum();
    }

    private static List<int> GetFirstList(List<string> inputLines)
    {
        return GetListAtPosition(inputLines, 0);
    }
    
    private static List<int> GetSecondList(List<string> inputLines)
    {
        return GetListAtPosition(inputLines, 1);
    }

    private static List<int> GetListAtPosition(List<string> inputLines, int position)
    {
        var lines = new List<int>();
        foreach (var line in inputLines)
        {
            lines.Add(int.Parse(line.Split("   ")[position]));
        }
        return lines;
    }
}