using System.Collections.Generic;
using System.IO;

namespace FileSystemViewer
{
    internal class OpenCloselogic
    {
        private List<DriveName> childrenTemporary = new List<DriveName>();
        public OpenCloselogic(ScrollLogic scroll)
        {
            Scroll = scroll;
        }
        public ScrollLogic Scroll { get; set; }
        public void Close()
        {
            if (Scroll.Current.IsOpen)
            {
                Scroll.FoldersUnderTop.RemoveAll(folder => folder.FullPath.Contains(Scroll.Current.FullPath)
                    && folder.Depth > Scroll.Current.Depth);
                Scroll.Current.IsOpen = false;
            }
        }
        private void GetChildren()
        {
            if (Scroll.Current.Depth == 0)
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    childrenTemporary.Add(new DriveName(drive.Name));
                }
                childrenTemporary[childrenTemporary.Count - 1].SetLastContainerPrefix();
            }
            else
            {
                if (Directory.Exists(Scroll.Current.FullPath))
                {
                    foreach (string directory in Directory.GetDirectories(Scroll.Current.FullPath))
                    {
                        childrenTemporary.Add(new FolderName(directory, Scroll.Current.Prefix));
                    }
                    if (childrenTemporary.Count > 0)
                    {
                        childrenTemporary[childrenTemporary.Count - 1].SetLastContainerPrefix();
                    }
                    foreach (string file in Directory.GetFiles(Scroll.Current.FullPath))
                    {
                        childrenTemporary.Add(new FileName(file, Scroll.Current.Prefix));
                    }
                }
            }
        }
        public void Open()
        {
            int nextPosition = Scroll.Position + 1;
            //первый раз проверяется любая папка (не пустая по умолчанию), т.к. неизвестно, пустая она или нет,
            //если пустая, при следующем раскрытии процесс получения вложенных папок отменяется
            if (!Scroll.Current.IsOpen && !Scroll.Current.IsEmpty)
            {
                GetChildren();
                if (childrenTemporary.Count > 0)
                {
                    Scroll.FoldersUnderTop.InsertRange(nextPosition, childrenTemporary);
                    childrenTemporary.Clear();
                    Scroll.Current.IsOpen = true;
                }
                else
                {
                    Scroll.Current.IsEmpty = true;
                }
            }
            //при открытии папки на последней строке список скроллится на 1 вниз
            if (Scroll.Position == Scroll.MaxRowIndex && Scroll.Current.IsOpen)
            {
                ++Scroll;
            }
        }
    }
}