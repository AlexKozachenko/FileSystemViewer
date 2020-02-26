using FileSystemViewer.Commands;
using System;
using System.IO;

namespace FileSystemViewer
{
    internal class FileSystemViewerRun
    {
        ProgramLogic fileViewer = new ProgramLogic();
        KeyAssign keys;
        public FileSystemViewerRun()
        {
            Console.Title = "FileSystemViever";
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;
            keys = new KeyAssign(fileViewer);
            fileViewer.Open();
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