using System;
using System.IO;

namespace ChangeOsuParam
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                try
                {
                    InputArguments inputArguments = ReadInputArguments();

                    var currentDir = Directory.GetCurrentDirectory(); // Assume we're in the osu! directory
                    var osuDirModifier = new OsuDirectoryModifier(currentDir);

                    osuDirModifier.ModifyOsuDirectoriesSafe(inputArguments);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static InputArguments ReadInputArguments()
        {
            var inputArguments = new InputArguments();

            Console.WriteLine("Input a folder name or part of its name.");
            inputArguments.SelectedFolderName = Console.ReadLine();

            Console.WriteLine("Change only the first matched folder: (y/n) defaults n");
            string changeOnlyFirstMatchStr = Console.ReadLine();

            bool changeOnlyFirstMatch = true;
            if (changeOnlyFirstMatchStr.ToLower().StartsWith("y"))
            {
                changeOnlyFirstMatch = false;
            }

            inputArguments.ChangeOnlyFirstMatchedFolder = changeOnlyFirstMatch;

            Console.WriteLine("Specify Difficulty setting to change");
            inputArguments.DifficultySetting = Console.ReadLine();

            Console.WriteLine("Specify new difficulty value");
            inputArguments.NewDifficultyValue = Console.ReadLine();
            return inputArguments;
        }
    }
}
