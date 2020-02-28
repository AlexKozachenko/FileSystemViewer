using static FileSystemViewer.Components.Literals;
using System;
using System.IO;

namespace FileSystemViewer.Components
{
    internal class DriveComponent : DefaultComponent
    {
        public DriveComponent(string fullPath) : base(fullPath)
        {
            SetName();
        }

        public override ConsoleColor Color => ConsoleColor.Green;

        protected override int Depth => DriveDepth;

        public override int Offset => Depth * StepOffset;

        public override string Prefix { get; protected set; } = T_ConnectingPart.ToString() + Hyphen;

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
            Prefix = Prefix.Replace(T_ConnectingPart, L_ConnectingPart);
        }

        protected virtual void SetName()
        {
            Name = FullPath;
        }
    }
}