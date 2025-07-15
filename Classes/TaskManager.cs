using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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

            if (tasks == null | tasks.Count == 0)
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

        }
        public static void DeleteTask()
        {
            Console.WriteLine("4");
        }
    }
}