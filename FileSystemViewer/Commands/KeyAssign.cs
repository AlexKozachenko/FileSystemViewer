using FileSystemViewer.Commands.ConcreteCommands;
using System;

namespace FileSystemViewer.Commands
{
    internal class KeyAssign 
    {
        private readonly AssignedKey[] keys;

        public KeyAssign(ProgramLogic fileViewer)
        {
            keys = new AssignedKey[]
            {
                new AssignedKey(ConsoleKey.UpArrow, new MoveUp(fileViewer)),
                new AssignedKey(ConsoleKey.DownArrow, new MoveDown(fileViewer)),
                new AssignedKey(ConsoleKey.RightArrow, new Open(fileViewer)),
                new AssignedKey(ConsoleKey.LeftArrow, new Close(fileViewer)),
            };
        }

        public void ExecuteAssignedCommand(ConsoleKey input)
        {
            foreach (AssignedKey key in keys)
            {
                if (key.Key == input)
                {
                    key.Command.Execute();
                    break;
                }
            }
        }
    }
}