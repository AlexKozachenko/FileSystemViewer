using System;
using System.IO;

namespace FileSystemViewer
{
    internal class Collapse : Key
    {
        public Collapse()
        {
            Input = ConsoleKey.LeftArrow;
        }

        public override void Action(FileViewer viewer)
        {
            const int nextIndex = 1;
            const int secondLast = 2;
            int position = viewer.PositionInFolders;
            string current = viewer.Folders[position];
            if ((Directory.Exists(current))
                && (viewer.IsOpen(current))
                && (position < viewer.Folders.Count - secondLast))
            {
                viewer.OpenFolders.Remove(current);
                Console.Clear();
                int index = 1;
                int slashes = viewer.CountSlashes(current);
                int slashesInNext = viewer.CountSlashes(viewer.Folders[position + nextIndex]);
                if (slashes < slashesInNext)
                {
                    while (slashes < viewer.CountSlashes(viewer.Folders[position + index]))
                    {
                        index++;
                    }
                }
                else
                {
                    if (current.Length == viewer.DriveNameLength)
                    {
                        while (viewer.Folders[position + index].Length != viewer.DriveNameLength)
                        {
                            index++;
                        }
                    }
                }
                viewer.Folders.RemoveRange(position + nextIndex, index - nextIndex);
                FileViewer.Process(viewer);
            }
        }
    }
}