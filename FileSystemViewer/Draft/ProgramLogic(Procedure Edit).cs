//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;

//namespace FileSystemViewer
//{
//    internal class ProgramLogic
//    {
//        private const int WindowHeight = 30;
//        private const int WindowWidth = 100;
//        private int cursorPosition = 0;
//        private List<AbstractFolder> folders;
//        private IEnumerable keys;
//        private int positionInFolders = 0;
//        private IList<AbstractFolder> screen;
//        public ProgramLogic()
//        {
//            folders = new List<AbstractFolder>()
//            {
//                new Root()
//            };
//            screen = new List<AbstractFolder>()
//            {
//                new Root()
//            };
//            keys = new AssignedKeys(this);
//        }
//        public void Run()
//        {
//            Console.SetWindowSize(WindowWidth, WindowHeight);
//            Console.SetBufferSize(WindowWidth, WindowHeight);
//            Console.CursorVisible = false;
//            try
//            {
//                do
//                {
//                    WriteScreen();
//                    new KeyRead(keys).Action();
//                } while (true);
//            }
//            catch (IOException)
//            {
//                Run();
//            }
//            catch (UnauthorizedAccessException)
//            {
//                Run();
//            }
//        }
//        private void WriteScreen()
//        {
//            Console.ResetColor();
//            Console.Clear();
//            for (int i = 0; i < screen.Count; ++i)
//            {
//                Console.SetCursorPosition(0, i);
//                Console.ForegroundColor = ConsoleColor.DarkGray;
//                Console.Write(screen[i].Prefix);
//                Console.ForegroundColor = screen[i].Color;
//                Console.Write(screen[i].Name);
//            }
//            Console.SetCursorPosition(screen[cursorPosition].Offset, cursorPosition);
//            Console.BackgroundColor = ConsoleColor.DarkGray;
//            Console.ForegroundColor = screen[cursorPosition].Color;
//            Console.Write(screen[cursorPosition].Name);

//        }
//        public void Collapse()
//        {
//            const int nextIndex = 1;
//            int index = 1;
//            AbstractFolder current = folders[positionInFolders];
//            if ((current.FullPath == "root"
//                || Directory.Exists(current.FullPath))
//                && current.IsOpen)
//            {
//                int afterCurrent = positionInFolders + index;
//                int nextDeep = folders[positionInFolders + nextIndex].Deep;
//                current.IsOpen = false;
//                if (current.Deep < nextDeep)
//                {
//                    while (afterCurrent < folders.Count
//                        && current.Deep < folders[afterCurrent].Deep)
//                    {
//                        folders.RemoveAt(afterCurrent);
//                        ++index;
//                    }
//                }
//                ModifiedScreen();
//            }
//        }
//        public void Expand()
//        {
//            AbstractFolder current = folders[positionInFolders];
//            if ((current.FullPath == "root"
//                || Directory.Exists(current.FullPath))
//                && !current.IsOpen)
//            {
//                List<AbstractFolder> children = GetChildren(current);
//                if (children.Count > 0)
//                {
//                    current.IsOpen = true;
//                    folders.InsertRange(positionInFolders + 1, children);
//                    //отрисовка новой иерархии от прежней позиции курсора
//                    ModifiedScreen();
//                    //автоскролл: при открытии папки на последней строке вызываем стрелку вниз
//                    if (cursorPosition == WindowHeight - 1
//                        && current.IsOpen)
//                    {
//                        MoveDown();
//                    }
//                }
//            }
//        }
//        private List<AbstractFolder> GetChildren(AbstractFolder parent)
//        {
//            string path = parent.FullPath;
//            List<AbstractFolder> children = new List<AbstractFolder>();
//            if (parent.FullPath == "root")
//            {
//                DriveInfo[] drives = DriveInfo.GetDrives();
//                foreach (DriveInfo drive in drives)
//                {
//                    children.Add(new DriveName(drive.Name));
//                }
//                if (children.Count > 0)
//                {
//                    children[children.Count - 1].IsLast = true;
//                }
//            }
//            if (Directory.Exists(path))
//            {
//                string[] directories = Directory.GetDirectories(path);
//                string[] files = Directory.GetFiles(path);
//                foreach (string directory in directories)
//                {
//                    children.Add(new FolderName(directory));
//                }
//                if (children.Count > 0)
//                {
//                    children[children.Count - 1].IsLast = true;
//                }
//                foreach (string file in files)
//                {
//                    children.Add(new FileName(file));
//                }
//            }
//            foreach (AbstractFolder child in children)
//            {
//                child.FormatPrefix(parent.Prefix);
//            }
//            return children;
//        }
//        //отрисовка новой иерархии (при закрытии или открытии папки) от прежней позиции курсора
//        private void ModifiedScreen()
//        {
//            screen.Clear();
//            int position = positionInFolders - cursorPosition;
//            for (int i = 0; i < folders.Count
//                && i < WindowHeight
//                && position < folders.Count; ++i)
//            {
//                screen.Add(folders[position]);
//                ++position;
//            }
//        }
//        public void MoveDown()
//        {
//            ++cursorPosition;
//            ++positionInFolders;
//            //если выделен последний элемент иерархии посреди экрана и корень над верхней границей и
//            //если курсор на последней папке или файле в иерархии, при движении вниз список тоже тянется вниз
//            if (cursorPosition > screen.Count - 1
//                && positionInFolders > folders.Count - 1
//                && screen.Count < WindowHeight
//                && screen.Count < folders.Count)
//            {
//                screen.Clear();
//                for (int i = positionInFolders - cursorPosition - 1; i < folders.Count; ++i)
//                {
//                    screen.Add(folders[i]);
//                }
//            }
//            if (cursorPosition > screen.Count - 1)
//            {
//                cursorPosition = screen.Count - 1;
//            }
//            if (positionInFolders > folders.Count - 1)
//            {
//                positionInFolders = folders.Count - 1;
//            }
//            if (positionInFolders > cursorPosition
//                && cursorPosition == WindowHeight - 1)
//            {
//                screen.Clear();
//                for (int i = positionInFolders - WindowHeight; i < positionInFolders; ++i)
//                {
//                    screen.Add(folders[i + 1]);
//                }
//            }
//        }
//        public void MoveUp()
//        {
//            --cursorPosition;
//            --positionInFolders;
//            if (cursorPosition < 0)
//            {
//                cursorPosition = 0;
//            }
//            if (positionInFolders < 0)
//            {
//                positionInFolders = 0;
//            }
//            if (folders.Count > screen.Count
//                && cursorPosition == 0
//                && positionInFolders >= 0)
//            {
//                screen.Clear();
//                for (int i = positionInFolders; i < positionInFolders + WindowHeight
//                    && i < folders.Count; ++i)
//                {
//                    screen.Add(folders[i]);
//                }
//            }
//        }
//    }
//}