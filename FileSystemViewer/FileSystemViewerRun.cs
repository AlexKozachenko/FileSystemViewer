using System;
using System.IO;

namespace FileSystemViewer
{
    internal class FileSystemViewerRun
    {
        ProgramLogic logic = new ProgramLogic();
        KeyAssign keys;
        public FileSystemViewerRun()
        {
            Console.Title = "FileSystemViever";
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;
            keys = new KeyAssign(logic);
            logic.Open();
            ++logic.SelectionPosition;
        }
        public void Run()
        {
            try
            {
                while (true)
                {
                    logic.ShowFileSystem();
                    keys.Read(Console.ReadKey().Key);
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