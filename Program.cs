using System;
using Task_Manager.Classes;

namespace Task_Manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static int ShowMenu(string[] options)
        {
            int selectedIndex = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine($"===== Task Manager =====\n");

                // Drawing options, showing ">" for selected option
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.Write("  ");
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

            Console.Clear();
            return selectedIndex;
        }

        static void Menu()
        {
            string[] options = { "Add a task", "View the task list", "Edit a task", "Delete a task", "Exit" };

            while (true)
            {
                int menuChoice = ShowMenu(options);

                switch (menuChoice)
                {
                    case 0:
                        TaskManager.AddTask();
                        break;
                    case 1:
                        TaskManager.ViewTask();
                        break;
                    case 2:
                        TaskManager.EditTask();
                        break;
                    case 3:
                        TaskManager.DeleteTask();
                        break;
                    case 4:
                        return;
                }
            }
        }
    }
}
