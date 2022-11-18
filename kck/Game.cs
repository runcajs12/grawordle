using kck.Enums;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace kck
{
    internal class Game
    {
        public int Level { get; set; }           //domyślne ustawienia gry
        public int Numberofletters { get; set; } //
        private WordManager WordManager { get; set; } = new WordManager();//Obiekt klasy WordManager odpowiadający za pobieranie słów z pliku,
                                                                          //losowanie, dodawanie i usuwanie słów
        public List<double> scoreboard = new List<double>();//Lista przechowująca najlepsze wyniki



        public Game(int level, int numberofletters) 
        {
            Level = level;
            Numberofletters = numberofletters;
        }

        public void Start()
        {
            
            Console.Title = "Wordle";
            RunMainMenu();//Menu główne
        }

        private void RunMainMenu()
        {
            Console.Clear();
            
            string prompt = "WITAJ W GRZE!";
            var optionsList = new List<string>()
            {
                "        Nowa gra        ",
                "   Zmień liczbe liter   ",
                "   Stwórz wlasne slowo  ",
                "       Usuń słowo       ",
                " Zmień poziom trudności ",
                "     Tablica wyników    ",
                "       Koniec gry       "
            };
            var mainMenu = new Menu(prompt, optionsList);

            var selectedIndex = mainMenu.Run();//Wywołanie menu głównego
            switch (selectedIndex)
            {
                case 0:
                    NewGame(Numberofletters, Level);
                    RunMainMenu();
                    break;
                case 1:
                    Numberofletters = ChangeNumberOfLetters();
                    RunMainMenu();
                    break;
                case 2:
                    AddWord();
                    RunMainMenu();
                    break;
                case 3:
                    DeleteWord();
                    RunMainMenu();
                    break;
                case 4:
                    Level = ChangeLevel();
                    RunMainMenu();
                    break;
                case 5:
                    ShowScoreboard(Level);
                    RunMainMenu();
                    break;
                case 6:
                    break;
                default:
                    Console.WriteLine("Błędny wybór");
                    break;
            }

        }
        private void NewGame(int _numberOfLetters, int level)
        {
            
            Console.Clear();
            var correctWord = WordManager.DrawWord(_numberOfLetters);//losowanie słowa o wybranej liczbie liter
            var word = default(string);//Zmienna do przyjmowania słowa od gracza
            Stopwatch time = new Stopwatch();
            time.Start();//Rozpoczęcie odliczania czasu

            Console.WriteLine("Podaj słowo:");
            do
            {
                
                word = Console.ReadLine();
                word = word.ToLower();//zamiana słowa na małe litery(gdyby gracz wpisywał słowo dużymi)
                if (word.Length != _numberOfLetters)//jeśli wpisane słowo będzie miało nieprawidłową liczbę liter
                {
                    
                    Console.WriteLine("Musisz podać słowo " + _numberOfLetters + " literowe.");
                }
                else
                {
                    var result = CheckLetters(word, correctWord);//funkcja zwróci tablicę enum która pokazuje stan
                                                                 //każdej litery w danym słowie
                    DisplayResult(result, word);//funkcja odpowiada za graficzne przedstawienie słowa
                                                //(na zielono dla prawidłowej litery i na żółto dla innej pozycji)
                    if (level != -1)
                    {
                        level--;
                        Console.WriteLine("                  *Pozostała liczba szans: " + level);
                    }
                    else
                        Console.WriteLine();
                }
               
            }
            while (correctWord != word && level != 0);//Gra kończy się gdy gracz zgadnie słowo, lub skończą się szanse
            if (correctWord == word)
            {
                time.Stop();
                var ts = Math.Round(time.Elapsed.TotalSeconds, 2);
                Console.WriteLine(@"
                    ____  ____    ____  ______  __ __  _       ____    __  ____    ___  __ 
                   /    ||    \  /    ||      ||  |  || |     /    |  /  ]|    |  /  _]|  |
                  |   __||  D  )|  o  ||      ||  |  || |    |  o  | /  / |__  | /  [_ |  |
                  |  |  ||    / |     ||_|  |_||  |  || |___ |     |/  /  __|  ||    _]|__|
                  |  |_ ||    \ |  _  |  |  |  |  :  ||     ||  _  /   \_/  |  ||   [_  __ 
                  |     ||  .  \|  |  |  |  |  |     ||     ||  |  \     \  `  ||     ||  |
                  |___,_||__|\_||__|__|  |__|   \__,_||_____||__|__|\____|\____j|_____||__|");
                Console.WriteLine("Twój czas: " + ts.ToString() + "s");

                if (level != -1)
                {
                    scoreboard.Add(ts);
                    scoreboard.Sort();
                    if (scoreboard.Count > 5 )//5 najlepszych wyników
                    {
                        scoreboard.RemoveAt(5);
                    }
                }

            }
            else
            {
                Console.WriteLine("Niestety, poległeś...");
                Console.WriteLine("Prawidłowe słowo to " + correctWord);
            }

            Console.ReadKey();
            Console.Clear();
            
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

        public LetterStatus[] CheckLetters(string _word, string correctWord)
        {
            var result = new LetterStatus[correctWord.Length];
            var tempcorrectWord = correctWord;
            for (int i = 0; i < correctWord.Length; i++)
            {
                if (_word[i] == correctWord[i])
                {
                    result[i] = LetterStatus.Correct;
                    tempcorrectWord = tempcorrectWord.Substring(0, i) + '`' + tempcorrectWord.Substring(i + 1);
                }
            }
            for (int i = 0; i < correctWord.Length; i++)
                {
                    if(!(result[i] == LetterStatus.Correct))
                {
                    if (tempcorrectWord.Contains(_word[i]))
                    {
                        int tempIndex = tempcorrectWord.IndexOf(_word[i]);
                        result[i] = LetterStatus.WrongPosition;
                        tempcorrectWord = tempcorrectWord.Substring(0, tempIndex) + '`' + tempcorrectWord.Substring(tempIndex + 1);
                    }
                    else
                    {
                        result[i] = LetterStatus.Wrong;
                    }
                }
               /* else if (tempcorrectWord.Contains(_word[i])){ 
                    result[i] = LetterStatus.WrongPosition;
                }
                else
                {
                    result[i] = LetterStatus.Wrong;
                }*/

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
            changeColorDefault();
            
            
        }
        private void AddWord()
        {
            Console.Clear();
            var word = default(string);
            
            do
            {
                Console.Clear();
                Console.WriteLine("Podaj słowo, które chcesz dodać do bazy");
                word = Console.ReadLine();
                word=word.ToLower();
                if(!(word.Length == 4 || word.Length == 5 || word.Length == 6))
                {
                    Console.WriteLine("Musisz podać słowo 4, 5 lub 6 literowe");
                    Console.ReadKey();
                }
            }
            while (!(word.Length == 4 || word.Length == 5 || word.Length == 6));

            if (WordManager.IfWordExist(word))
            {
                Console.WriteLine("Podane słowo już istnieje w bazie!");
            }
            else
            {
                WordManager.SaveWord(word);
                Console.WriteLine("Dodano słowo do bazy");
            }
            Console.ReadKey();
            Console.Clear();
        }
        private void DeleteWord()
        {
            Console.Clear();
            string prompt = "Wybierz słowo, jakie chcesz usunąć:";
            var wordsList = new List<string>();
            for(int i = 0; i < WordManager.Words.Count(); i++)
            {
                wordsList.Add(WordManager.Words[i].Name);
            }
            var removeWordMenu = new Menu(prompt, wordsList);

            var selectedIndex = removeWordMenu.Run();
            if (WordManager.canRemove(WordManager.Words[selectedIndex].Name.Length))
            WordManager.RemoveWord(selectedIndex);
            else
            {
                string warning = "Nie możesz usunąć tego słowa!";
                Console.SetCursorPosition((Console.WindowWidth - warning.Length) / 2, Console.CursorTop);
                Console.WriteLine(warning);
                Console.ReadKey();
            }
                

        }
        private int ChangeNumberOfLetters()
        {
            Console.Clear();
            string prompt = "Wybierz ilu literowe słowo chcesz zgadywać:";
            var optionsList = new List<string>()
            {
                " Czteroliterowe ",
                " Pięcioliterowe ",
                " Sześcioliterowe",
            };
            var difficultyMenu = new Menu(prompt, optionsList);

            var selectedIndex = difficultyMenu.Run();
            switch (selectedIndex)
            {
                case 0:
                    return 4;
                    break;
                case 1:
                    return 5;
                    break;
                case 2:
                    return 6;
                    break;
                default:
                    Console.WriteLine("Błędny wybór");
                    return 0;
                    break;
            }

        }
        private int ChangeLevel()
        {
            Console.Clear();
            var prompt = "Wybierz ile chcesz mieć szans na zgadnięcie:";
            var optionsList = new List<string>()
            {
                "  Sześć   ",
                "  Osiem   ",
                " Dziesięć ",
                "Bez limitu",
            };
            var difficultyMenu = new Menu(prompt, optionsList);

            var selectedIndex = difficultyMenu.Run();
            switch (selectedIndex)
            {
                case 0:
                    return 6;
                    break;
                case 1:
                    return 8;
                    break;
                case 2:
                    return 10;
                    break;
                case 3:
                    return -1;
                    break;
                default:
                    Console.WriteLine("Błędny wybór");
                    return 0;
                    break;
            }
        }

        private void ShowScoreboard(int level)
        {
            Console.Clear();
            string tw = "NAJLEPSZE WYNIKI:";
                int x = 5;
            Console.SetCursorPosition((Console.WindowWidth - tw.Length) / 2, Console.CursorTop + x);
            Console.WriteLine(tw);
            for (int i = 0; i < scoreboard.Count; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth - tw.Length) / 2, Console.CursorTop);
                Console.WriteLine(i + 1 + ". " + scoreboard[i] + "s");
            }
            Console.ReadKey();
        }

    }
}
