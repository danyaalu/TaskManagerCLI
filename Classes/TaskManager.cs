using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager.Classes
{
    internal class TaskManager
    {
        List<TaskItem> tasks = new List<TaskItem>();

        public static void AddTask()
        {
            Console.WriteLine("1");
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
