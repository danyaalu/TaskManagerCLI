using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }
        
        static void Menu()
        {
            string[] options = { "Add a task", "View the task list", "Edit a task", "Delete a task" };

            int selectedIndex = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();

                // Drawing options, showing ">" for selected option
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.Write($"> ");
                    }
                    else
                    {
                        Console.Write($"  ");
                    }
                    Console.WriteLine(options[i]);
                }

                // Read a key without echoing into console
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                key = keyInfo.Key;
                

                if (key == ConsoleKey.UpArrow)
                {
                    if (selectedIndex == 0)
                    {
                        selectedIndex = options.Length - 1;
                    }
                    else
                    {
                        selectedIndex--;
                    }
                }
                else if (key == ConsoleKey.DownArrow) 
                {
                    if (selectedIndex == options.Length - 1)
                    {
                        selectedIndex = 0;
                    }
                    else
                    {
                        selectedIndex++;
                    }
                }
            } while (key != ConsoleKey.Enter);
        }
    }
}
