using System;

namespace FileSystemViewer
{
    internal class Root : DefaultFolder
    {
        public Root()
        {
            Color = ConsoleColor.DarkBlue;
            Deep = 0;
            FullPath = "";
            Name = "ThisPC";
            Offset = 0;
        }
    }
}