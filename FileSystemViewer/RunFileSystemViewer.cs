using FileSystemViewer.Commands;
using System;
using System.IO;

namespace FileSystemViewer
{
    internal class RunFileSystemViewer
    {
        ProgramLogic fileViewer = new ProgramLogic();
        KeyAssign keys;
        public RunFileSystemViewer()
        {
            Console.Title = "FileSystemViewer";
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;
            keys = new KeyAssign(fileViewer);
            fileViewer.Expand();
            ++fileViewer.SelectionPosition;
        }
        public void Run()
        {
            try
            {
                while (true)
                {
                    fileViewer.ShowFileSystem();
                    keys.ExecuteAssignedCommand(Console.ReadKey().Key);
                }
            }
            catch (IOException)
            {
                Run();
            }
            catch (UnauthorizedAccessException)
            {
                Run();
            }
        }
    }
}