namespace AdventOfCode24;

public static class Core
{
    
    public static List<string> ConvertFileToLines()
    {
        var file = DotNetEnv.Env.GetString("INPUT_FILE");
        var lines = File.ReadAllLines(file);
        return lines.ToList();
    }

    public static string ConvertFileToText()
    {
        var lines = ConvertFileToLines();
        return string.Join("", lines);
    }

    public static List<List<int>> ConvertFileTo2dListInt(string separator = " ")
    {
        var lines = ConvertFileToLines();
        var list = new List<List<int>>();
        foreach (var line in lines)
        {
            var innerList = line.Split(separator).Select(x => int.Parse(x)).ToList();
            list.Add(innerList);
        }

        return list;    
    }

    public static List<List<char>> ConvertFileTo2dListChar()
    {
        var lines = ConvertFileToLines();
        var list = new List<List<char>>();
        foreach (var line in lines)
        {
            var innerList = line.ToCharArray().ToList();
            list.Add(innerList);
        }

        return list;
    }
}