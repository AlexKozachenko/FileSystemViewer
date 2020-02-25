namespace FileSystemViewer
{
    internal abstract class Command
    {
        public ProgramLogic FileViewer { get; }

        public Command(ProgramLogic fileViewer)
        {
            FileViewer = fileViewer;
        }

        public abstract void Execute();
    }
}