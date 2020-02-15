namespace FileSystemViewer
{
    internal class MoveDown : Command
    {
        public MoveDown(Program viewer) : base(viewer)
        {
        }
        public override void Execute()
        {
            ++Viewer.Cursor;
        }
    }
}