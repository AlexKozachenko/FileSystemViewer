using System;

namespace FileSystemViewer
{
    internal abstract class DefaultFolder
    {
        public DefaultFolder()
        {
        }
        public DefaultFolder(string fullPath)
        {
            FullPath = fullPath;
        }
        public abstract ConsoleColor Color { get; }
        public int Deep { get; protected set; }
        public string FullPath { get; protected set; } 
        //по умолчанию папка cчитается не пустой, пока не пройдет проверку при раскрытии в методе Open
        public bool IsEmpty { get; set; }
        public bool IsOpen { get; set; }
        public string Name { get; protected set; } 
        public int Offset { get; protected set; }
        public string Prefix { get;  set; }
    }
}