using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Task_Manager.Classes
{
    internal class FileManager
    {
        private const string filePath = "data/tasks.json";
        public static List<TaskItem> LoadFile(out string errorMsg)
        {
            errorMsg = "";
            List<TaskItem> tasks = new List<TaskItem>();

            if (!File.Exists(filePath))
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
                    if (string.IsNullOrWhiteSpace(json))
                    {
                        return tasks;
                    }
                    var deserialised = JsonSerializer.Deserialize<List<TaskItem>>(json);
                    if (deserialised != null)
                    {
                        tasks = deserialised;
                    }
                }
            }
            catch (JsonException ex)
            {
                errorMsg = $"Invalid JSON format: {ex.Message}";
            }
            catch (IOException ex)
            {
                errorMsg = $"File access error: {ex.Message}";
            }
            catch (UnauthorizedAccessException ex)
            {
                errorMsg = $"Permission denied: {ex.Message}";
            }
            catch (Exception ex)
            {
                errorMsg = $"Unexpected error: {ex.Message}";
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
