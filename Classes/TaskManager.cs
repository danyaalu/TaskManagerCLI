using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Manager.Classes
{
    internal class TaskManager
    {
        private static List<TaskItem> _tasks = new List<TaskItem>();
        private const int MaxAmountOfTasks = 5;
        public static void AddTask()
        {
            if (_tasks.Count >= MaxAmountOfTasks)
            {
                Console.Clear();
                Console.WriteLine("===== Add a task =====\n");
                Console.WriteLine("You have the max amount of tasks, remove or complete one and try again.");
                Console.ReadKey(true);
                return;
            }

            string name, description;
            bool isTaskValid = false; ;
            do
            {
                Console.Clear();
                Console.WriteLine("===== Add a task =====\n");

                Console.Write("Name: ");
                name = Console.ReadLine();

                Console.Write("Description: ");
                description = Console.ReadLine();

                Console.Clear();
                Console.WriteLine("===== Add a task =====\n");

                isTaskValid = ValidateTaskInput(name, description);
                if (isTaskValid)
                {
                    _tasks.Add(new TaskItem(name, description));
                    Console.WriteLine("Added new task to list... press any key to return to menu");
                    Console.ReadKey(true);
                }
            } while (!isTaskValid);
        }
        public static void ViewTask()
        {
            string title = "Viewing task list";
            Console.WriteLine($"===== {title} =====\n");
            if (_tasks == null || _tasks.Count == 0)
            {
                Console.WriteLine("No tasks found, press any key to return to menu");
                Console.ReadKey(true);
                return;
            }
            else
            {
                DisplayTaskList(title);
                Console.WriteLine("Press any key to return to menu");
                Console.ReadKey(true);
            }
        }
        public static void EditTask()
        {
            int selectedIndex = 0;
            ConsoleKey key;
            string title = "Edit a task";

            if (_tasks.Count == 0)
            {
                Console.WriteLine($"===== {title} =====\n");
                Console.WriteLine("No tasks to edit, press any key to return to menu");
                Console.ReadKey(true);
                return;
            }

            bool isTaskValid = false;
            do
            {
                do
                {
                    DisplayTaskList(title, selectedIndex);

                    // Read a key without echoing into console
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    key = keyInfo.Key;

                    if (key == ConsoleKey.UpArrow)
                    {
                        if (selectedIndex == 0)
                        {
                            selectedIndex = _tasks.Count - 1;
                        }
                        else
                        {
                            selectedIndex--;
                        }
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        if (selectedIndex == _tasks.Count - 1)
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

                Console.WriteLine($"Old name: {_tasks[selectedIndex].Name}");
                Console.Write("New name: ");
                string name = Console.ReadLine();

                Console.WriteLine($"\nOld description: {_tasks[selectedIndex].Description}");
                Console.Write("New description: ");
                string description = Console.ReadLine();

                Console.Clear();
                Console.WriteLine("===== Edit a task =====\n");

                // Use old values if left blank
                string newName = string.IsNullOrWhiteSpace(name) ? _tasks[selectedIndex].Name : name;
                string newDescription = string.IsNullOrWhiteSpace(description) ? _tasks[selectedIndex].Description : description;

                if (newName == _tasks[selectedIndex].Name && newDescription == _tasks[selectedIndex].Description)
                {
                    Console.WriteLine("No changes were made, press any key to return to menu");
                    Console.ReadKey(true);
                    return;
                }

                isTaskValid = ValidateTaskInput(newName, newDescription, selectedIndex);
                if (isTaskValid)
                {
                    _tasks[selectedIndex].Name = newName;
                    _tasks[selectedIndex].Description = newDescription;
                    Console.WriteLine("Task updated, press any key to return to menu");
                    Console.ReadKey(true);
                }
            } while (!isTaskValid);
        }
        public static void DeleteTask()
        {
            int selectedIndex = 0;
            ConsoleKey key;

            if (_tasks.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("===== Delete a task =====\n");
                Console.WriteLine("No tasks to delete, press any key to return to menu");
                Console.ReadKey(true);
                return;
            }

            do
            {
                Console.Clear();
                Console.WriteLine("===== Delete a task =====\n");

                for (int i = 0; i < _tasks.Count; i++)
                {
                    if (selectedIndex == i)
                    {
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }

                    var task = _tasks[i];
                    Console.WriteLine($"[{i + 1}] Name: {task.Name}");
                    Console.WriteLine($"      Description: {task.Description}\n");
                    Console.WriteLine($"{new string('-', 40)}\n");
                }

                // Read a key without echoing into console
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                key = keyInfo.Key;

                if (key == ConsoleKey.UpArrow)
                {
                    if (selectedIndex == 0)
                    {
                        selectedIndex = _tasks.Count - 1;
                    }
                    else
                    {
                        selectedIndex--;
                    }
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    if (selectedIndex == _tasks.Count - 1)
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
            Console.WriteLine("===== Delete a task =====\n");
            _tasks.RemoveAt(selectedIndex);
            Console.WriteLine("Task removed successfully, press any key to return to menu");
            Console.ReadKey(true);
        }
        private static bool ValidateTaskInput(string name, string description, int selectedIndex = -1)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Inputs cannot be empty, press any key to try again");
                Console.ReadKey(true);
                return false;
            }

            for (int i = 0; i < _tasks.Count; i++)
            {
                if (i == selectedIndex) continue;

                if (string.Equals(_tasks[i].Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Task name has already been used, press any key to try again");
                    Console.ReadKey(true);
                    return false;
                }
            }
            return true;
        }
        private static void DisplayTaskList(string title, int selectedIndex = -1)
        {
            Console.Clear();
            Console.WriteLine($"===== {title} =====\n");

            for (int i = 0; i < _tasks.Count; i++)
            {
                if (selectedIndex >= 0)
                {
                    Console.Write(selectedIndex == i ? "> " : "  ");
                }

                var task = _tasks[i];
                Console.WriteLine($"[{i + 1}] Name: {task.Name}");
                Console.WriteLine(selectedIndex >= 0 ? $"      Description: {task.Description}\n" : $"    Description: {task.Description}\n");
                Console.WriteLine($"{new string('-', 40)}\n");
            }
        }

    }
}