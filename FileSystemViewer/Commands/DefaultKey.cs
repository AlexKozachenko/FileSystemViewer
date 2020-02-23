namespace FileSystemViewer
{
    internal abstract class DefaultKey
    {
        public ProgramLogic Logic { get; set; }
        public DefaultKey(ProgramLogic logic)
        {
            Logic = logic;
        }
    }
}