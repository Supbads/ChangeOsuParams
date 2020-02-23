using System;

namespace ChangeOsuParam
{
    public class InputArguments
    {
        public string SelectedFolderName { get; set; }

        public bool ChangeOnlyFirstMatchedFolder { get; set; }

        public string DifficultySetting { get; set; }

        public string NewDifficultyValue { get; set; }

        public void ReadInputArguments()
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
        }
    }
}