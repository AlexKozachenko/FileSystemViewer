using System.IO;
using System.Collections.ObjectModel;

namespace FileSystemViewer
{
    internal class FolderManagement
    {
        private Collection<DriveName> childrenTemporary = new Collection<DriveName>();
        public FolderManagement(SelectionAndScrolling selection)
        {
            Selection = selection;
        }
        public SelectionAndScrolling Selection { get; set; }
        public DefaultFolder Current => Selection.FoldersUnderTop[Selection.Position];
        public void Close()
        {
            if (Current.IsOpen)
            {
                Selection.FoldersUnderTop.RemoveAll(folder => folder.FullPath.Contains(Current.FullPath)
                    && folder.Depth > Current.Depth);
                Current.IsOpen = false;
            }
        }
        public void Open()
        {
            int lastContainerIndex = - 1;
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
                if (childrenTemporary.Count > 0)
                {
                    int nextPosition = Selection.Position + 1;
                    if (lastContainerIndex >= 0)
                    {
                        childrenTemporary[lastContainerIndex].SetLastContainerPrefix();
                    }
                    Selection.FoldersUnderTop.InsertRange(nextPosition, childrenTemporary);
                    childrenTemporary.Clear();
                    Current.IsOpen = true;
                }
                else
                {
                    Current.IsEmpty = true;
                }
            }
            //при открытии папки на последней строке список скроллится на 1 вниз
            if (Selection.Position == Selection.MaxRowIndex && Current.IsOpen)
            {
                ++Selection.Position;
            }
        }
    }
}