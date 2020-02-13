using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;

namespace FileSystemViewer
{
    internal class Program
    {
        private int cursorPosition = 0;
        private Collection<DriveName> childrenTemporary = new Collection<DriveName>();
        private List<DefaultFolder> foldersUnderTop = new List<DefaultFolder>()
        {
            new Root()
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
        private void FinalizeContainers()
        {
            if (childrenTemporary.Count > 0)
            {
                childrenTemporary[childrenTemporary.Count - 1].IsLastContainer = true;
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
                FinalizeContainers();
            }
            else
            {
                if (Directory.Exists(Current.FullPath))
                {
                    foreach (string directory in Directory.GetDirectories(Current.FullPath))
                    {
                        childrenTemporary.Add(new FolderName(directory, Current.Prefix));
                    }
                    FinalizeContainers();
                    foreach (string file in Directory.GetFiles(Current.FullPath))
                    {
                        childrenTemporary.Add(new FileName(file, Current.Prefix));
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

        public void Write(ConsoleColor color, string line)
        {
            Console.ForegroundColor = color;
            Console.Write(line);
        }

        public void WriteScreen()
        {
            Console.ResetColor();
            Console.Clear();
            const ConsoleColor ServiceColor = ConsoleColor.DarkGray;
            for (int lineIndex = 0; lineIndex <= LastFolderIndex && lineIndex <= lastRowIndex; ++lineIndex)
            {
                Console.SetCursorPosition(0, lineIndex);
                Write(ServiceColor, foldersUnderTop[lineIndex].Prefix);
                Write(foldersUnderTop[lineIndex].Color, foldersUnderTop[lineIndex].Name);
            }
            Console.SetCursorPosition(Current.Offset, CursorPosition);
            Console.BackgroundColor = ServiceColor;
            Write(Current.Color, Current.Name);
        }
    }
}