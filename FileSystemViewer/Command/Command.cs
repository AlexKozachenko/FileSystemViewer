namespace FileSystemViewer
{
    internal abstract class Command
    {
        public ProgramLogic Logic { get; set; }
        public Command(ProgramLogic logic)
        {
            Logic = logic;
        }
        public abstract void Execute();
    }
}
