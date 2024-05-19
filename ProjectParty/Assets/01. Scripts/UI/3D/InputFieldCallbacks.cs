using TinyGiantStudio.Text;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.UI.Solid
{
    [RequireComponent(typeof(InputField))]
    public class InputFieldCallbacks : MonoBehaviour
    {
        [SerializeField] UnityEvent<string> OnInputEndEvent = null;
        private InputField inputField = null;

        private void Awake()
        {
            inputField = GetComponent<InputField>();
            inputField.onInputEnd.AddListener(HandleInputEnd);
        }

        private void OnDestroy()
        {
            inputField.onInputEnd.RemoveListener(HandleInputEnd);
        }

        private void HandleInputEnd()
        {
            OnInputEndEvent?.Invoke(inputField.Text);
        }
    }
}
