#if UNITY_EDITOR
using Newtonsoft.Json;
using OMG.Datas;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace OMG.Utility.Animations
{
    public class AnimationRetargetor : MonoBehaviour
    {
        [SerializeField] AnimationClip targetClip;
        [SerializeField] string targetName;
        [SerializeField] string newName;

        [ContextMenu("Retargeting")]
        public void Retargeting()
        {
            if (targetClip == null)
                return;            
            if (string.IsNullOrEmpty(targetName))
                return;
            if (string.IsNullOrEmpty(newName))
                return;

            string path = AssetDatabase.GetAssetPath(targetClip);
            Debug.Log(path);
            if (File.Exists(path))
            {
                try
                {
                    string rawData = "";
                    using (FileStream readStream = new FileStream(path, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(readStream))
                        {
                            rawData = reader.ReadToEnd();
                        }
                    }
                    rawData = rawData.Replace(targetName, newName);
                    Debug.Log(rawData);
                    using (FileStream writeStream = new FileStream(path, FileMode.Create))
                    {
                        using (StreamWriter writer = new StreamWriter(writeStream))
                        {
                            writer.Write(rawData);
                        }
                    }

                    AssetDatabase.Refresh();
                }
                catch (Exception err)
                {
                    Debug.Log($"error with loading data : {err.Message}");
                }
            }
        }
    }
}
#endif
