using System;
using System.IO;

namespace FileSystemViewer
{
    internal class FolderName : DriveName
    {
        public FolderName(string fullPath) : base(fullPath)
        {
            CutName();
        }
        public override ConsoleColor Color { get; } = ConsoleColor.Yellow;
        protected virtual void CutName()
        {
            int cut = Console.WindowWidth - Offset - "...".Length - 1;
            if (Name.Length > cut)
            {
                Name = Name.Remove(cut) + "...";
            }
        }
        public void FormatPrePrefix(string parentPrefix)
        {
            for (int i = 0; PrePrefix.Length < Offset - Step; i = i + Step)
            {
                string character = " ";
                //если символ в префиксе родителя ветвление или вертикальная черта, 
                //в наследнике на том же месте ставим верт. черту, 
                //если пробел или угол - пробел. верт. черта не может быть под углом или пробелом
                if (parentPrefix[i] == (char)0x251C
                    || parentPrefix[i] == (char)0x2502)
                {
                    character = ((char)0x2502).ToString();
                }
                //сборка предварительного префикса
                PrePrefix = PrePrefix + character + " ";
            }
        }
        protected override void GetDeep()
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
        protected override void GetName()
        {
            Name = new DirectoryInfo(FullPath).Name;
        }
    }
}