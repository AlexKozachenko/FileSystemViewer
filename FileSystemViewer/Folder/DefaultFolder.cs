using System;

namespace FileSystemViewer
{
    internal abstract class DefaultFolder
    {
        private const int step = 2;
        public ConsoleColor Color { get; set; }
        public int Deep { get; set; }
        public string FullPath { get; set; }
        public bool IsEmpty { get; set; }
        public bool IsLastChildDir { get; set; }
        public bool IsOpen { get; set; }
        public string Name { get; set; }
        public int Offset { get; set; }
        public string Prefix { get; set; }
        public int Step => step;
        public DefaultFolder(string fullPath)
        {
            FullPath = fullPath;
            //по умолчанию папка cчитается не пустой, пока не пройдет проверку при раскрытии в методе Open
            IsEmpty = false;
            IsOpen = false;
            IsLastChildDir = false;
        }
        public void FormatPrefix(string parentPrefix)
        {
            string prePrefix = "";
            int i = 0;
            while (prePrefix.Length < Offset - Step)
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
                prePrefix = prePrefix + character + " ";
                i = i + Step;
            }
            //если this - файл, окончание - 2 пробела
            if (Color == ConsoleColor.Cyan)
            {
                Prefix = prePrefix + "  ";
            }
            else
            {
                //если this - папка последняя в наследниках - угол+черта
                if (IsLastChildDir)
                {
                    Prefix = prePrefix + (char)0x2514 + (char)0x2500;
                }
                //если нет - ветвление+черта
                else
                {
                    Prefix = prePrefix + (char)0x251C + (char)0x2500;
                }
            }
        }
    }
}