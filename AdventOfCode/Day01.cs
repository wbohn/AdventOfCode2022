namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string _input;

    public Day01()
    {
        _input = File.ReadAllText(InputFilePath);
        Console.WriteLine(_input);
    }

    public override ValueTask<string> Solve_1()
    {           
        Console.WriteLine("Solve_1()");
        return ValueTask.FromResult(_input);
    }

    public override ValueTask<string> Solve_2()
    {
        Console.WriteLine("Solve_2()");
        return ValueTask.FromResult(_input);
    }
}
