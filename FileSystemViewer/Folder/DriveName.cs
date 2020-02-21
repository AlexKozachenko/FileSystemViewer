using System;
using System.IO;

namespace FileSystemViewer
{
    internal class DriveName : DefaultComponent
    {
        protected const int DriveDepth = 1;
        protected const int StepOffset = 2;
        public DriveName(string fullPath) : base(fullPath)
        {
            SetName();
        }
        public override ConsoleColor Color => ConsoleColor.Green;
        public override int Depth => DriveDepth;
        public override int Offset => Depth * StepOffset;
        public override string Prefix { get; protected set; } = ((char)0x251C).ToString() + (char)0x2500;
        public void SetLastContainerPrefix()
        {
            Prefix = Prefix.Replace((char)0x251C, (char)0x2514);
        }
        protected virtual void SetName()
        {
            Name = FullPath;
        }
        protected override void GetChildren()
        {
            foreach (string directory in Directory.GetDirectories(FullPath))
            {
                Children.Add(new FolderName(directory, Prefix));
            }
            MarkLastContainer();
            foreach (string file in Directory.GetFiles(FullPath))
            {
                Children.Add(new FileName(file, Prefix));
            }
        }
    }
}