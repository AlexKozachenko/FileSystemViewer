namespace FileSystemViewer
{
    internal abstract class OpenCloseCommand 
    {
        public FolderManagement ManageFolder { get; }
        public OpenCloseCommand(FolderManagement manageFolder)
        {
            ManageFolder = manageFolder;
        }
    }
}