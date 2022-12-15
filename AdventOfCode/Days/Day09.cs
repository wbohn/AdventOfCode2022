using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day09 : AbstractBaseDay
    {
        private readonly List<(string Direction, int Amount)> _movements = new();
        public Day09()
        {
            foreach (var line in _lines)
            {
                var tokens = line.Split(" ");
                _movements.Add(new(tokens.First(), int.Parse(tokens.Last())));
            }
        }

        public override ValueTask<string> Solve_1()
        {
            (int X, int Y) headKnot = (0, 0);
            (int X, int Y) tailKnot = (0, 0);

            List<(int X, int Y)> tailPositions = new()
            {
                tailKnot
            };

            foreach (var (direction, amount) in _movements)
            {
                for (var i = 0; i < amount; i++)
                {
                    headKnot = MoveKnot(headKnot, direction);
                    if (!KnotsAreAdjacent(headKnot, tailKnot))
                    {
                        tailKnot = FollowKnot(headKnot, tailKnot);
                        tailPositions.Add(tailKnot);
                    }
                }
            }
            var uniquePositions = tailPositions.Distinct().Count();
            return ValueTask.FromResult(uniquePositions.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            List<(int X, int Y)> knots = new();
            for (var i = 0; i < 10; i++)
            {
                knots.Add((0, 0));
            }

            List<(int X, int Y)> tailPositions = new()
            {
                (0,0)
            };
            foreach (var (direction, amount) in _movements)
            {
                for (int i = 0; i < amount; i++)
                {
                    for (int j = 0; j < knots.Count; j++)
                    {
                        if (j == 0) // head knot
                        {
                            knots[j] = MoveKnot(knots[j], direction);
                        } else
                        {
                            if (!KnotsAreAdjacent(knots[j], knots[j - 1])) {
                                knots[j] = FollowKnot(knots[j - 1], knots[j]);
                            }
                        }

                        if (j == knots.Count - 1) // tail knot
                        {
                            tailPositions.Add(knots[j]);
                        }
                    }
                }
            }
            var uniquePositions = tailPositions.Distinct().Count();
            return ValueTask.FromResult(uniquePositions.ToString());
        }

        private (int X, int Y) FollowKnot((int X, int Y) headKnot, (int X, int Y) tailKnot)
        {
            // same column
            if (tailKnot.X == headKnot.X)
            {
                if (tailKnot.Y < headKnot.Y)
                {
                    tailKnot = MoveKnot(tailKnot, "U");
                }
                else
                {
                    tailKnot = MoveKnot(tailKnot, "D");
                }
                return tailKnot;
            }

            // same row
            if (tailKnot.Y == headKnot.Y)
            {
                if (tailKnot.X < headKnot.X)
                {
                    tailKnot = MoveKnot(tailKnot, "R");
                }
                else
                {
                    tailKnot = MoveKnot(tailKnot, "L");
                }
                return tailKnot;
            }

            if (tailKnot.X < headKnot.X)
            {
                tailKnot = MoveKnot(tailKnot, "R");
            }
            else
            {
                tailKnot = MoveKnot(tailKnot, "L");
            }

            if (tailKnot.Y < headKnot.Y)
            {
                tailKnot = MoveKnot(tailKnot, "U");
            }
            else
            {
                tailKnot = MoveKnot(tailKnot, "D");
            }

            return tailKnot;
        }

        private (int X, int Y) MoveKnot((int X, int Y) point, string direction)
        {
            switch (direction)
            {
                case "U":
                    {
                        return new(point.X, point.Y + 1);
                    }
                case "D":
                    {
                        return new(point.X, point.Y - 1);
                    }
                case "L":
                    {
                        return new(point.X - 1, point.Y);
                    }
                case "R":
                    {
                        return new(point.X + 1, point.Y);
                    }
                default:
                    throw new ArgumentException(direction);
            }
        }
        private bool KnotsAreAdjacent((int X, int Y) headPosition, (int X, int Y) tailPosition)
        {
            if (headPosition.X == tailPosition.X && headPosition.Y == tailPosition.Y)
            {
                return true; // H covers T
            }

            var xDistance = Math.Abs(headPosition.X - tailPosition.X);
            var yDistance = Math.Abs(headPosition.Y - tailPosition.Y);

            if (xDistance == 0) // same column
            {
                return yDistance <= 1;
            }

            if (yDistance == 0) // same row
            {
                return xDistance <= 1;
            }

            return xDistance + yDistance <= 2;
        }
    }
}
