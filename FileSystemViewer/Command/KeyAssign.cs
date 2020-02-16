using System;

namespace FileSystemViewer
{
    internal class KeyAssign
    {
        private readonly AssignedKey[] keys;
        public SelectionAndScrolling Selection { get; set; }
        public FolderManagement ManageFolder { get; set; }
        public KeyAssign(SelectionAndScrolling cursor, FolderManagement manageFolder)
        {
            Selection = cursor;
            ManageFolder = manageFolder;
            keys = new AssignedKey[]
            {
                new AssignedKey(ConsoleKey.UpArrow, new MoveUp(Selection)),
                new AssignedKey(ConsoleKey.DownArrow, new MoveDown(Selection)),
                new AssignedKey(ConsoleKey.RightArrow, new Open(ManageFolder)),
                new AssignedKey(ConsoleKey.LeftArrow, new Close(ManageFolder)),
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