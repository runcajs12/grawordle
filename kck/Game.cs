namespace kck
{
    internal class Game
    {
       
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

            var test = "test";
            var word = default(string);
            Console.WriteLine("Podaj słowo:");
            do
            {
                
                word = Console.ReadLine();
                if(word != test)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(word[0]);
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write(word[1]);
                    
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nNie zgadles~! proboj dalej");
                }
            }
            while (word != test);
            Console.WriteLine("Zgadles");
                
            
        }
    }
}
