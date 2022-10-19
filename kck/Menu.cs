namespace kck
{
    public class Menu
    {
        
        private int _selectedIndex;
        private List<string> Options { get; set; }
        private string Prompt { get; set; }
        public Menu(string prompt, List<string> options)
        {
            _selectedIndex = 0;
            Options = options;
            Prompt = prompt;
        }

        private void Display()
        {
            
            Console.WriteLine(Prompt);
            for(var i=0; i < Options.Count; i++)
            {
                var currentOption = Options[i];
                var arrow = default(string);
                if(i == _selectedIndex)
                {
                    arrow = "->";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    arrow = "  ";
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                
                Console.WriteLine(arrow + currentOption);
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

                if(keyPressed == ConsoleKey.UpArrow)
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


            while (keyPressed != ConsoleKey.Enter);
            return _selectedIndex;
        }
    }
}
