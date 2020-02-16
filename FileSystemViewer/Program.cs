using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemViewer
{
    internal class Program
    {
        private readonly List<DefaultFolder> folders = new List<DefaultFolder>()
        {
            new Root()
        };
        KeyAssign keys;
        public Program()
        {
            Selection = new SelectionAndScrolling(folders);
            ManageFolder = new FolderManagement(Selection);
            Write = new Draw(Selection);
            keys = new KeyAssign(Selection, ManageFolder);
            ManageFolder.Open();
            ++Selection.Position;
        }
        public SelectionAndScrolling Selection { get; set; }
        public FolderManagement ManageFolder { get; }
        public Draw Write { get; }
        public void Run()
        {
            try
            {
                while (true)
                {
                    Write.WriteScreen();
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