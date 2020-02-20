using System;
using System.Collections.Generic;

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
        public virtual List<DriveName> Children { get; } = new List<DriveName>();
        public abstract ConsoleColor Color { get; }
        public virtual int Depth { get; protected set; }
        public string FullPath { get; protected set; } = "";
        public virtual bool IsEmpty { get; set; }
        public virtual bool IsOpen { get; set; }
        public string Name { get; protected set; }
        public virtual int Offset { get; protected set; }
        public virtual string Prefix { get; protected set; }
        public bool WasOpened { get; set; }
        public abstract void GetChildren();
        protected void SetLastContainer()
        {
            if (Children.Exists(folder => !folder.IsEmpty))
            {
                Children.FindLast(folder => !folder.IsEmpty).SetLastContainerPrefix();
            }
        }
    }
}