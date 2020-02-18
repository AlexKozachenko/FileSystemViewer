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
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;
            keys = new KeyAssign(logic);
            logic.Open();
            ++logic.Position;
        }
        public void Run()
        {
            try
            {
                while (true)
                {
                    logic.WriteScreen();
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