namespace kck
{
    internal class Game
    {

        public WordManager WordManager { get; set; } = new WordManager();

        public void Start()
        {
            
            Console.Title = "Wordle";
            RunMainMenu();
        }

        private void RunMainMenu()
        {
            var prompt = "Witaj w grze!";
            var optionsList = new List<string>()
            {
                "Nowa gra",
                "Zmien liczbe liter",
                "Stworz wlasne slowo",
                "Koniec gry"
            };
            var mainMenu = new Menu(prompt, optionsList);

            var selectedIndex = mainMenu.Run();
            switch (selectedIndex)
            {
                case 0:
                    NewGame();
                    break;
                case 1:
                    break;
                case 2:
                    AddWord();
                    break;
                case 3:
                    break;
                default:
                    Console.WriteLine("Błędny wybór");
                    break;
            }

        }
        private void NewGame()
        {
            Console.Clear();
            var test = "testy";
            var word = default(string);
            Console.WriteLine("Podaj słowo:");
            do
            {
                word = Console.ReadLine();
                var result = CheckLetters(word, test);
                DisplayResult(result, word);
            }
            while (test!=word);

            //for(int i = 0; i < test.Length; i++)
            //Console.WriteLine("z tej funkcji wyszlo:" + costam[i]);

        }
        private void changeColorYellow()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        private void changeColorGreen()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        private void changeColorDefault()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public enum LetterStatus
        {
                Empty,
                Wrong,
                WrongPosition,
                Correct
        }
        public LetterStatus[] CheckLetters(string _word, string _test)
        {
            var result = new LetterStatus[_test.Length];
            for(int i = 0; i < _test.Length; i++)
            {
                if (_word[i]== _test[i])
                {
                    result[i] = LetterStatus.Correct;
                }
                else if (_test.Contains(_word[i])){
                    result[i] = LetterStatus.WrongPosition;
                }
                else
                {
                    result[i] = LetterStatus.Wrong;
                }
            }
            return result;
        }
        public void DisplayResult(LetterStatus[] _result, string word)
        {
            for(int i = 0; i < _result.Length; i++)
            {
                if (_result[i] == LetterStatus.Correct)
                {
                    changeColorGreen();
                    Console.Write(word[i]);
                }
                else if (_result[i] == LetterStatus.WrongPosition)
                {
                    changeColorYellow();
                    Console.Write(word[i]);
                }
                else if (_result[i]== LetterStatus.Wrong)
                {
                    changeColorDefault();
                    Console.Write(word[i]);
                }
            }
            Console.WriteLine();
            changeColorDefault();
        }
        private void AddWord()
        {
            Console.WriteLine("Podaj słowo, które chcesz dodać do bazy");

            var word = Console.ReadLine();

            WordManager.AddWord(word);
        }
    }
}
