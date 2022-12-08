using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day07 : AbstractBaseDay
    {

        private readonly Dictionary<string, int> _directorySizes = new();

        private readonly Dictionary<string, Directory> _directoryMap = new();
        public Day07()
        {
            MapDirectories();
            CalculateSizes();
        }

        private void MapDirectories()
        {
            Directory? currentDirectory = null;

            foreach (var line in _lines)
            {
                if (line.StartsWith("$ ls")) {
                    continue;
                }

                var tokens = line.Split(" ");

                if (line.StartsWith("$ cd"))
                {
                    if (line == "$ cd ..")
                    {
                        var pathTokens = currentDirectory!.Path.Split("/").ToList();
                        pathTokens.RemoveAt(pathTokens.Count - 1);

                        pathTokens.RemoveAt(pathTokens.Count - 1);

                        var parentPath = string.Join("/", pathTokens) + "/";
                        currentDirectory = _directoryMap[parentPath];
                    }
                    else
                    {
                        var targetDir = tokens.Last();
                        var targetDirPath = currentDirectory == null ? "/" : currentDirectory.Path + targetDir + "/";

                        if (!_directoryMap.ContainsKey(targetDirPath))
                        {
                            Directory directory = new()
                            {
                                Path = targetDirPath
                            };

                            _directoryMap[directory.Path] = directory;
                            currentDirectory?.Children.Add(directory);

                            currentDirectory = directory;
                        }
                    }
                } else
                {
                    if (!line.StartsWith("dir"))
                    {
                        var fileName = tokens.Last();
                        if (!currentDirectory!.Files.Contains(fileName))
                        {
                            currentDirectory!.Files.Add(fileName);
                            var fileSize = int.Parse(tokens[0]);
                            currentDirectory!.Size += fileSize;
                        }
                    }
                }
            }
        }

        private void CalculateSizes()
        {
            foreach(var item in _directoryMap)
            {
                var directory = item.Value;
                var directorySize = CalculateDirectory(directory);
                _directorySizes[item.Key] = directorySize;
            }
        }

        private int CalculateDirectory(Directory directory)
        {
            var size = directory.Size;
            foreach (var item in directory.Children)
            {
                size += CalculateDirectory(item);
            }

            return size;
        }
        public override ValueTask<string> Solve_1()
        {
            var answer = _directorySizes.Select(d => d.Value)
                .Where(s => s <= 100000)
                .Sum();

            return ValueTask.FromResult(answer.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int diskSpace = 70000000;
            int spaceRequirements = 30000000;
            int unusedSpace = diskSpace - _directorySizes["/"];
            int spaceToClear = spaceRequirements - unusedSpace;

            var sizeOfdirToDelete = _directorySizes.Select(k => k.Value)
                .Where(s => s >= spaceToClear)
                .OrderBy(s => s)
                .Take(1)
                .First();

            return ValueTask.FromResult(sizeOfdirToDelete.ToString());

        }
    }
    internal class Directory
    {
        public string Path { get; set; } = null!;
        public List<Directory> Children = new();
        public List<string> Files = new();
        public int Size = 0;
    }
}
