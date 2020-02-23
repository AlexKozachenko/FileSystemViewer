namespace FileSystemViewer
{
    internal class MoveDown : DefaultCommand
    {
        public MoveDown(ProgramLogic logic) : base(logic)
        {
        }

        public override void Execute()
        {
            ++Logic.SelectionPosition;
        }
    }
}