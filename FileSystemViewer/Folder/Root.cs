using System;
using System.IO;


namespace FileSystemViewer
{
    internal class Root : DefaultFolder
    {
        public Root()
        {
            FullPath = "";
            Name = "ThisPC";
        }
        public override ConsoleColor Color => ConsoleColor.Blue;
    } 
}