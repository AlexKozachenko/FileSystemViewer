using System;

namespace FileSystemViewer
{
    internal class Draw
    {
        private const ConsoleColor ServiceColor = ConsoleColor.DarkGray;
        private ScrollLogic scroll;
        public Draw(ScrollLogic scroll_)
        {
            scroll = scroll_;
        }
        public void Write(ConsoleColor fontColor, string line)
        {
            Console.ForegroundColor = fontColor;
            Console.Write(line);
        }
        public void WriteScreen()
        {
            Console.ResetColor();
            Console.Clear();
            int rowIndex = 0;
            foreach (DefaultFolder folder in scroll.FoldersUnderTop)
            {
                Console.SetCursorPosition(0, rowIndex);
                Write(ServiceColor, folder.Prefix);
                Write(folder.Color, folder.Name);
                if (rowIndex == scroll.MaxRowIndex)
                {
                    break;
                }
                ++rowIndex;
            }
            Console.SetCursorPosition(scroll.Current.Offset, scroll.Position);
            Console.BackgroundColor = ServiceColor;
            Write(scroll.Current.Color, scroll.Current.Name);
        }
    }
}
