using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
            string prompt = "Witaj w grze!";
            string[] options = { "Nowa gra", "Zmien liczbe liter", "Stworz wlasne slowo", "Koniec gry" };
            Menu mainMenu = new Menu(prompt, options);
            //mainMenu.Display();
            int selectedIndex = mainMenu.Run();
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
            }
            
        }
        private void NewGame()
        {

            string test = "test";
            string word;
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
