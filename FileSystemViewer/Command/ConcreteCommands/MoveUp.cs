namespace FileSystemViewer
{
    internal class MoveUp : Command
    {
        public MoveUp(ProgramLogic logic) : base(logic)
        {
        }
        public override void Execute()
        {
            --Logic.Position;
        }
    }
}