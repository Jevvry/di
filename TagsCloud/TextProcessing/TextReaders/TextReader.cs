﻿using System.Collections.Generic;
using System.IO;

namespace TagsCloud.TextProcessing.TextReaders
{
    public class TextReader : ITextReader
    {
        public bool CanRead(string path) => path.EndsWith(".txt");

        public IEnumerable<string> ReadWords(string path) => File.ReadAllText(path).Split(' ', '\n');
    }
}
