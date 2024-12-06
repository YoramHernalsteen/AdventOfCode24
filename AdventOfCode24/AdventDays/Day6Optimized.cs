using AdventOfCode24.Models;

namespace AdventOfCode24.AdventDays;

public static class Day6Optimized
{
    private static readonly int Up = 0;
    private static readonly int Right = 1;
    private static readonly int Down = 2;
    private static readonly int Left = 3;
    
    
    public static int Solve(bool part2 = false)
    {
        var grid = Core.ConvertFileTo2dArrayChar();
        var (startPointX, startPointY) = Find(grid, '^');

        var route = GetRoute(startPointX, startPointY, grid);
        if (!part2) return route.Count;
        
        // for part 2, check for each point in the route if it leads to an infinite loop
        // meaning no out of bounds and visit the same cell twice from the same direction
        var infiniteLoops = 0;
        foreach (var (pointX, pointY) in route)
        {
            if (grid[pointY, pointX] != '.')
            {
                continue;
            }
            grid[pointY, pointX] = '#';
            // do calculation
            if(IsInfiniteLoop(startPointX, startPointY, grid)) infiniteLoops++;
            // return to grid as it was
            grid[pointY, pointX] = '.';
        }
        return infiniteLoops;
    }

    public static int SolveExtra()
    {
        return Solve(true);
    }

    private static (int, int) Move(int x, int y, int direction)
    {
        if (direction == Up) y--;
        else if (direction == Down) y++;
        else if (direction == Left) x--;
        else if (direction == Right) x++;
        
        return (x, y);
    }

    private static int ChangeDirection(int direction)
    {
        return (direction + 1) % 4;
    }
    
    private static bool IsInfiniteLoop(int pointX, int pointY, char [,] grid)
    {
        var direction = Up;
        var startPointX = pointX;
        var startPointY = pointY;
        var (nextPointX, nextPointY) = Move(pointX, pointY, direction);
        var visited = new HashSet<(int, int, int)>();
        while (Point.IsPointBetweenBoundaries(nextPointX, nextPointY, grid))
        {
            var nextPointAndDirection = (nextPointX, nextPointY, direction);
            var isStartPoint = (nextPointX == startPointX && nextPointY == startPointY);
            if (!isStartPoint && visited.Contains(nextPointAndDirection))
            {
                return true;
            }
            
            if (grid[nextPointY, nextPointX] == '#')
            {
                direction = ChangeDirection(direction);
                (nextPointX, nextPointY) = Move(pointX, pointY, direction);
                continue;
            }
            pointX = nextPointX;
            pointY = nextPointY;
            visited.Add(nextPointAndDirection);
            (nextPointX, nextPointY) = Move(pointX, pointY, direction);
        }
        return false;
    }

    private static HashSet<(int, int)> GetRoute(int x, int y, char [,] grid)
    {
        var route = new HashSet<(int, int)>{(x, y) };
        var direction = Up;
        
        var (nextX, nextY) = Move(x, y, direction);
        while (Point.IsPointBetweenBoundaries(nextX, nextY, grid))
        {
            if (grid[nextY, nextX] == '#')
            {
                direction = ChangeDirection(direction);
                (nextX, nextY) = Move(x, y, direction);
                continue;
            }
            x = nextX;
            y = nextY;
            route.Add((nextX, nextY));
            (nextX, nextY) = Move(x, y, direction);
        }
        return route;
    }

    private static (int, int) Find(char[,] grid, char character)
    {
        for (var y = 0; y < grid.GetLength(0); y++)
        {
            for (var x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[y, x] == character) return (x, y);
            }
        }

        return (-1, -1);
    }
}