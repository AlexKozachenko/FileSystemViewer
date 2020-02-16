namespace FileSystemViewer
{
    internal class Open : OpenCloseCommand, ICommand
    { 
        public Open(FolderManagement manageFolder) : base(manageFolder)
        {
        }
        public void Execute()
        {
            ManageFolder.Open();
        }
    }
}