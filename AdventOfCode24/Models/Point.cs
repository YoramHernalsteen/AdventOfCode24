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
        return IsPointBetweenBoundaries(point.x, point.y, data);
    }
    
    public static bool IsPointBetweenBoundaries(int x, int y, List<List<char>> data)
    {
        var yBound = data.Count;
        if (x < 0 || y < 0)
        {
            return false;
        }
        if (yBound <= y)
        {
            return false;
        }
        var xBound = data[y].Count;

        if (xBound <= x)
        {
            return false;
        }
        return true;
    }
    
    public static bool IsPointBetweenBoundaries(int x, int y, char[,] data)
    {
        var yBound = data.GetLength(0);
        if (x < 0 || y < 0)
        {
            return false;
        }
        if (yBound <= y)
        {
            return false;
        }
        var xBound = data.GetLength(1);

        if (xBound <= x)
        {
            return false;
        }
        return true;
    }
}