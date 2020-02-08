using System;

namespace FileSystemViewer
{
    internal class DriveName : DefaultFolder
    {
        public DriveName(string fullPath) : base(fullPath)
        {
            Color = ConsoleColor.Green;
            Deep = 1;
            Name = FullPath;
            Offset = Step;
        }
    }
}