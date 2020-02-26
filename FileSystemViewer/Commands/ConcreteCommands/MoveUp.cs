namespace FileSystemViewer.Commands.ConcreteCommands
{
    internal class MoveUp : Command
    {
        public MoveUp(ProgramLogic fileViewer) : base(fileViewer)
        {
        }

        public override void Execute()
        {
            --FileViewer.SelectionPosition;
        }
    }
}