using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemViewer
{
    internal class Program
    {
        private const ConsoleColor ServiceColor = ConsoleColor.DarkGray;
        private int cursorPosition = 0;
        private List<DriveName> childrenTemporary = new List<DriveName>();
        private List<DefaultFolder> foldersUnderTop = new List<DefaultFolder>()
        {
            new Root()
        };
        private Stack<DefaultFolder> foldersOverTop = new Stack<DefaultFolder>();
        public Program()
        {
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;
            Open();
        }
        private DefaultFolder Current => foldersUnderTop[CursorPosition];
        private int CursorPosition
        {
            get
            {
                return cursorPosition;
            }
            set
            {
                if (value < 0)
                {
                    PopInTop();
                    value = 0;
                }
                if (value > LastFolderIndex)
                {
                    //протяжка вниз до последней строки, если последняя папка закрылась посреди окна
                    if (LastFolderIndex < LastRowIndex)
                    {
                        PopInTop();
                    }
                    value = LastFolderIndex;
                }
                if (value > LastRowIndex)
                {
                    PushTop();
                    value = LastRowIndex;
                }
                cursorPosition = value;
            }
        }
        private int LastFolderIndex => foldersUnderTop.Count - 1;
        private int LastRowIndex => Console.WindowHeight - 1;

        public void Close()
        {
            if (Current.IsOpen)
            {
                foldersUnderTop.RemoveAll(folder => folder.FullPath.Contains(Current.FullPath)
                    && folder.Deep > Current.Deep);
                Current.IsOpen = false;
            }
        }
        private void GetChildren()
        {
            if (Current.Deep == 0)
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    childrenTemporary.Add(new DriveName(drive.Name));
                }
            }
            else
            {
                if (Directory.Exists(Current.FullPath))
                {
                    foreach (string directory in Directory.GetDirectories(Current.FullPath))
                    {
                        childrenTemporary.Add(new FolderName(directory, Current.Prefix));
                    }
                    foreach (string file in Directory.GetFiles(Current.FullPath))
                    {
                        childrenTemporary.Add(new FileName(file, Current.Prefix));
                    }
                }
            }
            int lastContainerIndex = childrenTemporary.FindLastIndex(folder => !folder.IsLeaf);
            if (lastContainerIndex > -1)
            {
                childrenTemporary[lastContainerIndex].GetLastContainerPrefix();
            }
        }
        public void MoveDown()
        {
            ++CursorPosition;
        }
        public void MoveUp()
        {
            --CursorPosition;
        }
        public void Open()
        {
            int nextPosition = CursorPosition + 1;
            //первый раз проверяется любая папка (не пустая по умолчанию), т.к. неизвестно, пустая она или нет,
            //если пустая, при следующем раскрытии процесс получения вложенных папок отменяется
            if (!Current.IsOpen && !Current.IsEmpty)
            {
                GetChildren();
                if (childrenTemporary.Count > 0)
                {
                    foldersUnderTop.InsertRange(nextPosition, childrenTemporary);
                    childrenTemporary.Clear();
                    Current.IsOpen = true;
                }
                else
                {
                    Current.IsEmpty = true;
                }
            }
            //при открытии папки на последней строке список скроллится на 1 вниз
            if (CursorPosition == LastRowIndex && Current.IsOpen)
            {
                ++CursorPosition;
            }
        }
        private void PopInTop()
        {
            if (foldersOverTop.Count > 0)
            {
                foldersUnderTop.Insert(0, foldersOverTop.Pop());
            }
        }
        private void PushTop()
        {
            foldersOverTop.Push(foldersUnderTop[0]);
            foldersUnderTop.RemoveAt(0);
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
            int row = 0;
            foreach (DefaultFolder folder in foldersUnderTop)
            {
                Console.SetCursorPosition(0, row);
                Write(ServiceColor, folder.Prefix);
                Write(folder.Color, folder.Name);
                if (row == LastRowIndex)
                {
                    break;
                }
                ++row;
            }
            Console.SetCursorPosition(Current.Offset, CursorPosition);
            Console.BackgroundColor = ServiceColor;
            Write(Current.Color, Current.Name);
        }
    }
}