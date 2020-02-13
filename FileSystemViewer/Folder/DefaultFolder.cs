﻿using System;

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
        public virtual int Deep { get; protected set; }
        public string FullPath { get; protected set; } 
        //по умолчанию папка cчитается не пустой, пока не пройдет проверку при раскрытии в методе Open
        public bool IsEmpty { get; set; }
        public bool IsOpen { get; set; }
        public virtual string Name { get; protected set; } 
        public virtual int Offset { get; protected set; }
        public string Prefix { get; protected set; }
    }
}