namespace FileSystemViewer
{
    internal class MoveUp : DefaultAction, ICommand
    {
        public MoveUp(Program viewer) : base(viewer)
        {
        }
        public void Execute()
        {
            Viewer.MoveUp();
        }
    }
}