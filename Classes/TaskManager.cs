using System;
using System.Collections.Generic;
using System.Linq;
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

            Console.Clear();
            Console.WriteLine("===== Add a task =====\n");

            if (tasks.Count >= maxAmountOfTasks)
            {
                Console.WriteLine("You have the max amount of tasks, remove or complete one and try again.");
                Console.ReadKey(true);
            }
            else
            {
                Console.Write("Name: ");
                name = Console.ReadLine();

                Console.Write("Description: ");
                description = Console.ReadLine();

                Console.Clear();
                Console.WriteLine("===== Add a task =====\n");

                if (string.IsNullOrWhiteSpace(name) == true || string.IsNullOrWhiteSpace(description) == true)
                {
                    Console.WriteLine("Inputs cannot be empty, press any key to try again...");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("Adding task to list...");
                    tasks.Add(new TaskItem(name, description));

                    Console.WriteLine("Press any key to return back to the menu...");
                    Console.ReadKey(true);
                }
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
            Console.WriteLine("3");
        }
        public static void DeleteTask()
        {
            Console.WriteLine("4");
        }
    }
}