using System;

namespace FileSystemViewer
{
    internal class MoveUp : Key
    {
        public MoveUp()
        {
            Input = ConsoleKey.UpArrow;
        }

        public override void Action(FileViewer viewer)
        {
            viewer.PositionInFolders--;
            if (viewer.PositionInFolders < 0)
            {
                viewer.PositionInFolders = 0;
            }
        }
    }
}