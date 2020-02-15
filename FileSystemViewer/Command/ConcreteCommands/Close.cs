namespace FileSystemViewer
{
    internal class Close : Command
    {
        public Close(Program viewer) : base(viewer)
        {
        }
        public override void Execute()
        {
            Viewer.OpenClose.Close();
        }
    }
}