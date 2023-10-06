using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace PocketZone.SaveLoad
{
    public class JsonDataService
    {
        public bool Save<T>(string relativePath, T data)
        {
            string path = Application.persistentDataPath + relativePath;

            try
            {
                if (File.Exists(path))
                {
                    Debug.Log("Data exists. Deleting old file and writing a new one.");
                    File.Delete(path);
                }
                else
                {
                    Debug.Log("Writing file for the first time");
                }

                using FileStream stream = File.Create(path);
                stream.Close();
                File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented));

                return true;
            }
            catch (IOException e)
            {
                Debug.LogError($"Unable to save data due to: {e.Message} {e.StackTrace}");
                return false;
            }
        }

        public T Load<T>(string relativePath)
        {
            string path = Application.persistentDataPath + relativePath;

            if (File.Exists(path) == false)
            {
                Debug.LogError($"Cannot load file at {path}. File does not exist");
                throw new FileNotFoundException($"{path} does not exist");
            }

            try
            {
                var data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                return data;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load data due to: {e.Message} {e.StackTrace}");
                throw;
            }
        }
    }
}