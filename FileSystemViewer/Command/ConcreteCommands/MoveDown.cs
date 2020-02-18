namespace FileSystemViewer
{
    internal class MoveDown : Command
    {
        public MoveDown(ProgramLogic logic) : base(logic)
        {
        }
        public override void Execute()
        {
            ++Logic.Position;
        }
    }
}