namespace AdventOfCode24;

public static class Core
{
    
    public static List<string> ConvertFileToLines(string filename)
    {
        var directory = DotNetEnv.Env.GetString("INPUT_DIR");
        var file = Path.Combine(directory, filename);
        file += ".txt";
        var lines = File.ReadAllLines(file);
        return lines.ToList();
    }

    public static string ConvertFileToText(string filename)
    {
        var lines = ConvertFileToLines(filename);
        return string.Join("", lines);
    }

    public static List<List<int>> ConvertFileTo2dListInt(string filename, string separator = " ")
    {
        var lines = ConvertFileToLines(filename);
        var list = new List<List<int>>();
        foreach (var line in lines)
        {
            var innerList = line.Split(separator).Select(x => int.Parse(x)).ToList();
            list.Add(innerList);
        }

        return list;    
    }

    public static List<List<char>> ConvertFileTo2dListChar(string filename)
    {
        var lines = ConvertFileToLines(filename);
        var list = new List<List<char>>();
        foreach (var line in lines)
        {
            var innerList = line.ToCharArray().ToList();
            list.Add(innerList);
        }

        return list;
    }
}