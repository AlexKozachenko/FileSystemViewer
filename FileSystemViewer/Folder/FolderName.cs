using System;
using System.IO;

namespace FileSystemViewer
{
    internal class FolderName : DefaultFolder
    {
        public FolderName(string fullPath) : base(fullPath)
        {
            GetColor();
            GetDeep();
            GetOffset();
            Name = new DirectoryInfo(fullPath).Name;
            FormatName();
        }

        protected virtual void GetColor()
        {
            Color = ConsoleColor.Yellow;
        }
        protected void GetDeep()
        {
            const int drivesDeep = 1;
            int slashesInPath = drivesDeep;
            foreach (char character in FullPath)
            {
                if (character == '\\')
                {
                    slashesInPath++;
                }
            }
            Deep = slashesInPath;
        }
        protected void GetOffset()
        {
            Offset = Deep * Step;
        }
        protected virtual void FormatName()
        {
            int cut = Console.WindowWidth - Offset - "...".Length - 1;
            if (Name.Length > cut)
            {
                Name = Name.Remove(cut) + "...";
            }
        }
    }
}