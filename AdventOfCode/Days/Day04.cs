using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day04 : AbstractBaseDay
    {
        private int totalContained = 0;
        private int totalOverlaps = 0;

        public override ValueTask<string> Solve_1()
        {
            foreach (var elfPair in _lines)
            {
                var assignments = elfPair.Split(",");
                var range1 = GetAssignmentRange(assignments[0]);
                var range2 = GetAssignmentRange(assignments[1]);

                if (RangesContained(range1, range2)) {
                    totalContained++;
                }
            }
            return ValueTask.FromResult(totalContained.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            foreach (var elfPair in _lines)
            {
                var assignments = elfPair.Split(",");
                var range1 = GetAssignmentRange(assignments[0]);
                var range2 = GetAssignmentRange(assignments[1]);

                if (RangesOverlap(range1, range2))
                {
                    totalOverlaps++;
                }
            }
            return ValueTask.FromResult(totalOverlaps.ToString());
        }

        private static List<int> GetAssignmentRange(string assigmnet)
        {
            var bounds = assigmnet.Split("-");
            var rangeStart = int.Parse(bounds[0]);
            var rangeEnd = int.Parse(bounds[1]);

            return Enumerable.Range(rangeStart, (rangeEnd - rangeStart) + 1).ToList();
        }

        private static bool RangesContained(List<int> range1, List<int> range2)
        {
            return range1.All(value => range2.Contains(value)) ||
                range2.All(value => range1.Contains(value));
        }

        private static bool RangesOverlap(List<int> range1, List<int> range2)
        {
            return range1.Any(value => range2.Contains(value)) ||
                range2.Any(value => range1.Contains(value));
        }
    }
}
