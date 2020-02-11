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
        public int Deep { get; set; }
        public string FullPath { get; set; } 
        //по умолчанию папка cчитается не пустой, пока не пройдет проверку при раскрытии в методе Open
        public bool IsEmpty { get; set; }
        public bool IsOpen { get; set; }
        public virtual string Name { get; set; } 
        public int Offset { get; set; }
        public string Prefix { get; set; }
        public virtual ConsoleColor PrefixColor { get; }
    }
}