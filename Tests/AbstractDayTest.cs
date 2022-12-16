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
            Assert.Equal(_part1Solution, result);
        }

        [Fact]
        public async void TestSolve_2()
        {
            var result = await _dayToTest.Solve_2();
            Assert.Equal(_part2Solution, result);
        }
    }
}
