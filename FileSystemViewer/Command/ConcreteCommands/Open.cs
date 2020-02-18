namespace FileSystemViewer
{
    internal class Open : Command
    { 
        public Open(ProgramLogic logic) : base(logic)
        {
        }
        public override void Execute()
        {
            Logic.Open();
        }
    }
}