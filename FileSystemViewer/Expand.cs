using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemViewer
{
    internal class Expand : Key
    {
        public Expand()
        {
            Input = ConsoleKey.RightArrow;
        }

        public override void Action(FileViewer viewer)
        {
            int position = viewer.PositionInFolders;
            string current = viewer.Folders[position];
            if ((Directory.Exists(current)) && (!viewer.IsOpen(current)))
            {
                Console.Clear();
                viewer.OpenFolders.Add(current);
                List<string> expanded = viewer.GetFolders(current);
                List<string> temporary = viewer.InsertList(expanded, viewer.Folders, position);
                viewer.Folders.Clear();
                viewer.Folders.AddRange(temporary);
                FileViewer.Process(viewer);
            }
        }
    }
}