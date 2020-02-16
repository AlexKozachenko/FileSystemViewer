using System;

namespace FileSystemViewer
{
    internal class MainFileSystemViever
    {
        public static void Main()
        {
            Console.Title = "FileSystemViever";
            Program viewer = new Program();
            viewer.Run();
        }
    }
}