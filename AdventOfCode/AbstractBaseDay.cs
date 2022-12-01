using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public abstract class AbstractBaseDay : BaseDay
    {
        protected readonly string[] _lines;

        protected AbstractBaseDay()
        {
            _lines = File.ReadAllLines(InputFilePath);
        }
    }
}
