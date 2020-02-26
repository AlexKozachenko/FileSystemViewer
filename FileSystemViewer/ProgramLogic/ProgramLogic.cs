using FileSystemViewer.Components;
using System;
using System.Collections.Generic;

namespace FileSystemViewer
{
    internal class ProgramLogic
    {
        private List<DefaultComponent> apparentUnderTop = new List<DefaultComponent>()
            {
                new RootComponent()
            };
        private Stack<DefaultComponent> hiddenOverTop = new Stack<DefaultComponent>();
        private int selectionPosition = 0;

        private DefaultComponent Current => apparentUnderTop[SelectionPosition];

        private int MaxFolderIndex => apparentUnderTop.Count - 1;

        private int MaxRowIndex => Console.WindowHeight - 1;

        public int SelectionPosition
        {
            get
            {
                return selectionPosition;
            }
            set
            {
                void PopInTop()
                {
                    if (hiddenOverTop.Count > 0)
                    {
                        apparentUnderTop.Insert(0, hiddenOverTop.Pop());
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
                    hiddenOverTop.Push(apparentUnderTop[0]);
                    apparentUnderTop.RemoveAt(0);
                    value = MaxRowIndex;
                }
                selectionPosition = value;
            }
        }

        public void Close()
        {
            Current.CloseComponent(apparentUnderTop, SelectionPosition);
        }

        public void Open()
        {
            Current.OpenComponent(apparentUnderTop, SelectionPosition);
            if (SelectionPosition == MaxRowIndex && Current.IsOpen)
            {
                ++SelectionPosition;
            }
        }

        public void ShowFileSystem()
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
            foreach (DefaultComponent folder in apparentUnderTop)
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
            Console.SetCursorPosition(Current.Offset, SelectionPosition);
            Console.BackgroundColor = ServiceColor;
            Write(Current.Color, Current.Name);
        }
    }
}