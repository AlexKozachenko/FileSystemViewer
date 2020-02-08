namespace FileSystemViewer
{
    internal class MoveDown : DefaultAction, ICommand
    {
        public MoveDown(Program viewer) : base(viewer)
        {
        }
        public void Execute()
        {
            Viewer.MoveDown();
        }
    }
}