using System;

namespace FileSystemViewer
{
    internal abstract class DefaultFolder
    {
        private const int step = 2;
        public DefaultFolder(string fullPath)
        {
            FullPath = fullPath;
            //по умолчанию папка cчитается не пустой, пока не пройдет проверку при раскрытии в методе Open
            IsEmpty = false;
            IsOpen = false;
            IsLastChildDir = false;
        }
        public ConsoleColor Color { get; set; }
        public int Deep { get; set; }
        public string FullPath { get; set; }
        public bool IsEmpty { get; set; }
        public bool IsLastChildDir { get; set; }
        public bool IsOpen { get; set; }
        public string Name { get; set; }
        public int Offset { get; set; }
        public string Prefix { get; set; }
        public int Step => step;
        public abstract void FormatPrefix(string parentPrePrefix);
    }
}