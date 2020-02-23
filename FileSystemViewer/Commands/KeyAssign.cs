using System;

namespace FileSystemViewer
{
    internal class KeyAssign
    {
        private readonly AssignedKey[] keys;

        public KeyAssign(ProgramLogic logic)
        {
            keys = new AssignedKey[]
            {
                new AssignedKey(ConsoleKey.UpArrow, new MoveUp(logic)),
                new AssignedKey(ConsoleKey.DownArrow, new MoveDown(logic)),
                new AssignedKey(ConsoleKey.RightArrow, new Open(logic)),
                new AssignedKey(ConsoleKey.LeftArrow, new Close(logic)),
            };
        }

        public void Read(ConsoleKey input)
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