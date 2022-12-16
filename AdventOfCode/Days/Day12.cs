using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    /* Modified code from
     *  https://dotnetcoretutorials.com/2020/07/25/a-search-pathfinding-algorithm-in-c
     */
    public class Day12 : AbstractBaseDay
    {
        private readonly List<string> _map;

        public Day12()
        {
            _map = _lines.ToList();
        }

        public override ValueTask<string> Solve_1()
        {
            Tile start = new()
            {
                Y = _map.FindIndex(x => x.Contains('S'))
            };
            start.X = _map[start.Y].IndexOf("S");

            List<Tile> shortestPath = WalkMap(start);

            return ValueTask.FromResult(shortestPath.Count.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            // brutally find all possible starting positions
            List<Tile> startTiles = new();
            for (int r = 0; r < _map.Count; r++)
            {
                for (int c = 0; c < _map.First().Length; c++)
                {
                    if (_map[r][c] == 'a' || _map[r][c] == 'S')
                    {
                        startTiles.Add(new Tile()
                        {
                            X = c,
                            Y = r
                        });
                    }
                }
            }

            List<List<Tile>> walkedPaths = new();
            foreach (var tile in startTiles)
            {
                var path = WalkMap(tile);
                if (path != null)
                {
                    walkedPaths.Add(path);
                }
            }

            var shortestPath = walkedPaths
                .OrderBy(p => p.Count)
                .Take(1)
                .First();

            return ValueTask.FromResult(shortestPath.Count.ToString());
        }

        private List<Tile>? WalkMap(Tile start)
        {
            Tile finish = new()
            {
                Y = _map.FindIndex(x => x.Contains('E'))
            };
            finish.X = _map[finish.Y].IndexOf("E");

            start.SetDistance(finish.X, finish.Y);

            List<Tile> activeTiles = new()
            {
                start
            };
            List<Tile> visitedTiles = new();

            while (activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(x => x.CostDistance).First(); // OrderByDesc(x => x.CostDistance).Last()

                if (checkTile.X == finish.X && checkTile.Y == finish.Y)
                {
                    //We found the destination and we can be sure (Because the the OrderBy above)
                    //That it's the most low cost option.
                    List<Tile> shortestPath = new();

                    var tile = checkTile;
                    Console.WriteLine("Retracing steps backwards...");
                    while (true)
                    {
                        tile = tile.Parent;
                        if (tile == null)
                        {
                            return shortestPath;

                        }
                        shortestPath.Add(tile);
                    }
                }

                visitedTiles.Add(checkTile);
                activeTiles.Remove(checkTile);

                var walkableTiles = GetWalkableTiles(_map, checkTile, finish);

                foreach (var walkableTile in walkableTiles)
                {
                    //We have already visited this tile so we don't need to do so again!
                    if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                        continue;

                    //It's already in the active list, but that's OK, maybe this new tile has a better value (e.g. We might zigzag earlier but this is now straighter). 
                    if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                    {
                        var existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                        if (existingTile.CostDistance > checkTile.CostDistance)
                        {
                            activeTiles.Remove(existingTile);
                            activeTiles.Add(walkableTile);
                        }
                    }
                    else
                    {
                        //We've never seen this tile before so add it to the list. 
                        activeTiles.Add(walkableTile);
                    }
                }
            }

            Console.WriteLine("No Path Found!");
            return null;
        }
        private static List<Tile> GetWalkableTiles(List<string> map, Tile currentTile, Tile targetTile)
        {
            var possibleTiles = new List<Tile>()
            {
                new Tile { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
                new Tile { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
            };

            possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

            var maxX = map.First().Length - 1;
            var maxY = map.Count - 1;

            var currentHeight = GetHeight(map, currentTile);

            var tilesInRange = possibleTiles
                    .Where(tile => tile.X >= 0 && tile.X <= maxX)
                    .Where(tile => tile.Y >= 0 && tile.Y <= maxY);

            var walkableTiles = tilesInRange
                    .Where(tile => GetHeight(map, tile) <= currentHeight + 1)
                    .ToList();
            return walkableTiles;
        }

        private static int GetHeight(List<string> map, Tile tile)
        {
            var c = map[tile.Y][tile.X];
            if (c == 'S')
            {
                return 0; // S = a
            }

            if (c == 'E')
            {
                return 25; // E = z 
            }
            return c - 97; // a = 0
        }
    }
}

class Tile
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Cost { get; set; }
    public int Distance { get; set; }
    public int CostDistance => Cost + Distance;
    public Tile? Parent { get; set; }

    //The distance is essentially the estimated distance, ignoring walls to our target. 
    //So how many tiles left and right, up and down, ignoring walls, to get there. 
    public void SetDistance(int targetX, int targetY)
    {
        this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
    }
}

