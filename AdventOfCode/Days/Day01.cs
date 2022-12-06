namespace AdventOfCode.Days;

public class Day01 : AbstractBaseDay
{
    private readonly IEnumerable<int> _caloriesSums;
    public Day01()
    {
        _caloriesSums = _text.Split("\r\n\r\n")
            .Select(e => e.Split("\r\n")
            .Select(e => int.Parse(e))
            .Sum());
    }

    public override ValueTask<string> Solve_1()
    {
        var maxCalories = _caloriesSums.Max();
        return ValueTask.FromResult(maxCalories.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var sumTopThree = _caloriesSums
            .OrderByDescending(c => c)
            .Take(3)
            .Sum();
        return ValueTask.FromResult(sumTopThree.ToString());
    }
}
