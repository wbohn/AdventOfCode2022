using AdventOfCode;
using AoCHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public abstract class AbstractDayTest
    {
        protected readonly AbstractBaseDay _dayToTest;
        protected readonly string _part1Solution;
        protected readonly string _part2Solution;

        protected AbstractDayTest(AbstractBaseDay dayToTest)
        {
            _dayToTest = dayToTest;

            var solutionPath = _dayToTest.InputFilePath.Replace("Inputs", "Solutions");
            var solutions = File.ReadAllLines(solutionPath);

            _part1Solution = solutions[0];
            _part2Solution = solutions[1];
        }

        [Fact]
        public async void TestSolve_1()
        {
            var result = await _dayToTest.Solve_1();
            Assert.Equal(result, _part1Solution);
        }

        [Fact]
        public async void TestSolve_2()
        {
            var result = await _dayToTest.Solve_2();
            Assert.Equal(result, _part2Solution);
        }
    }
}
