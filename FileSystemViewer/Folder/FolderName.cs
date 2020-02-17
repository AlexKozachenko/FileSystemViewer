using System;
using System.IO;

namespace FileSystemViewer
{
    internal class FolderName : DriveName
    {
        protected int lastColumnIndex = Console.WindowWidth - 1;
        public FolderName(string fullPath, string parentPrefix) : base(fullPath)
        {
            FormatPrefix(parentPrefix);
        }
        public override ConsoleColor Color => ConsoleColor.Yellow;
        public override int Depth => GetDepth();
        protected virtual void CutName()
        {
            int cut = lastColumnIndex - Offset - "...".Length;
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
        protected int GetDepth()
        {
            int slashesInPath = DriveDepth;
            foreach (char character in FullPath)
            {
                if (character == '\\')
                {
                    slashesInPath++;
                }
            }
            return slashesInPath;
        }
        protected override void SetName()
        {
            Name = new DirectoryInfo(FullPath).Name;
            CutName();
        }
    }
}