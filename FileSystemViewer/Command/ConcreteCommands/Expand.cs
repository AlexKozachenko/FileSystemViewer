namespace FileSystemViewer
{
    internal class Open : DefaultAction, ICommand
    {
        public Open(Program viewer) : base(viewer)
        {
        }
        public void Execute()
        {
            Viewer.Open();
        }
    }
}