using System.Collections;
using System.Collections.Generic;

namespace FileSystemViewer
{
    internal class KeyBoard : IEnumerable
    {
        private ICollection<Key> keys = new List<Key>();

        public KeyBoard()
        {
            keys.Add(new MoveUp());
            keys.Add(new MoveDown());
            keys.Add(new Collapse());
            keys.Add(new Expand());
        }

        public IEnumerator GetEnumerator()
        {
            return keys.GetEnumerator();
        }
    }
}