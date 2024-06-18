using UnityEngine.Events;

namespace OMG.Items
{
    [System.Serializable]
    public class NetworkItemEvent
    {
        public string EventName;
        public UnityEvent Event;
    }
}
