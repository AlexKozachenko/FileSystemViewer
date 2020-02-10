using System;

namespace FileSystemViewer
{
    internal class KeyAssign
    {
        private AssignedKey[] keys;
        public Program Viewer { get; set; }
        public KeyAssign(Program viewer)
        {
            Viewer = viewer;
            keys = new AssignedKey[]
            {
                new AssignedKey(ConsoleKey.UpArrow, new MoveUp(Viewer)),
                new AssignedKey(ConsoleKey.DownArrow, new MoveDown(Viewer)),
                new AssignedKey(ConsoleKey.RightArrow, new Open(Viewer)),
                new AssignedKey(ConsoleKey.LeftArrow, new Close(Viewer)),
                new AssignedKey(ConsoleKey.W, new MoveUp(Viewer)),
                new AssignedKey(ConsoleKey.S, new MoveDown(Viewer)),
                new AssignedKey(ConsoleKey.D, new Open(Viewer)),
                new AssignedKey(ConsoleKey.A, new Close(Viewer))
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