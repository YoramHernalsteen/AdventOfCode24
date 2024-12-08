using System.Reflection;
using AdventOfCode24.Models;

namespace AdventOfCode24.AdventDays;

public static class Day8
{
    public static int Solve(bool part2 = false)
    {
        var grid = Core.ConvertFileTo2dListChar();
        var antennaDict = new Dictionary<char, List<(int, int)>>();
        var antennas = new List<(int, int)>();
        for (var y = 0; y < grid.Count; y++)
        {
            for (var x = 0; x < grid[y].Count; x++)
            {
                if(grid[y][x] == '.')continue;
                antennas.Add((x, y));
                if (!antennaDict.ContainsKey(grid[y][x]))
                {
                    antennaDict.Add(grid[y][x], new List<(int, int)>(){(x, y)});
                }
                else
                {
                    antennaDict[grid[y][x]].Add((x, y));
                }
                
            }
        }

        var antiNodes = new HashSet<(int, int)>();
        foreach (var (xPoint, yPoint) in antennas)
        {
            var otherPoints = antennaDict[grid[yPoint][xPoint]];
            foreach (var (xPointOther, yPointOther) in otherPoints)
            {
                if(xPoint == xPointOther && yPoint == yPointOther) continue;
                
                var newPoint1X = -1;
                var newPoint1Y = -1;
                var newPoint2X = -1;
                var newPoint2Y = -1;

                if (!part2)
                {
                    (newPoint1X, newPoint1Y, _, _) = NextPointInLine(xPoint, yPoint, xPointOther, yPointOther);
                    (newPoint2X, newPoint2Y, _, _) = NextPointInLine(xPointOther, yPointOther, xPoint, yPoint);
                
                    if(Point.IsPointBetweenBoundaries(newPoint1X, newPoint1Y, grid)) antiNodes.Add((newPoint1X, newPoint1Y));
                    if(Point.IsPointBetweenBoundaries(newPoint2X, newPoint2Y, grid)) antiNodes.Add((newPoint2X, newPoint2Y));   
                }
                else
                {
                    (newPoint1X, newPoint1Y, var xDiff, var yDiff) = NextPointInLine(xPoint, yPoint, xPointOther, yPointOther, invert:true);
                    while (Point.IsPointBetweenBoundaries(newPoint1X, newPoint1Y, grid))
                    {
                        antiNodes.Add((newPoint1X, newPoint1Y));
                        newPoint1X += xDiff;
                        newPoint1Y += yDiff;
                        
                    }
                        
                    (newPoint2X, newPoint2Y, xDiff, yDiff) = NextPointInLine(xPointOther, yPointOther, xPoint, yPoint, invert:true);
                    while (Point.IsPointBetweenBoundaries(newPoint2X, newPoint2Y, grid))
                    {
                        antiNodes.Add((newPoint2X, newPoint2Y));
                        newPoint2X += xDiff;
                        newPoint2Y += yDiff;
                    }
                }
            }
        }

        return antiNodes.Count;
    }

    private static (int, int, int, int) NextPointInLine(int x, int y, int x1, int y1, bool invert = false)
    {
        var pointIsUp = y1 > y;
        var pointIsRight = x1 < x;
                
        var xDiff = Math.Abs(x - x1);
        var yDiff = Math.Abs(y - y1);
        
        if (pointIsUp && pointIsRight)
        {
            if (!invert)
            {
                return (x + xDiff, y - yDiff, xDiff, -yDiff);
            }
            return (x - xDiff, y + yDiff, -xDiff, yDiff);
        } else if (pointIsUp && !pointIsRight)
        {
            if (!invert)
            {
                return (x - xDiff, y - yDiff, -xDiff, -yDiff );
            }
            return (x + xDiff, y + yDiff, xDiff, yDiff);
        }
        else if (!pointIsUp && pointIsRight)
        {
            if (!invert)
            {
                return (x + xDiff, y + yDiff, xDiff, yDiff);
            }
            return (x - xDiff, y - yDiff, -xDiff, -yDiff);
        } else //(!pointIsUp && !pointIsRight)
        {
            if (!invert)
            {
                return (x - xDiff, y + yDiff, -xDiff, yDiff);
            }
            return (x + xDiff, y - yDiff, xDiff, -yDiff);
        }
        
    }
    public static int SolveExtra()
    {
        return Solve(part2:true);
    }
}