using System;
using System.IO;

namespace FileSystemViewer.Components
{
    internal class FileComponent : FolderComponent
    {
        private enum Bytes : long
        {
            KB = 1024,
            MB = 1024 * 1024,
            GB = 1024 * 1024 * 1024
        }

        private const string FilePrefix = "  ";

        public FileComponent(string fullPath, string parentPrefix) : base(fullPath, parentPrefix)
        {
        }

        public override ConsoleColor Color => ConsoleColor.Cyan;

        protected override bool IsEmpty => true;

        public override string Prefix { get; protected set; } = FilePrefix;

        protected override void CutName()
        {
            int cut = LastColumnIndex - Offset - Size().Length - ThreeDot.Length;
            if (Name.Length > cut)
            {
                Name = Name.Remove(cut) + ThreeDot + Size();
            }
            else
            {
                Name = Name + Size();
            }
        }

        private string Size()
        {
            string fileSize = "";
            long size = new FileInfo(FullPath).Length;
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
            return fileSize;
        }
    }
}