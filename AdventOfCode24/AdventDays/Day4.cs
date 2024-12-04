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
        private static readonly List<string> _directions = ["N", "NE", "E", "SE", "S", "SW", "W", "NW"];

        public static int Solve()
        {
            var data = Core.ConvertFileTo2dListChar("Day4");
            // save the start and endpoint in a dict
            var resultList = new List<SearchResult>();
            for (var y = 0; y < data.Count; y++)
            {
                for (var x = 0; x < data[y].Count; x++)
                {
                    var results = checkForStringInAllDirections("XMAS", new Point(x, y), data);
                    foreach (var result in results)
                    {
                        if(resultList.Any(x => x.endpoint.Equals(result.endpoint) && x.startpoint.Equals(result.startpoint)))
                        {
                            continue;
                        }

                        if (resultList.Any(x => x.endpoint.Equals(result.startpoint) && x.startpoint.Equals(result.endpoint)))
                        {
                            continue;
                        }
                        resultList.Add(result);
                    }
                }
            }

            return resultList.Count;
        }

        public static int SolveExtra()
        {
            return 0;
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

        private static List<SearchResult> checkForStringInAllDirections(string stringToSearch, Point currentPoint, List<List<char>> data)
        {
            var strLength = stringToSearch.Length - 1;
            var results = new List<SearchResult>();
            foreach (var direction in _directions)
            {
                var point = new Point(currentPoint.x, currentPoint.y);
                var foundStr = $"{data[point.y][point.x]}";
                for (var i = 0; i < strLength; i++)
                {
                    point = GetNextPointInDirection(direction, data, point);
                    if (!IsPointBetweenBoundaries(point.x, point.y, data)) break;
                    foundStr += data[point.y][point.x];
                }
                if (foundStr == stringToSearch || new string(foundStr.Reverse().ToArray()) == stringToSearch)
                {
                    results.Add(new SearchResult(point, currentPoint));
                }
            }
            return results;
        }

        private static Point GetNextPointInDirection(string direction, List<List<char>> data, Point point)
        {
            if (direction == "N") return new Point(point.x, point.y + 1);
            else if (direction == "NE") return new Point(point.x + 1, point.y + 1);
            else if (direction == "E") return new Point(point.x + 1, point.y);
            else if (direction == "SE") return new Point(point.x + 1, point.y - 1);
            else if (direction == "S") return new Point(point.x, point.y - 1);
            else if (direction == "SW") return new Point(point.x + 1 - 1, point.y - 1);
            else if (direction == "W") return new Point(point.x - 1, point.y);
            else if (direction == "NW") return new Point(point.x - 1, point.y + 1);
            return new Point(-1, -1);
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

        public override bool Equals(object? obj)
        {
            return obj is Point point &&
                   x == point.x &&
                   y == point.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }
    }

    public class SearchResult
    {
        public Point endpoint;
        public Point startpoint;


        public SearchResult(Point endpoint, Point startpoint)
        {
            this.endpoint = endpoint;
            this.startpoint = startpoint;
        }
    }
}
