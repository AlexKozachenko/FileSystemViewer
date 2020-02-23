namespace FileSystemViewer
{
    internal class Close : DefaultKey, ICommand
    {
        public Close(ProgramLogic logic) : base(logic)
        {
        }
        public void Execute()
        {
            Logic.Close();
        }
    }
}