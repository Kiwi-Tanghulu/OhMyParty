using System.Collections.Generic;
using UnityEngine.InputSystem;
using static Controls;

namespace OMG.Input
{
    public static class InputManager
    {
        public static Controls controls { get; private set; }
        private static Dictionary<InputMapType, InputActionMap> inputMapDic;
        private static InputMapType currentInputMapType;

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
            if (inputMapDic.ContainsKey(currentInputMapType))
                inputMapDic[currentInputMapType]?.Disable();
            currentInputMapType = inputMapType;
            if (inputMapDic.ContainsKey(currentInputMapType))
                inputMapDic[currentInputMapType]?.Enable();
        }

        public static void SetInputEnable(bool value)
        {
            if(value)
                inputMapDic[currentInputMapType]?.Enable();
            else
                inputMapDic[currentInputMapType]?.Disable();
        }
    }

}

