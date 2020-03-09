using static FileSystemViewer.Components.Literals;
using System;
using System.IO;

namespace FileSystemViewer.Components
{
    internal class DriveComponent : DefaultComponent
    {
        public DriveComponent(string fullPath) : base(fullPath) => SetName();

        public override ConsoleColor Color => ConsoleColor.Green;

        protected virtual int Depth => DriveDepth;

        public override int Offset => Depth * StepOffset;

        public override string Prefix { get; protected set; } = ConnectingPart_T.ToString() + ConnectingPart_Hyphen;

        public static void MarkLastContainer()
        {
            if (Children.Count > 0)
            {
                DriveComponent lastContainer = Children[Children.Count - 1];
                lastContainer.Prefix = lastContainer.Prefix.Replace(ConnectingPart_T, ConnectingPart_L);
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

        protected virtual void SetName() => Name = FullPath;
    }
}