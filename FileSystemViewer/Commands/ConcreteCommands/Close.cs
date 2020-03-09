namespace FileSystemViewer.Commands.ConcreteCommands
{
    internal class Close : Command
    { 
        public Close(ProgramLogic fileViewer) : base(fileViewer)
        {
        }

        public override void Execute()
        {
            FileViewer.Collapse();
        }
    }
}