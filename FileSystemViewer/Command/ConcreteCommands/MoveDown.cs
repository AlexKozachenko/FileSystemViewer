namespace FileSystemViewer
{
    internal class MoveDown : DefaultKey, ICommand
    {
        public MoveDown(ProgramLogic logic) : base(logic)
        {
        }
        public void Execute()
        {
            ++Logic.Position;
        }
    }
}