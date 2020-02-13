using System;
using System.IO;

namespace FileSystemViewer
{
    internal class FolderName : DriveName
    {
        public FolderName(string fullPath, string parentPrefix) : base(fullPath)
        {
            FormatPrePrefix(parentPrefix);
            CutName();
        }
        public override ConsoleColor Color { get; } = ConsoleColor.Yellow;
        public override int Deep
        {
            get
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
                return slashesInPath;
            }
        }
        public override string Name => new DirectoryInfo(FullPath).Name;
        protected virtual void CutName()
        {
            int cut = Console.WindowWidth - Offset - "...".Length - 1;
            if (Name.Length > cut)
            {
                Name = Name.Remove(cut) + "...";
            }
        }
        protected void FormatPrePrefix(string parentPrefix)
        {
            for (int i = 0; PrePrefix.Length < Offset - Step; i = i + Step)
            {
                string character = " ";
                //Предварительный префикс this по длине соотв. префиксу parent.
                //Если символ в префиксе имени родителя ветвление или вертикальная черта, 
                //в this на том же месте ставим верт. черту, 
                //если пробел или угол - пробел. Верт. черта не может быть под углом или пробелом
                if (parentPrefix[i] == (char)0x251C
                    || parentPrefix[i] == (char)0x2502)
                {
                    character = ((char)0x2502).ToString();
                }
                //сборка предварительного префикса
                //начальный пре-префикс: нулевая строка у жестких дисков
                PrePrefix = PrePrefix + character + " ";
            }
        }
    }
}