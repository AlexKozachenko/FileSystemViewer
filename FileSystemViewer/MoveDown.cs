using System;

namespace FileSystemViewer
{
    internal class MoveDown : Key
    {
        public MoveDown()
        {
            Input = ConsoleKey.DownArrow;
        }

        public override void Action(FileViewer viewer)
        {
            viewer.PositionInFolders++;
            if (viewer.PositionInFolders > viewer.Folders.Count - 1)
            {
                viewer.PositionInFolders = viewer.Folders.Count - 1;
            }
        }
    }
}