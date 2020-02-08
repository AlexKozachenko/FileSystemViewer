namespace FileSystemViewer
{
    internal abstract class DefaultAction
    {
        public Program Viewer { get; }
        public DefaultAction(Program viewer)
        {
            Viewer = viewer;
        }
    }
}
