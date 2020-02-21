using System;
using System.IO;

namespace FileSystemViewer
{
    internal class Root : DefaultComponent
    {
        public Root()
        {
            Name = "ThisPC";
        }
        public override ConsoleColor Color => ConsoleColor.Blue;
        protected override void GetChildren()
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                Children.Add(new DriveName(drive.Name));
            }
            MarkLastContainer();
        }
    }
}