using System;

namespace FileSystemViewer
{
    internal class DriveName : DefaultFolder
    {
        public DriveName(string fullPath) : base(fullPath)
        {
            GetColor();
            Deep = 1;
            Name = FullPath;
            Offset = Step;
        }
        public bool IsLastChildDir { get; set; }
        public virtual void FormatPrefix()
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
        protected virtual void GetColor()
        {
            Color = ConsoleColor.Green;
        }
    }
}