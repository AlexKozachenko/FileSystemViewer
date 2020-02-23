namespace FileSystemViewer
{
    internal class Open : DefaultKey, ICommand
    { 
        public Open(ProgramLogic logic) : base(logic)
        {
        }
        public void Execute()
        {
            Logic.Open();
        }
    }
}