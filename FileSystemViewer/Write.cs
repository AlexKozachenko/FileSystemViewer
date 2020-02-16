using System;

namespace FileSystemViewer
{
    internal class Draw
    {
        private const ConsoleColor ServiceColor = ConsoleColor.DarkGray;
        private SelectionAndScrolling selection;
        public Draw(SelectionAndScrolling selection_)
        {
            selection = selection_;
        }
        public DefaultFolder Current => selection.FoldersUnderTop[selection.Position];
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
            foreach (DefaultFolder folder in selection.FoldersUnderTop)
            {
                Console.SetCursorPosition(0, rowIndex);
                Write(ServiceColor, folder.Prefix);
                Write(folder.Color, folder.Name);
                if (rowIndex == selection.MaxRowIndex)
                {
                    break;
                }
                ++rowIndex;
            }
            Console.SetCursorPosition(Current.Offset, selection.Position);
            Console.BackgroundColor = ServiceColor;
            Write(Current.Color, Current.Name);
        }
    }
}