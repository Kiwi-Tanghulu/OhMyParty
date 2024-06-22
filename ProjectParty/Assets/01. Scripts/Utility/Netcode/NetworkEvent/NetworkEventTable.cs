using System.Collections.Generic;
using Unity.Collections;

namespace OMG.NetworkEvents
{
    internal static class NetworkEventTable
    {
        private static Dictionary<ulong, Dictionary<FixedString128Bytes, INetworkEvent>> eventTable;

        static NetworkEventTable()
        {
            eventTable = new Dictionary<ulong, Dictionary<FixedString128Bytes, INetworkEvent>>();
        }

        public static INetworkEvent GetEvent(ulong instanceID, FixedString128Bytes eventID) => eventTable[instanceID][eventID];

        public static void RegisterEvent(ulong instanceID, INetworkEvent networkEvent)
        {
            if(eventTable.ContainsKey(instanceID) == false)
                eventTable.Add(instanceID, new Dictionary<FixedString128Bytes, INetworkEvent>());

            eventTable[instanceID][networkEvent.EventID] = networkEvent;
        }

        public static void UnregisterEvent(ulong instanceID, INetworkEvent networkEvent)
        {
            if(eventTable.ContainsKey(instanceID) == false)
                return;

            eventTable[instanceID].Remove(networkEvent.EventID);
        }
    }
}
