using System;
using System.Collections.Generic;

using System.IO;


namespace FileSystemViewer
{
    internal class Root : DefaultFolder
    {
        private const string RootFullPath = "";
        public Root()
        {
            FullPath = RootFullPath;
            Name = "ThisPC";
        }
        public List<DriveName> ChildrenTemporary { get; }
        public override ConsoleColor Color => ConsoleColor.Blue;
        public int LastContainerIndex { get; set; } = -1;
        public void Expand() { }
        public void Collapse() { }
    } 
}