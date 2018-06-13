using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemViewer
{
    internal class FileViewer
    {
        private const int DriveNameLength_ = 3;
        private List<string> folders = new List<string>();
        private string lastDriveName;
        private List<string> openFolders = new List<string>();
        private int positionInFolders = 0;

        public FileViewer()
        {
            Console.CursorVisible = false;
            DriveInfo[] drives = DriveInfo.GetDrives();
            // список папок первоначально инициализируется в конструкторе списком жестких дисков
            foreach (DriveInfo drive in drives)
            {
                folders.Add(drive.Name);
            }
            lastDriveName = drives[drives.Length - 1].Name;
        }

        public int DriveNameLength => DriveNameLength_;

        public List<string> Folders => folders;

        public string LastDriveName => lastDriveName;

        public List<string> OpenFolders => openFolders;

        public int PositionInFolders
        {
            get => positionInFolders;
            set => positionInFolders = value;
        }

        public static void Process(FileViewer fileViewer)
        {
            try
            {
                do
                {
                    if (fileViewer.folders.Count < Console.BufferHeight)
                    {
                        fileViewer.Print();
                    }
                    else
                    {
                        fileViewer.Scroll();
                    }
                    ICommand useKey = new KeyRead(Console.ReadKey().Key, fileViewer);
                    useKey.Execute();
                } while (true);
            }
            catch (IOException)
            {
                Process(fileViewer);
            }
            catch (UnauthorizedAccessException)
            {
                Process(fileViewer);
            }
        }

        public int CountSlashes(string path)
        {
            int slashes = 0;
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] == '\\')
                {
                    slashes++;
                }
            }
            return slashes;
        }

        private string FileSize(string path)
        {
            string fileSize = "";
            long size = 0;
            if (File.Exists(path))
            {
                size = new FileInfo(path).Length;
                if (size < (long)Bytes.KB)
                {
                    fileSize = " (" + size.ToString() + " B)";
                }
                if (size >= (long)Bytes.KB && size < (long)Bytes.MB)
                {
                    fileSize = " (" + Math.Round(size / (double)Bytes.KB, 2).ToString() + " Kb)";
                }
                if (size >= (long)Bytes.MB && size < (long)Bytes.GB)
                {
                    fileSize = " (" + Math.Round(size / (double)Bytes.MB, 2).ToString() + " Mb)";
                }
                if (size >= (long)Bytes.GB)
                {
                    fileSize = " (" + Math.Round(size / (double)Bytes.GB, 2).ToString() + " Gb)";
                }
            }
            return fileSize;
        }

        private string FinalFolder(string path)
        {
            string[] foldersInPath = path.Split('\\');
            // индекс последнего элемента коллекции равен размеру коллекции - 1, т.к. индексация начинается от 0
            return foldersInPath[foldersInPath.Length - 1];
        }

        private void FormatPath(string path, int cursorPosition, ConsoleColor backGroundColor)
        {
            const int Offset = 2;
            string fileSize = FileSize(path);
            string folder = FinalFolder(path);
            int stringOffset = CountSlashes(path) * Offset;
            if (path.Length > DriveNameLength_)
            {
                Console.SetCursorPosition(stringOffset, cursorPosition);
                Console.BackgroundColor = backGroundColor;
                if (Directory.Exists(path))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                if (folder.Length + stringOffset + fileSize.Length > Console.BufferWidth)
                {
                    folder = folder.Remove(Console.BufferWidth - stringOffset - fileSize.Length - "...".Length) + "...";
                }
                Console.WriteLine(folder + fileSize);
            }
            else
            {
                Console.SetCursorPosition(0, cursorPosition);
                Console.BackgroundColor = backGroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(path);
            }
            Console.ResetColor();
        }

        public List<string> GetFolders(string path)
        {
            List<string> folders = new List<string>();
            if (Directory.Exists(path))
            {
                string[] directories = Directory.GetDirectories(path);
                string[] files = Directory.GetFiles(path);
                foreach (string subfolderPath in directories)
                {
                    folders.Add(subfolderPath);
                }
                foreach (string filePath in files)
                {
                    folders.Add(filePath);
                }
            }
            return folders;
        }

        public List<string> InsertList(List<string> source, List<string> destination, int index)
        {
            List<string> temporary = new List<string>();
            for (int i = 0; i <= index; i++)
            {
                temporary.Add(destination[i]);
            }
            for (int i = 0; i < source.Count; i++)
            {
                temporary.Add(source[i]);
            }
            for (int i = index + 1; i < destination.Count; i++)
            {
                temporary.Add(destination[i]);
            }
            return temporary;
        }

        public bool IsOpen(string path)
        {
            bool isOpen = false;
            foreach (string path_ in OpenFolders)
            {
                if (path_.CompareTo(path) == 0)
                {
                    isOpen = true;
                    break;
                }
            }
            return isOpen;
        }

        private void Print()
        {
            int index = 0;
            foreach (string path in folders)
            {
                FormatPath(path, index, ConsoleColor.Black);
                index++;
            }
            // Accentuation bar
            FormatPath(folders[positionInFolders], positionInFolders, ConsoleColor.DarkGray);
        }
        // прокрутка не такая, как требуется, но думаю над вопросом
        private void Scroll()
        {
            Console.Clear();
            for (int i = positionInFolders, index = 0;
                (i < positionInFolders + Console.BufferHeight - 1)
                && (i < folders.Count); i++, index++)
            {
                FormatPath(folders[i], index, ConsoleColor.Black);
            }
            FormatPath(folders[positionInFolders], 0, ConsoleColor.DarkGray);
        }
    }
}