using System;
using System.IO;

namespace FileSystemViewer
{
    internal class FolderName : DriveName
    {
        public FolderName(string fullPath, string parentPrefix) : base(fullPath)
        {
            FormatPrefix(parentPrefix);
            CutName();
        }
        public override ConsoleColor Color => ConsoleColor.Yellow;

        protected virtual void CutName()
        {
            int cut = Console.WindowWidth - Offset - "...".Length - 1;
            if (Name.Length > cut)
            {
                Name = Name.Remove(cut) + "...";
            }
        }
        protected void FormatPrefix(string parentPrefix)
        {
            string prePrefix  = "";
            for (int i = 0; prePrefix.Length < Offset - Step; i = i + Step)
            {
                string character = " ";
                //Предварительный префикс this по длине соотв. префиксу parent или смещению parent, или собств. смещению - 2.
                //Если символ в префиксе имени родителя ветвление или вертикальная черта, 
                //в this на том же месте ставим верт. черту, 
                //если пробел или угол - пробел. Верт. черта не может быть под углом или пробелом
                if (parentPrefix[i] == (char)0x251C
                    || parentPrefix[i] == (char)0x2502)
                {
                    character = ((char)0x2502).ToString();
                }
                //сборка предварительного префикса
                prePrefix = prePrefix + character + " ";
            }
            Prefix = prePrefix + Prefix;
        }
        protected override void GetDeep()
        {
            int slashesInPath = DriveDeep;
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