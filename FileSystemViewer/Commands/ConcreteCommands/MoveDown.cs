namespace FileSystemViewer.Commands.ConcreteCommands
{
    internal class MoveDown : Command
    {
        public MoveDown(ProgramLogic fileViewer) : base(fileViewer)
        {
        }

        public override void Execute()
        {
            ++FileViewer.SelectionPosition;
        }
    }
}