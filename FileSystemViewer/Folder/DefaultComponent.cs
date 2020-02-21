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
        protected static List<DriveName> Children { get; } = new List<DriveName>();
        public abstract ConsoleColor Color { get; }
        public virtual int Depth { get; protected set; }
        public string FullPath { get; protected set; } = "";
        protected virtual bool IsEmpty { get; set; }
        public  bool IsOpen { get; set; }
        public string Name { get; protected set; }
        public virtual int Offset { get; protected set; }
        public virtual string Prefix { get; protected set; }
        public void CloseComponent(List<DefaultComponent> component, int position)
        {
            if (IsOpen)
            {
                int nextPosition = position + 1;
                int allChildrenCount = component.FindLastIndex(folder => folder.FullPath.Contains(FullPath)) - position;
                component.RemoveRange(nextPosition, allChildrenCount);
                IsOpen = false;
            }
        }
        protected abstract void GetChildren();
        protected static void MarkLastContainer()
        {
            if (Children.Count > 0)
            {
                Children[Children.Count - 1].SetLastContainerPrefix();
            }
        }
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
                    Children.Clear();
                    IsOpen = true;
                }
                else
                {
                    IsEmpty = true;
                }
            }
        }
    }
}