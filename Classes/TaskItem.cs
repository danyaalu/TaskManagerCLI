using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Task_Manager.Classes
{
    internal class TaskItem
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public TaskItem(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}