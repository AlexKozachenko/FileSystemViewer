namespace FileSystemViewer
{
    internal class MoveUp : SelectionCommand, ICommand
    {
        public MoveUp(SelectionAndScrolling selection) : base(selection)
        {
        }
        public void Execute()
        {
            --Selection.Position;
        }
    }
}