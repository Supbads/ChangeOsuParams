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
                    InputArguments inputArguments = new InputArguments();
                    inputArguments.ReadInputArguments();

                    var currentDir = Directory.GetCurrentDirectory(); // Assume we're in the osu! directory
                    var osuDirModifier = new OsuDirectoryModifier(currentDir);

                    osuDirModifier.TryModifyOsuDirectories(inputArguments);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
