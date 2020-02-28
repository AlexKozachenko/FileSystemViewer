using static FileSystemViewer.Components.Literals;
using System;
using System.IO;

namespace FileSystemViewer.Components
{
    internal class FolderComponent : DriveComponent
    {
        public FolderComponent(string fullPath, string parentPrefix) : base(fullPath)
        {
            FormatPrefix(parentPrefix);
        }

        public override ConsoleColor Color => ConsoleColor.Yellow;

        protected override int Depth => GetDepth();

        protected int LastColumnIndex => Console.WindowWidth - 1;

        protected virtual void CutName()
        {
            int cut = LastColumnIndex - Offset - ThreeDot.Length;
            if (Name.Length > cut)
            {
                Name = Name.Remove(cut) + ThreeDot;
            }
        }

        protected void FormatPrefix(string parentPrefix)
        {
            string character;
            string prePrefix = "";
            for (int i = 0; prePrefix.Length < Offset - StepOffset; i += StepOffset)
            {
                //Предварительный префикс this по длине соотв. префиксу или смещению parent.
                //Впервые появляется у первого поколения папок.
                //Если символ в префиксе имени родителя ветвление или вертикальная черта, 
                //в this на том же месте ставим верт. черту, 
                //если пробел или угол - пробел. Верт. черта не может быть под углом или пробелом
                if (parentPrefix[i] == T_ConnectingPart
                        || parentPrefix[i] == I_ConnectingPart)
                {
                    character = I_ConnectingPart.ToString();
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
                if (character == Slash)
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