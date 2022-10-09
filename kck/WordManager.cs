using System.Data;
using System.Diagnostics.Tracing;
using System.Security.Cryptography;

namespace kck
{
    public class WordManager
    {
        public List<Word> Words { get; set; }
        public string FileName { get; set; } = "words.txt";

        public WordManager()
        {
            Words = new List<Word>();

            if (File.Exists(FileName))
            {
                var fileLines = File.ReadAllLines(FileName);

                foreach (var line in fileLines)
                {
                    AddWord(line, false);
                }
            }
        }
        
        public void AddWord(string name, bool shouldSaveToFile = true)
        {
            var word = new Word();
            word.Name = name;

            Words.Add(word);
            if (shouldSaveToFile)
            {
                File.AppendAllLines(FileName, new List<string> { name });
            }
            
        }
        


    }
}
