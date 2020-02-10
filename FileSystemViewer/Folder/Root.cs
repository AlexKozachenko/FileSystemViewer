using System;

namespace FileSystemViewer
{
    internal class Root : DefaultFolder
    {
        public Root(string fullPath) : base(fullPath)
        {
            Color = ConsoleColor.DarkBlue;
            Name = "ThisPC";
        }
    }
}