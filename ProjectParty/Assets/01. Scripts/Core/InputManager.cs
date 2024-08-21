using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

namespace OMG.Inputs
{
    public static class InputManager
    {
        public static Controls controls { get; private set; }
        private static Dictionary<InputMapType, InputActionMap> inputMapDic;
        private static InputMapType currentInputMapType;
        private static InputMapType prevInputMapType;
        public static InputMapType CurrentInputMapType => currentInputMapType;
        public static InputMapType PrevInputMapType => prevInputMapType;

        private static bool enable = true;
        public static bool Enable => enable;

        static InputManager()
        {
            controls = new Controls();
            inputMapDic = new Dictionary<InputMapType, InputActionMap>();
        }

        public static void RegistInputMap(InputSO inputSO, InputActionMap actionMap)
        {
            inputMapDic[inputSO.inputMapType] = actionMap;
            actionMap.Disable();
        }

        public static void ChangeInputMap(InputMapType inputMapType)
        {
            if (inputMapDic.ContainsKey(inputMapType) == false)
                return;
            if (inputMapDic[inputMapType] == null)
                return;

            if (inputMapDic.ContainsKey(currentInputMapType))
                inputMapDic[currentInputMapType]?.Disable();
            prevInputMapType = currentInputMapType;
            currentInputMapType = inputMapType;
            inputMapDic[currentInputMapType].Enable();

            SetInputEnable(enable);

            Debug.Log($"change input map : {currentInputMapType}");
        }

        public static void UndoChangeInputMap()
        {
            ChangeInputMap(prevInputMapType);
        }

        public static void SetInputEnable(bool value)
        {
            if(value)
                inputMapDic[currentInputMapType]?.Enable();
            else
                inputMapDic[currentInputMapType]?.Disable();

            enable = value;
        }
    }

}

