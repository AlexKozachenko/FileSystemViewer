using System;
using System.IO;

namespace FileSystemViewer
{
    internal class DriveComponent : DefaultComponent
    {
        protected const int DriveDepth = 1;
        private const char Hyphen = (char)0x2500;
        protected const char ContainerConnectingLinePart = (char)0x251C;
        protected const int StepOffset = 2;

        public DriveComponent(string fullPath) : base(fullPath)

        {
            SetName();
        }

        public override ConsoleColor Color => ConsoleColor.Green;

        protected override int Depth => DriveDepth;

        public override int Offset => Depth * StepOffset;

        public override string Prefix { get; protected set; } = ContainerConnectingLinePart.ToString() + Hyphen;

        public static void MarkLastContainer()
        {
            if (Children.Count > 0)
            {
                Children[Children.Count - 1].SetLastContainerPrefix();
            }
        }

        protected override void GetChildren()
        {
            foreach (string directory in Directory.GetDirectories(FullPath))
            {
                Children.Add(new FolderComponent(directory, Prefix));
            }
            MarkLastContainer();
            foreach (string file in Directory.GetFiles(FullPath))
            {
                Children.Add(new FileComponent(file, Prefix));
            }
        }

        private void SetLastContainerPrefix()
        {
            const char LastContainerConnectingLinePart = (char)0x2514;
            Prefix = Prefix.Replace(ContainerConnectingLinePart, LastContainerConnectingLinePart);
        }

        protected virtual void SetName()
        {
            Name = FullPath;
        }
    }
}