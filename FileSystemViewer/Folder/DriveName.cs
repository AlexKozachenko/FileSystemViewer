using System;

namespace FileSystemViewer
{
    internal class DriveName : DefaultFolder
    {
        protected const int Step = 2;
        public DriveName(string fullPath) : base(fullPath)
        {
        }
        public override ConsoleColor Color { get; } = ConsoleColor.Green;
        public override int Deep => 1;
        public bool IsLastContainer { get; set; }
        public override string Name => FullPath;
        public override int Offset => Deep * Step;
        protected string PrePrefix { get; set; } = "";
        public virtual void FormatPrefix()
        {
            if (IsLastContainer)
            {
                Prefix = PrePrefix + (char)0x2514 + (char)0x2500;
            }
            else
            {
                Prefix = PrePrefix + (char)0x251C + (char)0x2500;
            }
        }
    }
}