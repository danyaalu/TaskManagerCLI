using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Task_Manager.Classes
{
    internal class FileManager
    {
        public static List<TaskItem> LoadFile()
        {
            List<TaskItem> tasks = new List<TaskItem>();

            using (StreamReader sr = new StreamReader("data/tasks.json"))
            {
                string json = sr.ReadToEnd();
                tasks = JsonSerializer.Deserialize<List<TaskItem>>(json);
            }

            return tasks;
        }   
        public static bool SaveFile(List<TaskItem> tasks, out string errorMsg)
        {
            errorMsg = "";
            string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            try
            {
                Directory.CreateDirectory("data");
                using (StreamWriter sw = new StreamWriter("data/tasks.json"))
                {
                    sw.Write(json);
                }
            }
            catch (Exception ex)    
            {
                errorMsg = ex.Message;
                return false;
            }

            return true;
        }
    }
}
