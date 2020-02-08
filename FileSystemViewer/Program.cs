using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemViewer
{
    internal class Program
    {
        private int cursorPosition = 0;
        private List<DefaultFolder> children = new List<DefaultFolder>();
        private List<DefaultFolder> foldersUnderTop = new List<DefaultFolder>()
            {
                new Root()
            };
        private Stack<DefaultFolder> foldersOverTop = new Stack<DefaultFolder>();
        private int lastRowIndex = Console.WindowHeight - 1;
        public Program()
        {
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;
            Open();
        }
        private DefaultFolder Current => foldersUnderTop[cursorPosition];
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
            children.Clear();
            if (Current.GetType().ToString() == "FileSystemViewer.Root")
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    children.Add(new DriveName(drive.Name));
                }
                children[children.Count - 1].IsLastChildDir = true;
            }
            else
            {
                if (Directory.Exists(Current.FullPath))
                {
                    foreach (string directory in Directory.GetDirectories(Current.FullPath))
                    {
                        children.Add(new FolderName(directory));
                    }
                    if (children.Count > 0)
                    {
                        children[children.Count - 1].IsLastChildDir = true;
                    }
                    foreach (string file in Directory.GetFiles(Current.FullPath))
                    {
                        children.Add(new FileName(file));
                    }
                }
            }
        }
        public void MoveDown()
        {
            ++cursorPosition;
            if (cursorPosition > LastFolderIndex
                && foldersUnderTop.Count < Console.WindowHeight
                && foldersOverTop.Count > 0)
            {
                foldersUnderTop.Insert(0, foldersOverTop.Pop());
            }
            if (cursorPosition > LastFolderIndex)
            {
                cursorPosition = LastFolderIndex;
            }
            if (cursorPosition > lastRowIndex)
            {
                cursorPosition = lastRowIndex;
                foldersOverTop.Push(foldersUnderTop[0]);
                foldersUnderTop.RemoveAt(0);
            }
        }
        public void MoveUp()
        {
            --cursorPosition;
            if (cursorPosition < 0)
            {
                cursorPosition = 0;
            }
            if (foldersOverTop.Count > 0 && cursorPosition == 0)
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
                if (children.Count > 0)
                {
                    Current.IsOpen = true;
                    children.ForEach(child => child.FormatPrefix(Current.Prefix));
                    foldersUnderTop.InsertRange(cursorPosition + next, children);
                }
                else
                {
                    Current.IsEmpty = true;
                }
            }
            if (cursorPosition == lastRowIndex
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
            Console.SetCursorPosition(Current.Offset, cursorPosition);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = Current.Color;
            Console.Write(Current.Name);
        }
    }
}