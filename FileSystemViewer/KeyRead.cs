using System;

namespace FileSystemViewer
{
    internal class KeyRead : ICommand
    {
        private ConsoleKey input;
        private FileViewer viewer;

        public KeyRead(ConsoleKey input, FileViewer viewer)
        {
            this.viewer = viewer;
            this.input = input;
        }

        public void Execute()
        {
            foreach (Key key in new KeyBoard())
            {
                if (key.Input == input)
                {
                    key.Action(viewer);
                    break;
                }
            }
        }
    }
}