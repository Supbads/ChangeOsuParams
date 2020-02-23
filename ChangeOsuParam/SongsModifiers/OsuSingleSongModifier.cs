using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace ChangeOsuParam
{
    public class OsuSingleSongModifier : OsuSongsModifier
    {
        public OsuSingleSongModifier(string osuDirectory) : base(osuDirectory)
        {
        }

        protected override IEnumerable<string> FetchSongsFolder(string selectedFolderName)
        {
            var folder = Directory.GetDirectories(this.OsuSongsPath)
                      .FirstOrDefault(folderPath =>
                           Path.GetFileName(folderPath)
                               .Contains(selectedFolderName, StringComparison.InvariantCultureIgnoreCase));


            return Enumerable.Empty<string>().Append(folder);
        }
    }
}