using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kck
{
    public class Menu
    {
        private int SelectedIndex;
        private string[] options;
        private string Prompt;
        public Menu(string prompt, string[] options)
        {
            SelectedIndex = 0;
            this.options = options;
            Prompt = prompt;
        }

        private void Display()
        {
            Console.WriteLine(Prompt);
            for(int i=0; i < options.Length; i++)
            {
                string currentOption = options[i];
                string arrow;
                if(i == SelectedIndex)
                {
                    arrow = "->";
                    Console.ForegroundColor =ConsoleColor.Black;
                    Console.BackgroundColor= ConsoleColor.White;
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
            ConsoleKey keyPressed;
            do
            {
                Console.SetCursorPosition(0, 0);
                Display();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                //Update Selected Index based on arrow keys
                if(keyPressed == ConsoleKey.UpArrow)
                {
                    if(!(SelectedIndex ==0))
                    SelectedIndex--;
                }
                else if(keyPressed == ConsoleKey.DownArrow)
                {
                    if(!(SelectedIndex == options.Length-1))
                    SelectedIndex++;
                }
            }


            while (keyPressed != ConsoleKey.Enter);
            return SelectedIndex;
        }
        /*public int length = 5;
        string strzalka = "->";
        string gumka = "  ";
        public void showMenu()
        {
            Console.WriteLine("Witam Studenta!");
            string[] menu = new string[] { "1. Nowa gra", "2. Wybór liczby liter", "3. Koniec gry" };
            Console.WriteLine("   1. Nowa gra");
            Console.WriteLine("   2. Wybór liczby liter");
            Console.WriteLine("   3. Koniec gry");
            int x = 1;
            int y = 1;
            string strzalka = "->";
            string gumka = "  ";
            bool shiet = true;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(strzalka);

            while (shiet)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (y < 3)
                        {
                            RemoveArrow(x, y);
                            ///Console.SetCursorPosition(x, y);
                            //Console.WriteLine(gumka);
                            
                            DrawArrow(x, ++y);
                            //Console.SetCursorPosition(x, y);
                            //Console.WriteLine(strzalka);
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (y > 1)
                        {

                            //Console.SetCursorPosition(x, y);
                            //Console.WriteLine(gumka);
                            //y--;
                            RemoveArrow(x, y);
                            //Console.SetCursorPosition(x, y);
                            //Console.WriteLine(strzalka);
                            
                            DrawArrow(x, --y);
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        
                        //Console.WriteLine("Wybrales opcje: " + menu[y - 1]);
                        if (y == 3)
                        {
                            shiet = false;
                            break;
                        }
                        else if (y == 2)
                        {
                            y = 1;
                            Console.WriteLine("Wybierz liczbę liter:");
                            Console.WriteLine("   4");
                            Console.WriteLine("   5");
                            Console.WriteLine("   6");
                            if (key.Key == ConsoleKey.Enter)
                            {
                                length = y + 3;
                            }
                            break;

                        }
                        else if (y == 1)
                        {
                            Game newGame = new Game();
                            newGame.Start();
                        }

                            y = 1;
                        
                        break;
                }
            }
        }
       
        public void DrawArrow(int _x, int _y)
        {
            //string strzalka = "->";
            Console.SetCursorPosition(_x, _y);
            Console.WriteLine(strzalka);
        }
        public void RemoveArrow(int _x, int _y)
        {
            Console.SetCursorPosition(_x, _y);
            Console.WriteLine(gumka);
        }*/
    }
}
