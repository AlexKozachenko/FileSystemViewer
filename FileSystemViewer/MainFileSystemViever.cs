using System;
using System.IO;

namespace FileSystemViewer
{
    internal class MainFileSystemViever
    {
        public static void Main()
        {
            Console.Title = "FileSystemViewer";
            Program viewer = new Program();
            KeyAssign keys = new KeyAssign(viewer);
            Run(keys);
        }
        public static void Run(KeyAssign keys)
        {
            try
            {
                while (true)
                {
                    keys.Viewer.WriteScreen();
                    keys.Read(Console.ReadKey().Key);
                }
            }
            catch (IOException)
            {
                Run(keys);
            }
            catch (UnauthorizedAccessException)
            {
                Run(keys);
            }
        }
    }
}