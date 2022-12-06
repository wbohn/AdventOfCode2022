using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day06 : AbstractBaseDay
    {
        private readonly string _signal;

        public Day06()
        {
            _signal = _text;
        }

        public override ValueTask<string> Solve_1()
        {
            var charsRead = 0;
            List<char> buffer = new();

            foreach (var c in _signal)
            {
                if (buffer.Distinct().Count() == 4)
                {
                    break;
                }
                if (buffer.Count == 4)
                {
                    buffer.RemoveAt(0);
                }
                buffer.Add(c);
                charsRead++;
            }

            return ValueTask.FromResult(charsRead.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var charsRead = 0;
            List<char> buffer = new();
            foreach (var c in _signal)
            {
                if (buffer.Distinct().Count() == 14)
                {
                    break;
                }
                if (buffer.Count == 14)
                {
                    buffer.RemoveAt(0);
                }
                buffer.Add(c);
                charsRead++;
            }
            return ValueTask.FromResult(charsRead.ToString());
        }
    }
}
