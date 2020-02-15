using System;
using System.Collections.Generic;

namespace FileSystemViewer
{
    internal class ScrollLogic
    {
        private int cursorPosition = 0;
        private Stack<DefaultFolder> foldersOverTop = new Stack<DefaultFolder>();
        public ScrollLogic(List<DefaultFolder> folders)
        {
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;
            FoldersUnderTop = folders;
        }
        public DefaultFolder Current => FoldersUnderTop[Position];
        public List<DefaultFolder> FoldersUnderTop { get; }
        public int MaxFolderIndex => FoldersUnderTop.Count - 1;
        public int MaxRowIndex => Console.WindowHeight - 1;
        public int Position
        {
            get
            {
                return cursorPosition;
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
                    PushTop();
                    value = MaxRowIndex;
                }
                cursorPosition = value;
            }
        }
        private void PopInTop()
        {
            if (foldersOverTop.Count > 0)
            {
                FoldersUnderTop.Insert(0, foldersOverTop.Pop());
            }
        }
        private void PushTop()
        {
            foldersOverTop.Push(FoldersUnderTop[0]);
            FoldersUnderTop.RemoveAt(0);
        }
        public static ScrollLogic operator ++(ScrollLogic cursor)
        {
            ++cursor.Position;
            return cursor;
        }
        public static ScrollLogic operator --(ScrollLogic cursor)
        {
            --cursor.Position;
            return cursor;
        }
    }
}