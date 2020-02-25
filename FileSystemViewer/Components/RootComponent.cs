using System;
using System.IO;

namespace FileSystemViewer
{
    internal class RootComponent : DefaultComponent
    {
        private const string RootFullPath = "";
        private const string RootName = "ThisPC";

        public RootComponent()
        {
            FullPath = RootFullPath;
            Name = RootName;
        }

        public override ConsoleColor Color => ConsoleColor.Blue;

        protected override void GetChildren()
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                Children.Add(new DriveComponent(drive.Name));
            }
            DriveComponent.MarkLastContainer();
        }
    }
}