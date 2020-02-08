using System;

namespace FileSystemViewer
{
    internal class AssignedKey
    {
        public ConsoleKey Key { get; set; }
        public ICommand Command { get; set; }
        public AssignedKey(ConsoleKey key, ICommand command)
        {
            Key = key;
            Command = command;
        }
    }
}
