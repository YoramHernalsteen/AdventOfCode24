using AdventOfCode24.Models;

namespace AdventOfCode24.AdventDays;

public static class Day6
{
    public static int Solve(bool part2 = false)
    {
        var grid = Core.ConvertFileTo2dListChar();
        
        var startPoint = new Point(0, 0);
        startPoint.y = grid.FindIndex(0, x => x.Contains('^'));
        startPoint.x = grid[startPoint.y].FindIndex(0, x => x.Equals('^'));

        var route = GetRoute(new Point(startPoint.x, startPoint.y), grid);
        if (!part2)return route.Count;
        
        // for part 2, check for each point in the route if it leads to an infinite loop
        // meaning no out of bounds and visit the same cell twice from the same direction
        var infiniteLoops = 0;
        foreach (var point in route)
        {
            if (grid[point.y][point.x] != '.')
            {
                continue;
            }
            grid[point.y][point.x] = '#';
            // do calculation
            if(IsInfiniteLoop(new Point(startPoint.x, startPoint.y), grid)) infiniteLoops++;
            // return to grid as it was
            grid[point.y][point.x] = '.';
        }
        return infiniteLoops;
    }

    public static int SolveExtra()
    {
        return Solve(true);
    }
    
    private enum Direction
    {
        Up, Left, Right, Down
    }

    private static Point Move(Point point, Direction direction)
    {
        var  x = point.x;
        var y = point.y;

        if (direction == Direction.Up) y--;
        else if (direction == Direction.Down) y++;
        else if (direction == Direction.Left) x--;
        else if (direction == Direction.Right) x++;
        
        return new Point(x, y);
    }

    private static Direction ChangeDirection(Direction direction)
    {
        if (direction == Direction.Up) return Direction.Right;
        if (direction == Direction.Right) return Direction.Down;
        if (direction == Direction.Down) return Direction.Left;
        return Direction.Up;
    }
    
    private static bool IsInfiniteLoop(Point startPoint, List<List<char>> grid)
    {
        var point = new Point(startPoint.x, startPoint.y);
        var direction = Direction.Up;
        var nextPoint = Move(point, direction);
        var visited = new HashSet<Tuple<Point, Direction>>();
        while (Point.IsPointBetweenBoundaries(nextPoint, grid))
        {
            var nextPointAndDirection = Tuple.Create(nextPoint, direction);
            if (!nextPoint.Equals(startPoint) && visited.Contains(nextPointAndDirection))
            {
                return true;
            }
            
            if (grid[nextPoint.y][nextPoint.x] == '#')
            {
                direction = ChangeDirection(direction);
                nextPoint = Move(point, direction);
                continue;
            }
            point.x = nextPoint.x;
            point.y = nextPoint.y;
            visited.Add(nextPointAndDirection);
            nextPoint = Move(point, direction);
        }
        return false;
    }

    private static HashSet<Point> GetRoute(Point point, List<List<char>> grid)
    {
        var route = new HashSet<Point>{point};
        var direction = Direction.Up;
        
        var nextPoint = Move(point, direction);
        while (Point.IsPointBetweenBoundaries(nextPoint, grid))
        {
            if (grid[nextPoint.y][nextPoint.x] == '#')
            {
                direction = ChangeDirection(direction);
                nextPoint = Move(point, direction);
                continue;
            }
            point.x = nextPoint.x;
            point.y = nextPoint.y;
            route.Add(nextPoint);
            nextPoint = Move(point, direction);
        }
        return route;
    }
    
}