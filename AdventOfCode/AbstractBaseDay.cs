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
        protected readonly string _text;
        protected AbstractBaseDay()
        {
            _text = File.ReadAllText(InputFilePath);
            _lines = _text.Split("\r\n");
        }
    }
}
