namespace FileSystemViewer
{
    internal abstract class Command
    {
        public Program Viewer { get; }
        public Command(Program viewer)
        {
            Viewer = viewer;
        }
        public abstract void Execute();
    }
}
