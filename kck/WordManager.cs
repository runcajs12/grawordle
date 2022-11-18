namespace kck
{
    public class WordManager
    {
        public List<Word> Words { get; set; }
        public string FileName { get; set; } = "../../../words.txt";

        public WordManager()
        {
            Words = new List<Word>();

            if (File.Exists(FileName))
            {
                var fileLines = File.ReadAllLines(FileName);

                foreach (var line in fileLines)
                {
                    SaveWord(line, false);
                }
            }
        }
        public void RemoveWord(int index)
        {
            Words.RemoveAt(index);
            StreamWriter sw = new StreamWriter(FileName, false);
            foreach(var line in Words)
            {
                sw.WriteLine(line.Name);
            }
            sw.Close();      
        }
        public bool canRemove(int length)
        {
            int counter = 0;

            foreach (var word in Words)
            {
                if (word.Name.Length == length)
                {
                    counter++;
                }
            }
            if (counter > 1)
                return true;
            else return false;
        }
        public void SaveWord(string name, bool shouldSaveToFile = true)
        {
            var word = new Word();
            word.Name = name;

            Words.Add(word);
            if (shouldSaveToFile)
            {
                File.AppendAllLines(FileName, new List<string> { name });
            }

        }


        public string DrawWord(int numberOfLetters)
        {
            var wordsInChoosenNumberOfLetters = new List<string>();
            foreach (var word in Words)
            {
                if(word.Name.Length == numberOfLetters)
                {
                    wordsInChoosenNumberOfLetters.Add(word.Name);
                }
            }
            var random = new Random();
            int index = random.Next(wordsInChoosenNumberOfLetters.Count);
            //Console.WriteLine(wordsInChoosenNumberOfLetters[index]);
            return wordsInChoosenNumberOfLetters[index];
        }
        public bool IfWordExist(string newWord)
        {
            var ifWordExist = false;
            foreach (var word in Words)
            {
                if(word.Name == newWord)
                {
                    ifWordExist = true;   
                }
            }

            return ifWordExist;
        }

        
    }
}
