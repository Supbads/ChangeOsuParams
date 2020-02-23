using System;
using System.IO;

namespace ChangeOsuParam
{
    public class OsuMapModifier
    {
        private string _osuMapPath;

        public OsuMapModifier(string osuMapPath)
        {
            this._osuMapPath = osuMapPath;
        }

        public string OsuMapPath
        {
            get
            {
                return this._osuMapPath;
            }
            private set
            {
                if (!File.Exists(value))
                {
                    throw new ArgumentException($"Cannot find osu map for path: {value}");
                }

                this._osuMapPath = value;
            }
         }

        public void TryUpdateMapSettings(InputArguments arguments)
        {
            string[] osuMapLines = File.ReadAllLines(this._osuMapPath);
            int difficultyLineIndex = FindDifficultyIndex(arguments.DifficultySetting, osuMapLines);

            if (difficultyLineIndex < 0 || difficultyLineIndex > osuMapLines.Length)
            {
                throw new IndexOutOfRangeException($"Could not find difficulty in osu map file: {Path.GetFileName(this._osuMapPath)}");
            }

            UpdateFileLine(arguments, osuMapLines, difficultyLineIndex);
            SaveChangesToFile(osuMapLines);
        }

        private void UpdateFileLine(InputArguments arguments, string[] osuMapLines, int difficultyLineIndex)
        {
            string difficultyLineToUpdate = osuMapLines[difficultyLineIndex];
            string newLine = GetUpdatedDifficltyLine(arguments.NewDifficultyValue, difficultyLineToUpdate);
            osuMapLines[difficultyLineIndex] = newLine;
        }

        private int FindDifficultyIndex(string difficultySetting, string[] osuMapLines)
        {
            int result = -1;
            for (int i = 15; i < osuMapLines.Length; i++)
            {
                if (i > 50)
                {
                    break;
                }

                if (osuMapLines[i].StartsWith(difficultySetting))
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        private string GetUpdatedDifficltyLine(string newDifficultyValue, string difficultyLine)
        {
            var difficultyLineArgs = difficultyLine.Split(':');
            difficultyLineArgs[1] = newDifficultyValue;
            var newLine = string.Join(':', difficultyLineArgs);
            return newLine;
        }

        private void SaveChangesToFile(string[] osuMapLines)
        {
            File.WriteAllLines(this._osuMapPath, osuMapLines);
        }
    }
}