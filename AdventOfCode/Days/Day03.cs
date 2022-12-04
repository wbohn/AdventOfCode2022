using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day03 : AbstractBaseDay
    {
        private int _totalItemPriority = 0;
        private int _totalGroupPriority = 0;
        public override ValueTask<string> Solve_1()
        {
            foreach (var ruckSack in _lines)
            {
                var compartment1 = ruckSack[..(ruckSack.Length / 2)];
                var compartment2 = ruckSack.Substring(ruckSack.Length / 2, ruckSack.Length / 2);

                var commonItem = compartment1.Intersect(compartment2).FirstOrDefault();

                _totalItemPriority += GetItemPriority(commonItem);
            }
            return ValueTask.FromResult(_totalItemPriority.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var elfGroups = _lines.Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 3)
                .Select(g => g.Select(x => x.Value).ToList())
                .ToList();

            elfGroups.ForEach(g =>
            {
                var commonItem = g[0].Intersect(g[1]).Intersect(g[2]).FirstOrDefault();
                _totalGroupPriority += GetItemPriority(commonItem);
            });

            return ValueTask.FromResult(_totalGroupPriority.ToString());
        }

        public static int GetItemPriority(char c)
        {
            if (char.IsUpper(c))
            {
                return c - 38;
            }
            return c - 96; // lower
        }
    }
}
