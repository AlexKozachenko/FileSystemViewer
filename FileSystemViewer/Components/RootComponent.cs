using System;
using System.IO;

namespace FileSystemViewer
{
    internal class RootComponent : DefaultComponent
    {
        private const string RootFullPath = "";

        public RootComponent()
        {
            FullPath = RootFullPath;
            Name = "ThisPC";
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