namespace FileSystemViewer
{
    internal class Close : DefaultAction, ICommand
    {
        public Close(Program viewer) : base(viewer)
        {
        }
        public void Execute()
        {
            Viewer.Close();
        }
    }
}