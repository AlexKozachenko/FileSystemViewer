using System;

namespace FileSystemViewer
{
    internal class Root : DefaultFolder
    {
        public Root(string fullPath) : base(fullPath)
        {
            Color = ConsoleColor.DarkBlue;
            Deep = 0;
            Name = "ThisPC";
            Offset = 0;
        }
    }
}