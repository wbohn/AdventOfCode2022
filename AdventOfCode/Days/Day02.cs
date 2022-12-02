namespace AdventOfCode.Days;

public class Day02 : AbstractBaseDay
{
    private readonly Dictionary<string, string> _choices = new()
    {
        {"A", "Rock" },
        {"B", "Paper" },
        {"C", "Scissors" },
        {"X", "Rock" },
        {"Y", "Paper" },
        {"Z", "Scissors" }
    };

    private readonly Dictionary<string, string> _winningPlays = new()
    {
        {"Rock", "Paper" },
        {"Paper", "Scissors" },
        {"Scissors", "Rock" }
    };

    private readonly Dictionary<string, string> _losingPlays = new()
    {
        {"Paper", "Rock" },
        {"Scissors", "Paper" },
        {"Rock", "Scissors" }
    };

    private readonly Dictionary<string, int> _choiceScores = new()
    {
        {"Rock", 1 },
        {"Paper", 2 },
        {"Scissors", 3 }
    };

    private int _score = 0;
    private int _score2 = 0;

    public Day02()
    {
    }

    public override ValueTask<string> Solve_1()
    {
        foreach (var match in _lines)
        {
            var matchChoices = match.Split(" ");
            var opponent = _choices[matchChoices[0]];
            var mine = _choices[matchChoices[1]];

            _score += _choiceScores[mine];
            _score += DoComparison(opponent, mine);

        }
        
        var answer = _score.ToString();
        return ValueTask.FromResult(answer);
    }

    public override ValueTask<string> Solve_2()
    {
        foreach (var match in _lines)
        {
            var matchChoices = match.Split(" ");
            var opponentRaw = matchChoices[0];
            var opponentText = _choices[opponentRaw];

            var outcome = matchChoices[1];//_choices[matchChocies[1]];

            var play = GetPlay(opponentText, outcome);


            _score2 += GetChoiceScore(play);

            _score2 += DoComparison2(outcome);
        }

        var answer = _score2.ToString();
        return ValueTask.FromResult(answer);
    }

    private int DoComparison(string opponentChoice, string myChoice)
    {
        if (opponentChoice == myChoice)
        {
            return 3; // draw
        }

        if (_winningPlays[myChoice] == opponentChoice)
        {
            return 0; // lose
        } else
        {
            return 6; // win
        }
    }

    private int DoComparison2(string outcome)
    {
        if (outcome == "X")
        {
            return 0; // lose
        }

        if (outcome == "Y")
        {
            return 3; // draw
        }

        return 6; // win
    }

    private int GetChoiceScore(string choiceText)
    {
        return _choiceScores[choiceText];
    }

    private string GetPlay(string opponentChoice, string outcome)
    {
        if (outcome == "X") // lose
        {
            return _losingPlays[opponentChoice];
        }

        if (outcome == "Y") // draw
        {
            return opponentChoice;
        }

        return _winningPlays[opponentChoice]; // win
    }
}
