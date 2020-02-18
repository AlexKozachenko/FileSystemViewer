using System;

namespace FileSystemViewer
{
    internal class DriveName : DefaultFolder
    {
        protected const int DriveDepth = 1;
        protected const int Step = 2;
        public DriveName(string fullPath) : base(fullPath)
        {
            SetName();
        }
        public override ConsoleColor Color => ConsoleColor.Green;
        public override int Depth => DriveDepth;
        public override int Offset => Depth * Step;
        public override string Prefix { get; set; } = ((char)0x251C).ToString() + (char)0x2500;
        public void SetLastContainerPrefix()
        {
            Prefix = Prefix.Replace((char)0x251C, (char)0x2514);
        }
        protected virtual void SetName()
        {
            Name = FullPath;
        }
    }
}