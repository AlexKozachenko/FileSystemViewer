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
        private Draw write;
        public Program()
        {
            Cursor = new ScrollLogic(folders);
            OpenClose = new OpenCloselogic(Cursor);
            write = new Draw(Cursor);
            OpenClose.Open();
        }
        public ScrollLogic Cursor { get; set; }
        public OpenCloselogic OpenClose { get; }
        public void Run(KeyAssign keys)
        {
            try
            {
                while (true)
                {
                    write.WriteScreen();
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