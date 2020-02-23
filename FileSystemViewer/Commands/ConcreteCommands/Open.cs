namespace FileSystemViewer
{
    internal class Open : DefaultCommand
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