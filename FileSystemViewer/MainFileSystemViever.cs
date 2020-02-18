using System;

namespace FileSystemViewer
{
    internal class MainFileSystemViever
    {
        public static void Main()
        {
            Console.Title = "FileSystemViever";
            FileSystemViewerRun viewer = new FileSystemViewerRun();
            viewer.Run();
        }
    }
}