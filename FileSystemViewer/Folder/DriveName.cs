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

        public override void FormatPrefix(string parentPrefix)
        {
            if (IsLastChildDir)
            {
                Prefix = ((char)0x2514).ToString() + (char)0x2500;
            }
            else
            {
                Prefix = ((char)0x251C).ToString() + (char)0x2500;
            }
        }
    }
}