using AdventOfCode;

namespace Tests
{
    public class Day01Test
    {
        private Day01 _day;
        
        private readonly string _input;
        private readonly string _part1Solution;
        private readonly string _part2Solution;

        public Day01Test()
        {
            _day = new Day01();

            _input = File.ReadAllText(_day.InputFilePath);

            var solutionPath = _day.InputFilePath.Replace("Inputs", "Solutions");
            var solutions = File.ReadAllLines(solutionPath);
            
            _part1Solution = solutions[0];
            _part2Solution = solutions[1];
        }
        [Fact]
        public async void  TestSolve_1()
        {
            var result = await _day.Solve_1();

            Assert.Equal(result, _part1Solution);
        }

        [Fact]
        public async void TestSolve_2()
        {
            var result = await _day.Solve_2();

            Assert.Equal(result, _part2Solution);
        }
    }
}