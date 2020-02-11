using System;

namespace FileSystemViewer
{
    internal class Root : DefaultFolder
    {
        public Root()
        {
            FullPath = "";
            Name = "ThisPC";
        }
        public override ConsoleColor Color { get; } = ConsoleColor.Blue;
    } 
}
