namespace FileSystemViewer
{
    internal abstract class SelectionCommand
    {
        public SelectionAndScrolling Selection { get; set; }
        public SelectionCommand(SelectionAndScrolling selection)
        {
            Selection = selection;
        }
    }
}
