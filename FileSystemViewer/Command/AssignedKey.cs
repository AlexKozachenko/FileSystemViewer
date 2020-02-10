using System;

namespace FileSystemViewer
{
    internal class AssignedKey
    {
        public ConsoleKey Key { get; set; }
        public Command Command { get; set; }
        public AssignedKey(ConsoleKey key, Command command)
        {
            Key = key;
            Command = command;
        }
    }
}