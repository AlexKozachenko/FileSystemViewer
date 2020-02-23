namespace FileSystemViewer
{
    internal class MoveUp : DefaultKey, ICommand
    {
        public MoveUp(ProgramLogic logic) : base(logic)
        {
        }
        public void Execute()
        {
            --Logic.Position;
        }
    }
}