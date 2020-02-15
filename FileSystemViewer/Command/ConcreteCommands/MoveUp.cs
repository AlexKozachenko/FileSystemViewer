namespace FileSystemViewer
{
    internal class MoveUp : Command
    {
        public MoveUp(Program viewer) : base(viewer)
        {
        }
        public override void Execute()
        {
            --Viewer.Cursor;
        }
    }
}