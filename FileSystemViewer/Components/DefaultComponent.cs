using System;
using System.Collections.Generic;

namespace FileSystemViewer
{
    internal abstract class DefaultComponent 
    {
        public DefaultComponent()
        {
        }
        public DefaultComponent(string fullPath)
        {
            FullPath = fullPath;
        }

        protected static IList<DriveComponent> Children { get; } = new List<DriveComponent>();

        public abstract ConsoleColor Color { get; }

        public virtual int Depth { get; }

        public string FullPath { get; protected set; }
    
        protected virtual bool IsEmpty { get; set; }

        public bool IsOpen { get; protected set; }

        public string Name { get; protected set; }

        public virtual int Offset { get; }

        public virtual string Prefix { get; protected set; }

        public void CloseComponent(List<DefaultComponent> components, int position)
        {
            if (IsOpen)
            {
                components.RemoveAll(folder => folder.FullPath.Contains(FullPath) && folder.FullPath != FullPath);
                IsOpen = false;
            }
        }

        protected abstract void GetChildren();

        public void OpenComponent(List<DefaultComponent> components, int position)
        {
            //первый раз проверяется любая папка (не пустая по умолчанию, т.к. неизвестно, пустая она или нет),
            //если пустая, при следующем раскрытии процесс получения вложенных папок отменяется
            if (!IsOpen && !IsEmpty)
            {
                GetChildren();
                if (Children.Count > 0)
                {
                    int nextPosition = position + 1;
                    components.InsertRange(nextPosition, Children);
                    IsOpen = true;
                    Children.Clear();
                }
                else
                {
                    IsEmpty = true;
                }
            }
        }
    }
}