using System;
using System.IO;

namespace FileSystemViewer
{
    internal class Root : DefaultFolder
    {
        public Root()
        {
            Name = "ThisPC";
        }
        public override ConsoleColor Color => ConsoleColor.Blue;
        public override void GetChildren()
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                Children.Add(new DriveName(drive.Name));
            }
            SetLastContainer();
        }
    }
}