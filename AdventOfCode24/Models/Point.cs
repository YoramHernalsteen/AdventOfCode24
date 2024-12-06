namespace AdventOfCode24.Models;

public class Point
{
    public int x;
    public int y;
    
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"[{x},{y}]";
    }

    public override bool Equals(object? obj)
    {
        return obj is Point p && this.x == p.x && this.y == p.y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }

    public static bool IsPointBetweenBoundaries(Point point, List<List<char>> data)
    {
        var ybound = data.Count;
        if (point.x < 0 || point.y < 0)
        {
            return false;
        }
        if (ybound <= point.y)
        {
            return false;
        }
        var xbound = data[point.y].Count;

        if (xbound <= point.x)
        {
            return false;
        }
        return true;
    }
}