﻿namespace FileSystemViewer.Commands.ConcreteCommands
{
    internal class Open : Command
    { 
        public Open(ProgramLogic fileViewer) : base(fileViewer)
        {
        }

        public override void Execute()
        {
            FileViewer.Expand();
        }
    }
}