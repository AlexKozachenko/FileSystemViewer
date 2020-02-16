namespace FileSystemViewer
{
    internal class MoveDown : SelectionCommand, ICommand
    {
        public MoveDown(SelectionAndScrolling selection) : base(selection)
        {
        }
        public void Execute()
        {
            ++Selection.Position;
        }
    }
}