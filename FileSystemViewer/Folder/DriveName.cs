using System;

namespace FileSystemViewer
{
    internal class DriveName : DefaultFolder
    {
        public DriveName(string fullPath) : base(fullPath)
        {
            GetDeep();
            GetOffset();
            GetName();
        }
        public override ConsoleColor Color { get; } = ConsoleColor.Green;
        public bool IsLastChildDir { get; set; }
        public override ConsoleColor PrefixColor { get; } = ConsoleColor.DarkGray;
        protected string PrePrefix { get; set; } = "";
        protected int Step { get; } = 2;
        public virtual void FormatPrefix()
        {
            if (IsLastChildDir)
            {
                Prefix = PrePrefix + (char)0x2514 + (char)0x2500;
            }
            else
            {
                Prefix = PrePrefix + (char)0x251C + (char)0x2500;
            }
        }
        protected virtual void GetDeep()
        {
            Deep = 1;
        }
        protected virtual void GetName()
        {
            Name = FullPath;
        }
        protected void GetOffset()
        {
            Offset = Deep * Step;
        }
    }
}