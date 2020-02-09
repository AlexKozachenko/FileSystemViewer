namespace FileSystemViewer
{
    internal class Open : Command
    {
        public Open(Program viewer) : base(viewer)
        {
        }
        public override void Execute()
        {
            Viewer.Open();
        }
    }
}