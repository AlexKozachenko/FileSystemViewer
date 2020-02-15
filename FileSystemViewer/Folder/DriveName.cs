using System;

namespace FileSystemViewer
{
    internal class DriveName : DefaultFolder
    {
        protected const int DriveDeep = 1;
        protected const int Step = 2;
        public DriveName(string fullPath) : base(fullPath)
        {
            GetDeep();
            GetName();
            GetOffset();
            GetPrefix();
        }
        public override ConsoleColor Color => ConsoleColor.Green;
        public bool IsLeaf { get; protected set; }
        protected virtual void GetDeep()
        {
            Deep = DriveDeep;
        }
        protected virtual void GetName()
        {
            Name = FullPath;
        }
        protected void GetOffset()
        {
            Offset = Deep * Step;
        }
        protected virtual void GetPrefix()
        {
            Prefix = ((char)0x251C).ToString() + (char)0x2500;
        }
        public void GetLastContainerPrefix()
        {
            Prefix = Prefix.Replace((char)0x251C,(char)0x2514);
        }
    }
}