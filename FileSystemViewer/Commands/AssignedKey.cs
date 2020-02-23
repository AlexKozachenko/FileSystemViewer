using System;

namespace FileSystemViewer
{
    internal class AssignedKey
    {
        public AssignedKey(ConsoleKey key, DefaultCommand command)
        {
            Key = key;
            Command = command;
        }

        public DefaultCommand Command { get; }
        public ConsoleKey Key { get; }
    }
}