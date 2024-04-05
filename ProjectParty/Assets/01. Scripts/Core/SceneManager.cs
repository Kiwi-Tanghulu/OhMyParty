using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using LoadSceneMode = UnityEngine.SceneManagement.LoadSceneMode;

namespace OMG
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance = null;

        private bool callbackPostpone = false;
        private Action loadSceneCallbackBuffer = null;
        
        private void Start()
        {
            NetworkManager.Singleton.OnServerStarted += SubscribeSceneCallback;
        }

        public void LoadSceneAsync(SceneType sceneType, bool postponeOneFrame = false, Action callback = null)
            => LoadSceneAsync(sceneType.ToString(), postponeOneFrame, callback);

        public void LoadSceneAsync(string sceneName, bool postponeOneFrame = false, Action callback = null)
        {
            if(NetworkManager.Singleton.IsHost == false)
                return;

            callbackPostpone = postponeOneFrame;
            loadSceneCallbackBuffer = callback;

            NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        private void HandleLoadEventCompleted(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
        {
            if(loadSceneCallbackBuffer == null)
                return;

            if(callbackPostpone)
                StartCoroutine(this.PostponeFrameCoroutine(InvokeDisposableCallback));
            else
                InvokeDisposableCallback();
        }

        private void InvokeDisposableCallback()
        {
            loadSceneCallbackBuffer?.Invoke();
            loadSceneCallbackBuffer = null;

            callbackPostpone = false;
        }
        
        private void SubscribeSceneCallback()
        {
            NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += HandleLoadEventCompleted;
        }
    }
}
