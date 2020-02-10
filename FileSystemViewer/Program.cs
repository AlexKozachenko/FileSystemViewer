using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;

namespace FileSystemViewer
{
     class Program
    {
        private int cursorPosition = 0;
        private Collection<DefaultFolder> childrenTemporary = new Collection<DefaultFolder>();
        private List<DefaultFolder> foldersUnderTop = new List<DefaultFolder>()
            {
                new Root("")
            };
        private Stack<DefaultFolder> foldersOverTop = new Stack<DefaultFolder>();
        private readonly int lastRowIndex = Console.WindowHeight - 1;
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
                    if (LastFolderIndex < lastRowIndex)
                    {
                        PopInTop();
                    }
                    value = LastFolderIndex;
                }
                if (value > lastRowIndex)
                {
                    PushTop();
                    value = lastRowIndex;
                }
                cursorPosition = value;
            }
        }
        private int LastFolderIndex => foldersUnderTop.Count - 1;
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
            if (Current.FullPath == "")
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    childrenTemporary.Add(new DriveName(drive.Name));
                }
                childrenTemporary[childrenTemporary.Count - 1].IsLastChildDir = true;
            }
            else
            {
                if (Directory.Exists(Current.FullPath))
                {
                    foreach (string directory in Directory.GetDirectories(Current.FullPath))
                    {
                        childrenTemporary.Add(new FolderName(directory));
                    }
                    if (childrenTemporary.Count > 0)
                    {
                        childrenTemporary[childrenTemporary.Count - 1].IsLastChildDir = true;
                    }
                    foreach (string file in Directory.GetFiles(Current.FullPath))
                    {
                        childrenTemporary.Add(new FileName(file));
                    }
                    foreach (FolderName child in childrenTemporary)
                    {
                        child.FormatPrePrefix(Current.Prefix);
                    }
                }
            }
            foreach (DriveName child in childrenTemporary)
            {
                child.FormatPrefix();
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
            if (CursorPosition == lastRowIndex && Current.IsOpen)
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
        public void WriteScreen()
        {
            Console.ResetColor();
            Console.Clear();
            for (int i = 0; i <= LastFolderIndex && i <= lastRowIndex; ++i)
            {
                Console.SetCursorPosition(0, i);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(foldersUnderTop[i].Prefix);
                Console.ForegroundColor = foldersUnderTop[i].Color;
                Console.Write(foldersUnderTop[i].Name);
            }
            Console.SetCursorPosition(Current.Offset, CursorPosition);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = Current.Color;
            Console.Write(Current.Name);
        }
    }
}