using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace OMG.Datas
{
    public static class DataManager
    {
        public static UserData UserData = null;
        private const string DATA_FILE_NAME = "UserData.data";

        private static string fullPath => Path.Combine(Application.persistentDataPath, DATA_FILE_NAME);

        public static void LoadData()
        {
            if (File.Exists(fullPath))
            {
                try
                {
                    string dataToLoad = "";
                    using (FileStream readStream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(readStream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    UserData = JsonConvert.DeserializeObject<UserData>(dataToLoad);
                    if(UserData == null)
                        CreateData();
                }
                catch (Exception err)
                {
                    Debug.Log($"error with loading data : {err.Message}");
                }
            }
            else
                CreateData();
        }

        public static void SaveData()
        {
            try
            {
                Directory.CreateDirectory(Application.persistentDataPath);
                string dataToStore = JsonConvert.SerializeObject(UserData);

                using (FileStream writeStream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(writeStream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception err)
            {
                Debug.Log($"error with saving data : {err.Message}");
            }
        }

        public static void ClearData()
        {
            if (File.Exists(fullPath))
            {
                try
                {
                    File.Delete(fullPath);
                }
                catch (Exception err)
                {
                    Debug.Log($"errror with deleting data file : {err.Message}");
                }
            }
        }

        private static void CreateData()
        {
            UserData = new UserData();
            UserData.CreateData();
        }
    }
}