using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day11 : AbstractBaseDay
    {
        private List<Monkey> _monkeys = new();
        private string[] _monkeyConfigs = null!;
        public Day11()
        {
            _monkeyConfigs = _text.Split("\r\n\r\n");
        }

        public override ValueTask<string> Solve_1()
        {
            ParseMonkeys();
            DoRounds(20, 3);
            var monkeyBusiness = GetMonkeyBusiness();
            return ValueTask.FromResult(monkeyBusiness.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            ParseMonkeys();
            DoRounds(10000, 1);
            var monkeyBusiness = GetMonkeyBusiness();
            return ValueTask.FromResult(monkeyBusiness.ToString());
        }

        private void ParseMonkeys()
        {
            _monkeys = new();
            foreach (var config in _monkeyConfigs)
            {
                var variables = config.Split("\r\n  ").ToList();

                var monkeyName = variables[0].Split(":").First();
                var items = new Queue<int>(variables[1].Split(": ").Last().Split(", ").Select(i => int.Parse(i)));

                var operation = variables[2].Split("Operation: new = ").Last().Split(" ");
                var operationValue1 = operation.First();
                var operationType = operation[1];
                var operationValue2 = operation.Last();

                List<string> operationValues = new()
                {
                    operationValue1,
                    operationValue2
                };
                var testValue = int.Parse(variables[3].Split("Test: divisible by ").Last());
                var trueIndex = int.Parse(variables[4].Split("  If true: throw to monkey ").Last());
                var falseIndex = int.Parse(variables[5].Split("  If false: throw to monkey ").Last());

                Monkey monkey = new()
                {
                    Name = monkeyName,
                    Items = items,
                    ItemInspections = 0,
                    OperationType = operationType,
                    OperationValues = operationValues,
                    TestValue = testValue,
                    TrueMonkey = trueIndex,
                    FalseMonkey = falseIndex
                };
                _monkeys.Add(monkey);
            }
        }
        private void DoRounds(int numRounds, int worryFactor = 1)
        {
            for (int i = 0; i < numRounds; i++)
            {
                foreach (var monkey in _monkeys)
                {
                    foreach (var (item, nextIndex) in monkey.InspectItems(worryFactor))
                    {
                        var receivingMonkey = _monkeys[nextIndex];
                        receivingMonkey.CatchItem(item);
                    }
                }
            }
        }

        private int GetMonkeyBusiness()
        {
            var monkeyBusiness = 1;
            _monkeys.Select(m => m.ItemInspections)
                .OrderByDescending(m => m)
                .Take(2)
                .ToList()
                .ForEach(i =>
                {
                    monkeyBusiness *= i;
                });
            return monkeyBusiness;
        }
    }

    public class Monkey
    {
        public string Name { get; set; } = "";
        public Queue<int> Items { get; set; } = new();
        public int ItemInspections { get; set; }

        public string OperationType { get; set; } = null!;
        public List<string> OperationValues { get; set; } = new();
        public int TestValue { get; set; }
        public int TrueMonkey { get; set; }
        public int FalseMonkey { get; set; }

        public void CatchItem(int item)
        {
            Items.Enqueue(item);
        }

        public IEnumerable<(int Item, int NextMonkey)> InspectItems(int worryFactor = 1)
        {
            while (Items.Count > 0)
            {
                ItemInspections += 1;

                int item = Items.Dequeue();
                int operationResult = GetOperationResult(item);

                int worryLevel = operationResult / worryFactor;
                int nextMonkey;

                if (worryLevel % TestValue == 0)
                {
                    nextMonkey = TrueMonkey;
                }
                else
                {
                    nextMonkey = FalseMonkey;
                }

                yield return (worryLevel, nextMonkey);
            }
        }

        private int GetOperationResult(int initialValue)
        {
            List<int> arguments = new();
            foreach (var stringVal in OperationValues)
            {
                int val;
                if (stringVal == "old")
                {
                    val = initialValue;
                }
                else
                {
                    val = int.Parse(stringVal);
                }
                arguments.Add(val);
            }

            int operationResult;
            if (OperationType == "+")
            {
                operationResult = arguments[0] + arguments[1];
            }
            else
            {
                operationResult = arguments[0] * arguments[1];
            }

            return operationResult;
        }

    }
}
