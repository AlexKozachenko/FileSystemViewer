namespace FileSystemViewer
{
    internal class Close : DefaultCommand
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