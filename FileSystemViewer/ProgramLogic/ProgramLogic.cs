using System;
using System.Collections.Generic;

namespace FileSystemViewer
{
    internal class ProgramLogic
    {
        private Stack<DefaultFolder> foldersOverTop = new Stack<DefaultFolder>();
        private List<DefaultFolder> foldersUnderTop = new List<DefaultFolder>()
            {
                new Root()
            };
        private int position = 0;
        private DefaultFolder Current => foldersUnderTop[Position];
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
            if (Current.IsOpen)
            {
                foldersUnderTop.RemoveAll(folder => folder.FullPath.Contains(Current.FullPath)
                    && folder.Depth > Current.Depth);
                Current.IsOpen = false;
            }
        }
        public void Open()
        {
            void Insert()
            {
                int nextPosition = Position + 1;
                foldersUnderTop.InsertRange(nextPosition, Current.Children);
                Current.IsOpen = true;
            }
            //первый раз проверяется любая папка (не пустая по умолчанию, т.к. неизвестно, пустая она или нет),
            //если пустая, при следующем раскрытии процесс получения вложенных папок отменяется
            if (!Current.IsOpen && !Current.IsEmpty)
            {
                if (!Current.WasOpened)
                {
                    Current.GetChildren();
                    if (Current.Children.Count > 0)
                    {
                        Insert();
                        Current.WasOpened = true;
                    }
                    else
                    {
                        Current.IsEmpty = true;
                    }

                }
                else 
                {
                    Insert();
                }
            }
            //при открытии папки на последней строке список скроллится на 1 вниз
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
            foreach (DefaultFolder folder in foldersUnderTop)
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