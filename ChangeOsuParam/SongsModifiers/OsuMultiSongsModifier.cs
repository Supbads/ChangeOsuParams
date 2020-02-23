using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace ChangeOsuParam
{
    public class OsuMultiSongsModifier : OsuSongsModifier
    {
        public OsuMultiSongsModifier(string osuDirectory) : base(osuDirectory)
        {
        }

        protected override IEnumerable<string> FetchSongsFolder(string selectedFolderName)
        {
            return Directory.GetDirectories(this.OsuSongsPath)
                .Where(folderPath =>
                    Path.GetFileName(folderPath)
                        .Contains(selectedFolderName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
