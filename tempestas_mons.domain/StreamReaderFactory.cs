using System;
using System.IO;
using System.Reflection;

namespace tempestas_mons.domain
{
    public class StreamReaderFactory
    {
        public virtual StreamReader GetEmbeddedFile(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var stream = assembly.GetManifestResourceStream(fileName);
            if (stream == null)
                throw new ArgumentOutOfRangeException("fileName", fileName, "Unable to find specified file");

            var reader = new StreamReader(stream);

            return reader;
        }
    }
}