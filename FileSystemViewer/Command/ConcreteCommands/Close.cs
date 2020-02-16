namespace FileSystemViewer
{
    internal class Close : OpenCloseCommand, ICommand
    {
        public Close(FolderManagement manageFolder) : base(manageFolder)
        {
        }
        public void Execute()
        {
            ManageFolder.Close();
        }
    }
}