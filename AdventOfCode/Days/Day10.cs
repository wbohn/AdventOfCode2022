using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day10 : AbstractBaseDay
    {
        private readonly List<(int Cycle, int RegisterValue)> _cycleValues = new();

        public Day10()
        {
            int xRegister = 1;
            int cycleCount = 0;

            foreach (var instruction in _lines)
            {
                var instructionType = instruction[..4];
                if (instructionType == "noop")
                {
                    cycleCount++;
                    _cycleValues.Add(new(cycleCount, xRegister));
                }
                else
                {
                    var valueToAdd = int.Parse(instruction.Split(" ").Last());

                    for (int i = 0; i < 2; i++)
                    {
                        cycleCount++;
                        _cycleValues.Add(new(cycleCount, xRegister));
                    }
                    xRegister += valueToAdd;
                }
            }
        }

        public override ValueTask<string> Solve_1()
        {
            List<int> cyclesToCheck = new()
            {
                20, 60, 100, 140, 180, 220
            };

            var totalSignalStrength = _cycleValues
                .Where(c => cyclesToCheck.Contains(c.Cycle))
                .Select(c => c.Cycle * c.RegisterValue)
                .Sum();

            return ValueTask.FromResult(totalSignalStrength.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int currentPixel = 0;
            foreach (var (Cycle, RegisterValue) in _cycleValues)
            {
                List<int> spritePixels = new()
                {
                    RegisterValue - 1,
                    RegisterValue,
                    RegisterValue + 1
                };

                if (spritePixels.Contains(currentPixel))
                {
                    Console.Write("#");
                } else
                {
                    Console.Write(".");
                }
                currentPixel++;

                if (Cycle % 40 == 0)
                {
                    Console.Write("\r\n");
                    currentPixel = 0;
                }
            }

            /*
                ####..##...##....##.####...##.####.#....
                ...#.#..#.#..#....#....#....#.#....#....
                ..#..#....#.......#...#.....#.###..#....
                .#...#.##.#.......#..#......#.#....#....
                #....#..#.#..#.#..#.#....#..#.#....#....
                ####..###..##...##..####..##..#....####.
             */
            return ValueTask.FromResult("ZGCJZJFL".ToString());
        }
    }
}
