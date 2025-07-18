using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Task_Manager.Classes
{
    internal class FileManager
    {
        private const string filePath = "data/tasks.json";

        public static List<TaskItem> LoadFile(out string errorMsg)
        {
            List<TaskItem> tasks = new List<TaskItem>();            
            errorMsg = "";
            
            if(!File.Exists(filePath))
            {
                return tasks;   
            }

            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Length == 0)
                {
                    return tasks;
                }

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string json = sr.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(json) || json == "[]")
                    {
                        return tasks;
                    }

                    tasks = JsonSerializer.Deserialize<List<TaskItem>>(json);
                }

                if (tasks == null)
                {
                    tasks = new List<TaskItem>();
                    errorMsg = "Failed to parse task data (null result)";
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return tasks;
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
                using (StreamWriter sw = new StreamWriter(filePath))
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
