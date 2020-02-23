using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ChangeOsuParam
{
    public class OsuDirectoryModifier
    {
        private const string SongsDirectory = "\\Songs";
        private string _osuSongsPath;

        public OsuDirectoryModifier(string osuDirectory)
        {
            this.OsuSongsPath = osuDirectory + SongsDirectory;
        }

        public bool ChangeOnlyFirstMatch { get; }

        public string OsuSongsPath
        {
            get
            {
                return this._osuSongsPath;
            }
            set
            {
                if (!Directory.Exists(value))
                {
                    throw new ArgumentException($"An Osu! Songs folder does not exist in the presented directory: {value}");
                }

                this._osuSongsPath = value;
            }
        }

        public void ModifyOsuDirectoriesSafe(InputArguments inputArguments)
        {
            try
            {
                ModifyOsuDirecotriesUnsafe(inputArguments);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ModifyOsuDirecotriesUnsafe(InputArguments inputArguments)
        {
            var selectedSongFolders = inputArguments.ChangeOnlyFirstMatchedFolder ? FetchOneSongFolder(inputArguments.SelectedFolderName, this.OsuSongsPath)
                : FetchSongFolders(inputArguments.SelectedFolderName, this.OsuSongsPath);
            selectedSongFolders = selectedSongFolders.ToList();

            foreach (var songFolder in selectedSongFolders)
            {
                var osuMapPaths = Directory.GetFiles(songFolder)
                    .Where(file => file.EndsWith(".osu"));

                foreach (var osuMapPath in osuMapPaths)
                {
                    var osuMapModifier = new OsuMapModifier(osuMapPath);
                    osuMapModifier.TryUpdateMapSettings(inputArguments);
                }
            }
        }

        private IEnumerable<string> FetchOneSongFolder(string selectedFolderName, string osuSongsPath)
        {
            var folder = Directory.GetDirectories(osuSongsPath)
                                  .FirstOrDefault(folderPath =>
                                       Path.GetFileName(folderPath)
                                           .Contains(selectedFolderName, StringComparison.InvariantCultureIgnoreCase));


            return Enumerable.Empty<string>().Append(folder);
        }

        private IEnumerable<string> FetchSongFolders(string selectedFolderName, string osuSongsPath)
        {
            return Directory.GetDirectories(osuSongsPath)
                            .Where(folder => folder.Contains(selectedFolderName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}