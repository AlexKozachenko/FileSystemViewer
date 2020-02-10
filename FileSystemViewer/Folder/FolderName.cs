using System;
using System.IO;

namespace FileSystemViewer
{
    internal class FolderName : DriveName
    {
        public FolderName(string fullPath) : base(fullPath)
        {
            GetColor();
            GetDeep();
            GetOffset();
            Name = new DirectoryInfo(fullPath).Name;
            FormatName();
        }
        protected string PrePrefix { get; set; } = "";
        protected virtual void FormatName()
        {
            int cut = Console.WindowWidth - Offset - "...".Length - 1;
            if (Name.Length > cut)
            {
                Name = Name.Remove(cut) + "...";
            }
        }
        public void FormatPrePrefix(string parentPrefix)
        {
            int i = 0;
            while (PrePrefix.Length < Offset - Step)
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
                i = i + Step;
            }
        }
        public override void FormatPrefix()
        {
            if (IsLastChildDir)
            {
                Prefix = PrePrefix + (char)0x2514 + (char)0x2500;
            }
            else
            {
                Prefix = PrePrefix + (char)0x251C + (char)0x2500;
            }
        }
        protected override void GetColor()
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
    }
}