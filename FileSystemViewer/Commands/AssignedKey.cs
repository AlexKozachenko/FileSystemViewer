using System;

namespace FileSystemViewer
{
    internal class AssignedKey
    {
        public AssignedKey(ConsoleKey key, Command command)
        {
            Key = key;
            Command = command;
        }

        public Command Command { get; }
        public ConsoleKey Key { get; }
    }
}