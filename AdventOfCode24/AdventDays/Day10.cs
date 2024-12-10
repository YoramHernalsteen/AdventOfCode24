using AdventOfCode24.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode24.AdventDays
{
    public static class Day10
    {
        public static int Solve(bool part2 = false)
        {
            var grid = Core.ConvertFileTo2dListInt();
            var totalPaths = 0;
            for (int y = 0; y < grid.Count; y++)
            {
                for (int x = 0; x < grid[y].Count; x++)
                {
                    if (grid[y][x] == 0)
                    {
                        if (part2) totalPaths += PossiblePaths(x, y, grid, new List<(int, int)>());
                        else totalPaths += PossiblePaths(x, y, grid, new HashSet<(int, int)>());
                    }
                }
            }
            // get al indexes for 0
            // for all of these indexes check the path using a stack (get neighbour as long as it is value + 1)
            // if we get at 9, we finish and count ++
            return totalPaths;
        }

        public static int SolveExtra()
        {
            return Solve(part2:true);
        }

        private static int PossiblePaths<T>(int x, int y, List<List<int>> grid, T paths) where T : ICollection<(int, int)>
        {
            var directions = new List<(int, int)> {(0, 1), (1, 0), (0, -1), (-1, 0)};
            //previousValue, x, y
            var pathsToCheck = new Stack<(int, int, int)>();
            pathsToCheck.Push((-1, x, y));
            while (pathsToCheck.Count > 0)
            {
                var (previousValue, currentX, currentY) = pathsToCheck.Pop();
                var nextValue = grid[currentY][currentX];

                if (nextValue == previousValue + 1)
                {
                    if (nextValue == 9)
                    {
                        paths.Add((currentX, currentY));
                    }

                    foreach (var(xDirection, yDirection) in directions)
                    {

                        if(Point.IsPointBetweenBoundaries( currentX + xDirection, currentY + yDirection, grid))
                        {
                            pathsToCheck.Push((nextValue, currentX + xDirection, currentY + yDirection));
                        }
                    }
                }
            }

            return paths.Count;
        }
    }
}
