using System;
using System.Collections.Generic;

namespace FileSystemViewer
{
    internal class ProgramLogic
    {
        private Stack<DefaultComponent> foldersOverTop = new Stack<DefaultComponent>();
        private List<DefaultComponent> foldersUnderTop = new List<DefaultComponent>()
            {
                new Root()
            };
        private int position = 0;
        private DefaultComponent Current => foldersUnderTop[Position];
        private int MaxFolderIndex => foldersUnderTop.Count - 1;
        private int MaxRowIndex => Console.WindowHeight - 1;
        public int Position
        {
            get
            {
                return position;
            }
            set
            {
                void PopInTop()
                {
                    if (foldersOverTop.Count > 0)
                    {
                        foldersUnderTop.Insert(0, foldersOverTop.Pop());
                    }
                }
                if (value < 0)
                {
                    PopInTop();
                    value = 0;
                }
                if (value > MaxFolderIndex)
                {
                    //протяжка вниз до последней строки, если последняя папка закрылась посреди окна
                    if (MaxFolderIndex < MaxRowIndex)
                    {
                        PopInTop();
                    }
                    value = MaxFolderIndex;
                }
                if (value > MaxRowIndex)
                {
                    //PushTop:
                    foldersOverTop.Push(foldersUnderTop[0]);
                    foldersUnderTop.RemoveAt(0);
                    value = MaxRowIndex;
                }
                position = value;
            }
        }
        public void Close()
        {
            Current.CloseContainer(foldersUnderTop, Position);
        }
        public void Open()
        {
            Current.OpenContainer(foldersUnderTop, Position);
            if (Position == MaxRowIndex && Current.IsOpen)
            {
                ++Position;
            }
        }
        public void WriteScreen()
        {
            void Write(ConsoleColor fontColor, string line)
            {
                Console.ForegroundColor = fontColor;
                Console.Write(line);
            }
            const ConsoleColor ServiceColor = ConsoleColor.DarkGray;
            Console.ResetColor();
            Console.Clear();
            int rowIndex = 0;
            foreach (DefaultComponent folder in foldersUnderTop)
            {
                Console.SetCursorPosition(0, rowIndex);
                Write(ServiceColor, folder.Prefix);
                Write(folder.Color, folder.Name);
                if (rowIndex == MaxRowIndex)
                {
                    break;
                }
                ++rowIndex;
            }
            Console.SetCursorPosition(Current.Offset, Position);
            Console.BackgroundColor = ServiceColor;
            Write(Current.Color, Current.Name);
        }
    }
}