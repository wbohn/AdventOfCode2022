using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AdventOfCode.Days
{
    public class Day08 : AbstractBaseDay
    {
        private readonly DataTable _grid = new("Trees");

        public Day08()
        {
            GetGrid();
        }
        public override ValueTask<string> Solve_1()
        {
            var visibleTrees = 0;

            for (var r = 0; r < _grid.Rows.Count; r++)
            {
                DataRow row = _grid.Rows[r];

                for (var c = 0; c < _grid.Columns.Count; c++)
                {
                    var tree = (int)row[c];
                    if (TreeIsVisible(tree, r, c))
                    {
                        visibleTrees++;
                    }
                }
            }

            return ValueTask.FromResult(visibleTrees.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            List<int> scenicScores = new();

            for (var r = 0; r < _grid.Rows.Count; r++)
            {
                DataRow row = _grid.Rows[r];

                for (var c = 0; c < _grid.Columns.Count; c++)
                {
                    var tree = (int)row[c];
                    var left = GetViewingDistance(tree, GetLeftTrees(r, c).AsEnumerable().Reverse().ToList());
                    var right = GetViewingDistance(tree, GetRightTrees(r, c));
                    var up = GetViewingDistance(tree, GetUpTrees(r, c).AsEnumerable().Reverse().ToList());
                    var down = GetViewingDistance(tree, GetDownTrees(r, c));

                    scenicScores.Add(left * right * up * down);
                }
            }

            return ValueTask.FromResult(scenicScores.Max().ToString());
        }
        private void GetGrid()
        {
            for (int i = 0; i < _lines[0].Length; i++)
            {
                _grid.Columns.Add(new DataColumn(i.ToString(), Type.GetType("System.Int32")!));
            }

            foreach (var line in _lines)
            {
                DataRow row = _grid.NewRow();
                for (int i = 0; i < _lines.Length; i++)
                {
                    row[i] = int.Parse(line[i].ToString());
                }
                _grid.Rows.Add(row);
            }
        }

        private bool TreeIsVisible(int tree, int row, int col)
        {
            if (row == 0 || col == 0 || col == _grid.Columns.Count - 1 || row == _grid.Rows.Count - 1)
            {
                return true;
            }

            return VisibleInRow(tree, row, col) || VisibleInCol(tree, row, col); ;
        }

        private List<int> GetLeftTrees(int row, int col)
        {
            List<int> leftTrees = new();
            for (var c = 0; c < col; c++)
            {
                leftTrees.Add((int)_grid.Rows[row][c]);
            }
            return leftTrees;
        }

        private List<int> GetRightTrees(int row, int col)
        {
            List<int> rightTrees = new();
            for (var c = col + 1; c < _grid.Columns.Count; c++)
            {
                rightTrees.Add((int)_grid.Rows[row][c]);
            }
            return rightTrees;
        }

        private bool VisibleInRow(int tree, int row, int col)
        {
            List<int> previousInRow = GetLeftTrees(row, col);

            List<int> nextInRow = GetRightTrees(row, col);

            return previousInRow.All(t => t < tree) || nextInRow.All(t => t < tree);
        }

        private List<int> GetUpTrees(int row, int col)
        {
            List<int> upTrees = new();
            for (var r = 0; r < row; r++)
            {
                upTrees.Add((int)_grid.Rows[r][col]);
            }
            return upTrees;
        }

        private List<int> GetDownTrees(int row, int col)
        {
            List<int> downTrees = new();
            for (var r = row + 1; r < _grid.Rows.Count; r++)
            {
                downTrees.Add((int)_grid.Rows[r][col]);
            }
            return downTrees;
        }
        private bool VisibleInCol(int tree, int row, int col)
        {
            List<int> previousInCol = GetUpTrees(row, col);

            List<int> nextInCol = GetDownTrees(row, col);
            return previousInCol.All(t => t < tree) || nextInCol.All(t => t < tree);

        }
        private int GetViewingDistance(int tree, List<int> otherTrees)
        {
            var viewingDistance = 0;
            for (int i = 0; i < otherTrees.Count; i++)
            {
                viewingDistance++;
                if (otherTrees[i] >= tree)
                {
                    break;
                }
            }
            return viewingDistance;
        }
    } 
}
