namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly List<int> _caloriesSums = new();

    public Day01()
    {
        var lines = File.ReadAllLines(InputFilePath);

        List<int> currentCalories = new();

        foreach (string line in lines)
        {
            if (line == string.Empty)
            {
                _caloriesSums.Add(currentCalories.Sum());
                currentCalories = new();
            }
            else
            {
                var caloriesNumber = int.Parse(line);
                currentCalories.Add(caloriesNumber);
            }
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var maxCalories = _caloriesSums.Max();
        return ValueTask.FromResult(maxCalories.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        _caloriesSums.Sort();
        _caloriesSums.Reverse();

        var sumTopThree = _caloriesSums.Take(3).Sum();
        return ValueTask.FromResult(sumTopThree.ToString());
    }
}
