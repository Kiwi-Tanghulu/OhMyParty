using OMG.Datas;
using UnityEngine;

namespace OMG.Test
{
    public class TDataManager : MonoBehaviour
    {
        public UserData UserData = null;

        private void Awake()
        {
            DataManager.LoadData();
            UserData = DataManager.UserData;
        }

        private void Update()
        {
            if(UnityEngine.Input.GetKey(KeyCode.LeftControl))
            {
                if(UnityEngine.Input.GetKey(KeyCode.LeftShift))
                {
                    if(UnityEngine.Input.GetKeyDown(KeyCode.R))
                    {
                        DataManager.ClearData();
                        DataManager.LoadData();
                    }
                }
            }
        }

        private void OnDestroy()
        {
            DataManager.SaveData();
        }
    }
}
