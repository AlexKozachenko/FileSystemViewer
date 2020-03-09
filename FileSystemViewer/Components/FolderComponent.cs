using static FileSystemViewer.Components.Literals;
using System;
using System.IO;

namespace FileSystemViewer.Components
{
    internal class FolderComponent : DriveComponent
    {
        public FolderComponent(string fullPath, string parentPrefix) : base(fullPath) => FormatPrefix(parentPrefix);

        public override ConsoleColor Color => ConsoleColor.Yellow;

        protected override int Depth => GetDepth();

        protected int LastColumnIndex => Console.WindowWidth - 1;

        protected virtual void CutName()
        {
            int cut = LastColumnIndex - Offset - Ellipsis.Length;
            if (Name.Length > cut)
            {
                Name = Name.Remove(cut) + Ellipsis;
            }
        }

        protected void FormatPrefix(string parentPrefix)
        {
            string character;
            string prePrefix = EmptyString;
            for (int i = 0; prePrefix.Length < Offset - StepOffset; i += StepOffset)
            {
                //Предварительный префикс this по длине соотв. префиксу или смещению parent.
                //Впервые появляется у первого поколения папок.
                //Если символ в префиксе имени родителя ветвление или вертикальная черта, 
                //в this на том же месте ставим верт. черту, 
                //если пробел или угол - пробел. Верт. черта не может быть под углом или пробелом
                if (parentPrefix[i] == ConnectingPart_T || parentPrefix[i] == ConnectingPart_I)
                {
                    character = ConnectingPart_I.ToString();
                }
                else
                {
                    character = Space;
                }
                //сборка предварительного префикса
                prePrefix += character + Space;
            }
            Prefix = prePrefix + Prefix;
        }

        protected int GetDepth()
        {
            int depth = DriveDepth;
            foreach (char character in FullPath)
            {
                if (character == BackSlash)
                {
                    ++depth;
                }
            }
            return depth;
        }

        protected override void SetName()
        {
            Name = new DirectoryInfo(FullPath).Name;
            CutName();
        }
    }
}