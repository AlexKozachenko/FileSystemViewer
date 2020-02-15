using System;

namespace FileSystemViewer
{
    internal class MainFileSystemViever
    {
        public static void Main()
        {
            Console.Title = "FileSystemViever";
            Program viewer = new Program();
            KeyAssign keys = new KeyAssign(viewer);
            viewer.Run(keys);
        }
    }
}