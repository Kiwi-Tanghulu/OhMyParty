using Unity.Collections;

namespace OMG.NetworkEvents
{
    internal interface INetworkEvent
    {
        internal FixedString128Bytes EventID { get; }
        internal void Invoke(NetworkEventParams eventParams);
    }
}
