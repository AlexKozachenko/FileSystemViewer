using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemViewer
{
    internal class Program
    {
        private int cursorPosition = 0;
        private List<DefaultFolder> childrenTemporary = new List<DefaultFolder>();
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
                    value = 0;
                }
                if (value > LastFolderIndex)
                {
                    value = LastFolderIndex;
                }
                if (value > lastRowIndex)
                {
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
                }
            }
        }
        public void MoveDown()
        {
            ++CursorPosition;
            //протяжка вниз до последней строки, если последняя папка закрылась посреди окна
            if (CursorPosition == LastFolderIndex
                && foldersUnderTop.Count < Console.WindowHeight
                && foldersOverTop.Count > 0)
            {
                foldersUnderTop.Insert(0, foldersOverTop.Pop());
                ++CursorPosition;
            }
            if (CursorPosition == lastRowIndex 
                && lastRowIndex < LastFolderIndex)
            {
                foldersOverTop.Push(foldersUnderTop[0]);
                foldersUnderTop.RemoveAt(0);
            }
        }
        public void MoveUp()
        {
            --CursorPosition;
            if (foldersOverTop.Count > 0 && CursorPosition == 0)
            {
                foldersUnderTop.Insert(0, foldersOverTop.Pop());
            }
        }
        public void Open()
        {
            const int next = 1;
            //первый раз проверяется любая папка (не пустая по умолчанию), т.к. неизвестно, пустая она или нет,
            //если пустая, при следующем раскрытии процесс получения вложенных папок отменяется
            if (!Current.IsOpen && !Current.IsEmpty)
            {
                GetChildren();
                if (childrenTemporary.Count > 0)
                {
                    Current.IsOpen = true;
                    childrenTemporary.ForEach(child => child.FormatPrefix(Current.Prefix));
                    foldersUnderTop.InsertRange(CursorPosition + next, childrenTemporary);
                    childrenTemporary.Clear();
                }
                else
                {
                    Current.IsEmpty = true;
                }
            }
            if (CursorPosition == lastRowIndex
                && Current.IsOpen)
            {
                MoveDown();
            }
        }
        public void WriteScreen()
        {
            Console.ResetColor();
            Console.Clear();
            for (int i = 0; i < foldersUnderTop.Count && i < Console.WindowHeight; ++i)
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