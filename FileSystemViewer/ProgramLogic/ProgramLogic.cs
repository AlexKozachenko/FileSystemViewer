using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

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
            Collection<DriveName> childrenTemporary = new Collection<DriveName>();
            int lastContainerIndex = -1;
            //первый раз проверяется любая папка (не пустая по умолчанию), т.к. неизвестно, пустая она или нет,
            //если пустая, при следующем раскрытии процесс получения вложенных папок отменяется
            if (!Current.IsOpen && !Current.IsEmpty)
            {
                if (Current.Depth == 0)
                {
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        childrenTemporary.Add(new DriveName(drive.Name));
                        ++lastContainerIndex;
                    }
                }
                else
                {
                    if (Directory.Exists(Current.FullPath))
                    {
                        foreach (string directory in Directory.GetDirectories(Current.FullPath))
                        {
                            childrenTemporary.Add(new FolderName(directory, Current.Prefix));
                            ++lastContainerIndex;
                        }
                        foreach (string file in Directory.GetFiles(Current.FullPath))
                        {
                            childrenTemporary.Add(new FileName(file, Current.Prefix));
                        }
                    }
                }
                if (lastContainerIndex >= 0)
                {
                    childrenTemporary[lastContainerIndex].SetLastContainerPrefix();
                }
                if (childrenTemporary.Count > 0)
                {
                    int nextPosition = Position + 1;
                    foldersUnderTop.InsertRange(nextPosition, childrenTemporary);
                    Current.IsOpen = true;
                }
                else
                {
                    Current.IsEmpty = true;
                }
            }
            //при открытии папки на последней строке список скроллится на 1 вниз
            if (Position == MaxRowIndex && Current.IsOpen)
            {
                ++Position;
            }
        }
        private void PopInTop()
        {
            if (foldersOverTop.Count > 0)
            {
                foldersUnderTop.Insert(0, foldersOverTop.Pop());
            }
        }
        private void Write(ConsoleColor fontColor, string line)
        {
            Console.ForegroundColor = fontColor;
            Console.Write(line);
        }
        public void WriteScreen()
        {
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