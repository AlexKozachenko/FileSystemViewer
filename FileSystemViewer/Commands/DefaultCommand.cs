namespace FileSystemViewer
{
    internal abstract class DefaultCommand
    {
        public ProgramLogic Logic { get; }

        public DefaultCommand(ProgramLogic logic)
        {
            Logic = logic;
        }

        public abstract void Execute();
    }
}