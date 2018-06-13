using System;

namespace FileSystemViewer
{
    internal abstract class Key
    {
        private ConsoleKey input;

        public ConsoleKey Input
        {
            get => input;
            set => input = value;
        }

        public abstract void Action(FileViewer viewer);
    }
}