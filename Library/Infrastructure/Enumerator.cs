using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Library.Infrastructure
{
    public class CSVEnumerator : IEnumerator<string>
    {
        private readonly string path;
        private StreamReader reader;

        public CSVEnumerator(string path)
        {
            this.path = path;
            reader = new StreamReader(path, System.Text.Encoding.UTF8);
            reader.ReadLine();
        }

        public string Current { get; set; }
        object IEnumerator.Current { get => Current; }

        public void Dispose()
        {
            reader.Dispose();
        }

        public bool MoveNext()
        {
            Current = reader.ReadLine();
            return Current != null;
        }

        public void Reset()
        {
            reader.Dispose();
            reader = new StreamReader(path);
        }
    }

    public class CSVEnumerable : IEnumerable<string>
    {
        private readonly string path;

        public CSVEnumerable(string path)
        {
            this.path = path;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return new CSVEnumerator(path);
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return new CSVEnumerator(path);
        }
    }
}