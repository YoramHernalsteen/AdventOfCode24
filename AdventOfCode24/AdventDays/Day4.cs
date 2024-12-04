using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode24.AdventDays
{
    public static class Day4
    {
        private static readonly List<int> _directions = [-1, 0, 1];

        public static int Solve(bool isPartTwo = false)
        {
            var data = Core.ConvertFileTo2dListChar();
            var found = 0;
            for (var y = 0; y < data.Count; y++)
            {
                for (var x = 0; x < data[y].Count; x++)
                {
                    if (!isPartTwo)
                    {
                        if (data[y][x] != 'X') continue;
                        found += CheckForStringInAllDirections("XMAS", new Point(x, y), data);
                    }
                    else
                    {
                        if (data[y][x] != 'A') continue;
                        found += CheckForCross(new Point(x, y), data);
                    }
                }
            }
            return found;
        }

        public static int SolveExtra()
        {
            return Solve(true);
        }

        private static bool IsPointBetweenBoundaries(int x, int y, List<List<char>> data)
        {
            var ybound = data.Count;
            if (x < 0 || y < 0)
            {
                return false;
            }
            if (ybound <= y)
            {
                return false;
            }
            var xbound = data[y].Count;

            if (xbound <= x)
            {
                return false;
            }
            return true;
        }

        private static int CheckForStringInAllDirections(string stringToSearch, Point currentPoint, List<List<char>> data)
        {
            var found = 0;
            foreach (var xDirection in _directions)
            {
                foreach (var yDirection in _directions)
                {
                    if(xDirection == 0 && yDirection == 0) continue;
                    var point = new Point(currentPoint.x, currentPoint.y);
                    var foundStr = $"{data[point.y][point.x]}";
                    
                    for (var i = 0; i < stringToSearch.Length - 1; i++)
                    {
                        point.x += xDirection;
                        point.y += yDirection;
                        if (!IsPointBetweenBoundaries(point.x, point.y, data)) break;
                        foundStr += data[point.y][point.x];
                    }
                    
                    if (foundStr == stringToSearch || new string(foundStr.Reverse().ToArray()) == stringToSearch)
                    {
                        found++;
                    }
                }
            }
            
            return found;
        }
        
        private static int CheckForCross(Point currentPoint, List<List<char>> data)
        {
            var topLeft = new Point(currentPoint.x -1, currentPoint.y -1);
            var topRight = new Point(currentPoint.x + 1, currentPoint.y -1);
            var bottomLeft = new Point(currentPoint.x - 1, currentPoint.y + 1);
            var bottomRight = new Point(currentPoint.x + 1, currentPoint.y + 1);
            
            foreach (var point in new List<Point>(){topLeft, topRight, bottomLeft, bottomRight})
            {
                if (!IsPointBetweenBoundaries(point.x, point.y, data))
                {
                    return 0;
                }
            }

            var diagonal1 = $"{data[topLeft.y][topLeft.x]}{data[bottomRight.y][bottomRight.x]}";
            var diagonal2 = $"{data[topRight.y][topRight.x]}{data[bottomLeft.y][bottomLeft.x]}";
            foreach (var str in new List<string>() { diagonal1, diagonal2 })
            {
                if (str != "SM" && str != "MS")
                {
                    return 0;
                }
            }
            
            return 1;
        }
        
    }

    public class Point
    {
        public int x;
        public int y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
