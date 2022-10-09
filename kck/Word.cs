using System.Data;
using System.Diagnostics.Tracing;
using System.Security.Cryptography;

namespace kck
{
    public class Word
    {
        public List<string> Words { get; set; }
        public string FileName { get; set; } = "words.txt";

        public Word()
        {
            Words = new List<string>();

            if (File.Exists(FileName))
            {
                var fileLines = File.ReadAllLines(FileName);

                foreach (var line in fileLines)
                {
                    Words.Add(line);
                }
            }
        }



    }
}
