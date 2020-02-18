namespace FileSystemViewer
{
    internal class Close : Command
    {
        public Close(ProgramLogic logic) : base(logic)
        {
        }
        public override void Execute()
        {
            Logic.Close();
        }
    }
}