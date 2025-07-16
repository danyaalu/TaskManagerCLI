using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Manager.Classes
{
    internal class TaskManager
    {
        static List<TaskItem> tasks = new List<TaskItem>();
        static int maxAmountOfTasks = 5;
        public static void AddTask()
        {
            string name, description;
            bool isTaskValid = true;

            if (tasks.Count >= maxAmountOfTasks)
            {
                Console.Clear();
                Console.WriteLine("===== Add a task =====\n");

                Console.WriteLine("You have the max amount of tasks, remove or complete one and try again.");
                Console.ReadKey(true);
            }
            else
            {
                do
                {
                    isTaskValid = true;

                    Console.Clear();
                    Console.WriteLine("===== Add a task =====\n");

                    Console.Write("Name: ");
                    name = Console.ReadLine();

                    Console.Write("Description: ");
                    description = Console.ReadLine();

                    Console.Clear();
                    Console.WriteLine("===== Add a task =====\n");

                    if (string.IsNullOrWhiteSpace(name) == true || string.IsNullOrWhiteSpace(description) == true)
                    {
                        isTaskValid = false;
                        Console.WriteLine("Inputs cannot be empty, press any key to try again...");
                        Console.ReadKey(true);
                    }
                    else
                    {
                        for (int i = 0; i < tasks.Count; i++)
                        {
                            var task = tasks[i];

                            if (task.Name == name)
                            {
                                isTaskValid = false;

                                Console.WriteLine("Task name has already been used, press any key to try again");
                                Console.ReadKey(true);
                                break;
                            }
                        }
                    }

                    if (isTaskValid)
                    {
                        Console.WriteLine("Adding task to list...");
                        tasks.Add(new TaskItem(name, description));

                        Console.WriteLine("Press any key to return back to the menu...");
                        Console.ReadKey(true);
                    }
                } while (!isTaskValid);
            }
        }
        public static void ViewTask()
        {
            Console.WriteLine("===== Viewing task list =====\n");

            if (tasks == null || tasks.Count == 0)
            {
                Console.WriteLine("No tasks found, press any key to return to menu");
                Console.ReadKey(true);
            }
            else
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    var task = tasks[i];

                    Console.WriteLine($"[{i + 1}] Name: {task.Name}");
                    Console.WriteLine($"    Description: {task.Description}\n");
                    Console.WriteLine($"{new string('-', 40)}\n");
                }
            }

            Console.WriteLine("Press any key to return to menu");
            Console.ReadKey(true);
        }
        public static void EditTask()
        {
            int selectedIndex = 0;
            ConsoleKey key;
            bool isTaskValid;

            do
            {
                isTaskValid = true;
                do
                {
                    Console.Clear();
                    Console.WriteLine("===== Edit a task =====\n");

                    for (int i = 0; i < tasks.Count; i++)
                    {
                        if (selectedIndex == i)
                        {
                            Console.Write("> ");
                        }
                        else
                        {
                            Console.Write("  ");
                        }

                        var task = tasks[i];

                        Console.WriteLine($"[{i + 1}] Name: {task.Name}");
                        Console.WriteLine($"    Description: {task.Description}\n");
                        Console.WriteLine($"{new string('-', 40)}\n");
                    }


                    // Read a key without echoing into console
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    key = keyInfo.Key;

                    if (key == ConsoleKey.UpArrow)
                    {
                        if (selectedIndex == 0)
                        {
                            selectedIndex = tasks.Count - 1;
                        }
                        else
                        {
                            selectedIndex--;
                        }
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        if (selectedIndex == tasks.Count - 1)
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
                Console.WriteLine("===== Edit a task =====\n");

                Console.WriteLine($"Old name: {tasks[selectedIndex].Name}");
                Console.Write("New name: ");
                string name = Console.ReadLine();

                Console.WriteLine($"\nOld description: {tasks[selectedIndex].Description}");
                Console.Write("New description: ");
                string description = Console.ReadLine();

                Console.Clear();
                Console.WriteLine("===== Edit a task =====\n");

                // TODO: Rebuild validation logic

                for (int i = 0; i < tasks.Count; i++)
                {
                    if (i != selectedIndex)
                    {
                        var task = tasks[i];

                        if (task.Name == name)
                        {
                            isTaskValid = false;

                            Console.WriteLine("Task name has already been used, press any key to try again");
                            Console.ReadKey(true);
                            break;
                        }
                    }
                }

                if (isTaskValid)
                {
                    if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(description))
                    {
                        Console.WriteLine("No changes were made, press any key to return to menu");
                        Console.ReadKey(true);
                    }
                    else if (name != tasks[selectedIndex].Name && string.IsNullOrWhiteSpace(description))
                    {
                        tasks[selectedIndex].Name = name;

                        Console.WriteLine("Name has been changed, press any key to return to menu");
                        Console.ReadKey(true);
                    }
                    else if (string.IsNullOrWhiteSpace(name) && description != tasks[selectedIndex].Description)
                    {
                        tasks[selectedIndex].Description = description;

                        Console.WriteLine("Description has been changed, press any key to return to menu");
                        Console.ReadKey(true);
                    }
                    else if (name != tasks[selectedIndex].Name && description != tasks[selectedIndex].Description)
                    {
                        tasks[selectedIndex].Name = name;
                        tasks[selectedIndex].Description = description;

                        Console.WriteLine("Name and description has been changed, press any key to return to menu");
                        Console.ReadKey(true);
                    }
                }
            } while (!isTaskValid);
        }
        public static void DeleteTask()
        {
            Console.WriteLine("4");
        }

        // Create helper function

        private static bool ValidateTaskInput(string name, string description, int selectedIndex)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Inputs cannot be empty, press any key to try again");
                Console.ReadKey(true);
                return false;
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                if (i == selectedIndex) continue;

                var task = tasks[selectedIndex];
                if (task.Name.ToLower() == name.ToLower())
                {
                    Console.WriteLine("Task name has already been used, press any key to try again");
                    Console.ReadKey(true);
                    return false;
                }
            }
            return true;
        }
    }
}