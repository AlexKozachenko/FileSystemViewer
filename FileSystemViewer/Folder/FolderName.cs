using System;
using System.IO;

namespace FileSystemViewer
{
    internal class FolderName : DriveName
    {
        protected readonly int lastColumnIndex = Console.WindowWidth - 1;
        protected readonly string threeDot = "...";
        public FolderName(string fullPath, string parentPrefix) : base(fullPath)
        {
            FormatPrefix(parentPrefix);
        }
        public override ConsoleColor Color => ConsoleColor.Yellow;
        public override int Depth => GetDepth();
        protected virtual void CutName()
        {
            int cut = lastColumnIndex - Offset - threeDot.Length;
            if (Name.Length > cut)
            {
                Name = Name.Remove(cut) + threeDot;
            }
        }
        protected void FormatPrefix(string parentPrefix)
        {
            string prePrefix  = "";
            for (int i = 0; prePrefix.Length < Offset - StepOffset; i += StepOffset)
            {
                string character = " ";
                //Предварительный префикс this по длине соотв. префиксу или смещению parent.
                //Впервые появляется у первого поколения папок.
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
                    ++slashesInPath;
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