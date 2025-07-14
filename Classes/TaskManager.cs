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
        public static void AddTask()
        {
            string name, description;

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
        public static void ViewTask()
        {
            Console.WriteLine("2");
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
