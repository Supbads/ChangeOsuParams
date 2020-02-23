using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace ChangeOsuParam
{
    public abstract class OsuSongsModifier
    {
        protected const string SongsDirectory = "\\Songs";
        private string _osuSongsPath;

        public OsuSongsModifier(string osuDirectory)
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

        public void TryModifyOsuDirectories(InputArguments inputArguments)
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
            var selectedSongFolders = this.FetchSongsFolder(inputArguments.SelectedFolderName).ToList();

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

        protected abstract IEnumerable<string> FetchSongsFolder(string selectedFolderName);
    }
}