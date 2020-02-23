namespace FileSystemViewer
{
    internal class MoveUp : DefaultCommand
    {
        public MoveUp(ProgramLogic logic) : base(logic)
        {
        }

        public override void Execute()
        {
            --Logic.SelectionPosition;
        }
    }
}