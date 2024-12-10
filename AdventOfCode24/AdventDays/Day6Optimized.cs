using AdventOfCode24.Models;

namespace AdventOfCode24.AdventDays;

public static class Day6Optimized
{
    private static readonly int Up = 0;
    private static readonly int Right = 1;
    private static readonly int Down = 2;
    private static readonly int Left = 3;
    
    
    public static int Solve()
    {
        var grid = Core.ConvertFileTo2dArrayChar();
        var (startPointX, startPointY) = Find(grid, '^');

        var route = GetRoute(startPointX, startPointY, grid);
        return route.Count;
    }

    
    // we can make it faster by starting the route just before the block you place
    public static int SolveExtra()
    {
        var grid = Core.ConvertFileTo2dArrayChar();
        var (startPointX, startPointY) = Find(grid, '^');
        var route = GetRouteWithDirection(startPointX, startPointY, grid);
        
        var visited = new HashSet<(int, int)>();
        
        var infiniteLoops = 0;
        foreach (var (pointX, pointY, direction) in route)
        {
            if (visited.Contains((pointX, pointY))) continue;
            
            visited.Add((pointX, pointY));
            
            if (grid[pointY, pointX] != '.')
            {
                continue;
            }
            grid[pointY, pointX] = '#';
            // do calculation
            var (previousX, previousY) = Move(pointX, pointY, direction, moveBack:true);
            if(IsInfiniteLoop(previousX, previousY, grid, direction)) infiniteLoops++;
            // return to grid as it was
            grid[pointY, pointX] = '.';
        }
        return infiniteLoops;
    }

    private static (int, int) Move(int x, int y, int direction, bool moveBack = false)
    {
        if (direction == Up && !moveBack) return(x, --y);
        else if (direction == Down && !moveBack) return (x, ++y);
        else if (direction == Left && !moveBack) return (--x, y);
        else if (direction == Right && !moveBack)return (++x, y);
        else if (direction == Up && moveBack) return(x, ++y);
        else if (direction == Down && moveBack) return (x, --y);
        else if (direction == Left && moveBack) return (++x, y);
        else if (direction == Right && moveBack)return (--x, y);
        
        return (x, y);
    }

    private static int ChangeDirection(int direction)
    {
        return (direction + 1) % 4;
    }
    
    private static bool IsInfiniteLoop(int pointX, int pointY, char [,] grid, int direction)
    {
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

    private static List<(int, int, int)> GetRouteWithDirection(int x, int y, char [,] grid)
    {
        var route = new List<(int, int, int)>{(x, y, Up) };
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
            route.Add((nextX, nextY, direction));
            x = nextX;
            y = nextY;
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