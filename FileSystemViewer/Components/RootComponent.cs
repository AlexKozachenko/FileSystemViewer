using static FileSystemViewer.Components.Literals;
using System;
using System.IO;

namespace FileSystemViewer.Components
{
    internal class RootComponent : DefaultComponent
    {
        public RootComponent()
        {
            FullPath = EmptyString;
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