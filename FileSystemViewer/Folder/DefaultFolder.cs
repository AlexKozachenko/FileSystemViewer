using System;

namespace FileSystemViewer
{
    internal abstract class DefaultFolder
    {
        public DefaultFolder(string fullPath)
        {
            FullPath = fullPath;
        }
        public ConsoleColor Color { get; set; }
        public int Deep { get; set; } 
        public string FullPath { get; set; }
        //по умолчанию папка cчитается не пустой, пока не пройдет проверку при раскрытии в методе Open
        public bool IsEmpty { get; set; } 
        public bool IsLastChildDir { get; set; } 
        public bool IsOpen { get; set; } 
        public string Name { get; set; }
        public int Offset { get; set; }
        public string Prefix { get; set; }
        protected int Step { get; } = 2;
    }
}