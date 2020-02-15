using System;
using System.IO;

namespace FileSystemViewer
{
    internal class FileName : FolderName
    {
        private enum Bytes : long
        {
            KB = 1024,
            MB = 1024 * 1024,
            GB = 1024 * 1024 * 1024
        }
        public FileName(string fullPath, string parentPrefix) : base(fullPath, parentPrefix)
        {
            IsEmpty = IsLeaf = true;
        }
        public override ConsoleColor Color => ConsoleColor.Cyan;
        protected override void CutName()
        {
            int cut = Console.WindowWidth - Offset - Size().Length - "...".Length - 1;
            if (Name.Length > cut)
            {
                Name = Name.Remove(cut) + "..." + Size();
            }
            else
            {
                Name = Name + Size();
            }
        }
        protected override void GetPrefix()
        {
            Prefix = "  ";
        }
        private string Size()
        {
            string fileSize = "";
            long size = 0;
            if (File.Exists(FullPath))
            {
                size = new FileInfo(FullPath).Length;
                if (size < (long)Bytes.KB)
                {
                    fileSize = " (" + size.ToString() + " B)";
                }
                if (size >= (long)Bytes.KB && size < (long)Bytes.MB)
                {
                    fileSize = " (" + Math.Round(size / (double)Bytes.KB, 2).ToString() + " Kb)";
                }
                if (size >= (long)Bytes.MB && size < (long)Bytes.GB)
                {
                    fileSize = " (" + Math.Round(size / (double)Bytes.MB, 2).ToString() + " Mb)";
                }
                if (size >= (long)Bytes.GB)
                {
                    fileSize = " (" + Math.Round(size / (double)Bytes.GB, 2).ToString() + " Gb)";
                }
            }
            return fileSize;
        }
    }
}