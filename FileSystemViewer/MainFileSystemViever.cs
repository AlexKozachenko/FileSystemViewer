namespace FileSystemViewer
{
    internal class MainFileSystemViever
    {
        public static void Main(string[] arguments)
        {
            FileViewer fileViewer = new FileViewer();
            FileViewer.Process(fileViewer);
        }
    }
}