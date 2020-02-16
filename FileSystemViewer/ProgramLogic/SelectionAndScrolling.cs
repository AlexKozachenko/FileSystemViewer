using System;
using System.Collections.Generic;

namespace FileSystemViewer
{
    internal class SelectionAndScrolling
    {
        private Stack<DefaultFolder> foldersOverTop = new Stack<DefaultFolder>();
        private int position = 0;
        public SelectionAndScrolling(List<DefaultFolder> folders)
        {
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;
            FoldersUnderTop = folders;
        }
        public List<DefaultFolder> FoldersUnderTop { get; }
        public int MaxFolderIndex => FoldersUnderTop.Count - 1;
        public int MaxRowIndex => Console.WindowHeight - 1;
        public int Position
        {
            get
            {
                return position;
            }
            set
            {
                if (value < 0)
                {
                    PopInTop();
                    value = 0;
                }
                if (value > MaxFolderIndex)
                {
                    //протяжка вниз до последней строки, если последняя папка закрылась посреди окна
                    if (MaxFolderIndex < MaxRowIndex)
                    {
                        PopInTop();
                    }
                    value = MaxFolderIndex;
                }
                if (value > MaxRowIndex)
                {
                    //PushTop:
                    foldersOverTop.Push(FoldersUnderTop[0]);
                    FoldersUnderTop.RemoveAt(0);
                    value = MaxRowIndex;
                }
                position = value;
            }
        }
        private void PopInTop()
        {
            if (foldersOverTop.Count > 0)
            {
                FoldersUnderTop.Insert(0, foldersOverTop.Pop());
            }
        }
    }
}