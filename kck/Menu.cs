namespace kck
{
    public class Menu
    {
        
        private int _selectedIndex;
        private List<string> Options { get; set; }
        private string Prompt { get; set; }
        public Menu(string prompt, List<string> options)//Konstruktor menu pobiera napis jaki ma się znajdować
                                                        //nad menu oraz opcje możliwe do wyboru
        {
            _selectedIndex = 0;
            Options = options;
            Prompt = prompt;
        }

        private void Display()
        {
            int x = 5;
            Console.SetCursorPosition((Console.WindowWidth - Prompt.Length) / 2, Console.CursorTop+x);
            Console.WriteLine(Prompt);
            for(var i=0; i < Options.Count; i++)
            {
                var currentOption = Options[i];
                
                if(i == _selectedIndex)
                {
                    
                    Console.ForegroundColor = ConsoleColor.Black;//podświetlanie zaznaczonej opcji
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.SetCursorPosition((Console.WindowWidth - Options[0].Length) / 2, Console.CursorTop);
                Console.WriteLine(currentOption);
                
            }
            Console.ResetColor();
        }
        
        public int Run()
        {
            var keyPressed = default(ConsoleKey);
            do
            {
                Console.SetCursorPosition(0, 0);
                Display();
                var keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if(keyPressed == ConsoleKey.UpArrow)//sterowanie menu za pomocą strzałek
                {
                    if (!(_selectedIndex == 0))
                        _selectedIndex--;
                    else
                        _selectedIndex = Options.Count - 1;
                }
                else if(keyPressed == ConsoleKey.DownArrow)
                {
                    if (!(_selectedIndex == Options.Count - 1))
                        _selectedIndex++;
                    else
                        _selectedIndex = 0;
                }
            }


            while (keyPressed != ConsoleKey.Enter);//kliknięcie enter zwraca wybraną opcję
            return _selectedIndex;
        }
    }
}
