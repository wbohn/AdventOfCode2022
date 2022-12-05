using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day05 : AbstractBaseDay
    {
        private List<List<string>> GetStacks()
        {
            List<List<string>> stacks = new();

            List<string> rows = _lines.Take(8).ToList();

            for (int c = 1; c < 35 - 1; c += 4)
            {
                List<string> stack = new();
                for (int r = 0; r < rows.Count; r++)
                {
                    var crate = rows[r][c].ToString();
                    if (crate != " ")
                    {
                        stack.Add(crate);
                    }
                }
                stacks.Add(stack);
            }
            return stacks;
        }

        private static string GetTopCrates(List<List<string>> stacks)
        {
            var topCrates = "";

            foreach (var stack in stacks)
            {
                topCrates += stack[0];
            }
            return topCrates;
        }
        public override ValueTask<string> Solve_1()
        {
            var stacks = GetStacks();

            var topStacks = GetTopCrates(MoveStacks(stacks));
            return ValueTask.FromResult(topStacks);
        }

        private List<List<string>> MoveStacks(List<List<string>> stacks)
        {
            for (int i = 10; i < _lines.Length; i++)
            {
                var tokens = _lines[i].Split(" ");

                var amount = int.Parse(tokens[1]);
                var from = int.Parse(tokens[3]);
                var to = int.Parse(tokens[5]);

                var fromStack = stacks[from - 1];
                var toStack = stacks[to - 1];

                for (int j = 0; j < amount; j++)
                {
                    var crateToMove = fromStack[0];
                    fromStack.RemoveAt(0);
                    toStack.Insert(0, crateToMove);
                }
            }
            return stacks;
        }

        public override ValueTask<string> Solve_2()
        {
            var stacks = GetStacks();
            return ValueTask.FromResult(GetTopCrates(MoveStacks2(stacks)));
        }

        private List<List<string>> MoveStacks2(List<List<string>> stacks)
        {
            for (int i = 10; i < _lines.Length; i++)
            {
                var tokens = _lines[i].Split(" ");

                var amount = int.Parse(tokens[1]);
                var from = int.Parse(tokens[3]);
                var to = int.Parse(tokens[5]);

                var fromStack = stacks[from - 1];
                var toStack = stacks[to - 1];

                List<string> cratesToMove = new();

                for (int j = 0; j < amount; j++)
                {
                    cratesToMove.Add(fromStack[0]);
                    fromStack.RemoveAt(0);
                }

                for (int k = cratesToMove.Count - 1; k >= 0; k--)
                {
                    toStack.Insert(0, cratesToMove[k]);
                }
            }

            return stacks;
        }
    }
}