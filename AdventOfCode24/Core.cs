namespace AdventOfCode24;

public static class Core
{
    private static readonly string _directory = "/Users/yoramhernalsteen/RiderProjects/AdventOfCode24/AdventOfCode24/Input";
    
    public static List<string> ConvertFileToLines(string filename)
    {
        var file = Path.Combine(_directory, filename);
        file += ".txt";
        var lines = File.ReadAllLines(file);
        return lines.ToList();
    }
}