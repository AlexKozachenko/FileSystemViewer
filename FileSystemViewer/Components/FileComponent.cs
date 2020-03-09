using static FileSystemViewer.Components.Literals;
using System;
using System.IO;

namespace FileSystemViewer.Components
{
    internal class FileComponent : FolderComponent
    {
        private enum FileSize : long
        {
            KB = 1024,
            MB = 1024 * 1024,
            GB = 1024 * 1024 * 1024
        }

        public FileComponent(string fullPath, string parentPrefix) : base(fullPath, parentPrefix) => IsEmpty = true;

        public override ConsoleColor Color => ConsoleColor.Cyan;

        public override string Prefix { get; protected set; } = FilePrefix;

        protected override void CutName()
        {
            string fileLength = GetFileLength();
            int cut = LastColumnIndex - Offset - fileLength.Length - Ellipsis.Length;
            if (Name.Length > cut)
            {
                Name = Name.Remove(cut) + Ellipsis + fileLength;
            }
            else
            {
                Name = Name + fileLength;
            }
        }

        private string GetFileLength()
        {
            string fileLength = EmptyString;
            long length = new FileInfo(FullPath).Length;
            void GetLength(string size, string bytes)
            {
                fileLength = OpeningBracket + size + bytes + ClosingBracket;
            }
            string Round(FileSize bytes) => Math.Round(length / (double)bytes, 2).ToString();
            if (length < (long)FileSize.KB)
            {
                GetLength(length.ToString(), Bytes);
            }
            if (length >= (long)FileSize.KB && length < (long)FileSize.MB)
            {
                GetLength(Round(FileSize.KB), KiloBytes);
            }
            if (length >= (long)FileSize.MB && length < (long)FileSize.GB)
            {
                GetLength(Round(FileSize.MB), MegaBytes);
            }
            if (length >= (long)FileSize.GB)
            {
                GetLength(Round(FileSize.GB), GigaBytes);
            }
            return fileLength;
        }
    }
}