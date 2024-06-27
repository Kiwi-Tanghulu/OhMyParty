using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }

        private CinemachineBrain _cinemachineBrain;
        private CinemachineBrain cinemachineBrain
        {
            get
            {
                if(_cinemachineBrain == null)
                    _cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();

                return _cinemachineBrain;
            }

            set
            {
                _cinemachineBrain = value;
            }
        }

        private CinemachineVirtualCamera currentCam;
        private CinemachineVirtualCamera prevCam;

        public static bool CreateSingleton(GameObject gameObject)
        {
            if (Instance != null)
                return false;

            CameraManager instance = gameObject.AddComponent<CameraManager>();
            Instance = instance;

            return true;
        }

        public void ChangeCamera(CinemachineVirtualCamera cam, float transitionTime = 0f, Action OnStartEvent = null, Action OnEndEvent = null)
        {
            if (cam == null)
            {
                Debug.LogError("camera is null");

                return;
            }

            cinemachineBrain.m_DefaultBlend.m_Time = transitionTime;
            OnStartEvent?.Invoke();

            prevCam = currentCam;
            currentCam = cam;

            if (prevCam != null)
                prevCam.Priority = DEFINE.UNFOCUSED_PRIORITY;
            currentCam.Priority = DEFINE.FOCUSED_PRIORITY;

            StopAllCoroutines();
            StartCoroutine(DelayAction(transitionTime, OnEndEvent));
        }

        public void ChangePrevCam(float transitionTime = 0f, Action OnStartEvent = null, Action OnEndEvent = null)
        {
            if (prevCam == null)
                return;

            ChangeCamera(prevCam, transitionTime, OnStartEvent, OnEndEvent);
        }

        private IEnumerator DelayAction(float delayTime, Action action)
        {
            yield return new WaitForSeconds(delayTime);

            action?.Invoke();
        }
    }
}