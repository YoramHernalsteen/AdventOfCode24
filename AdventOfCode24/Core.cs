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
}