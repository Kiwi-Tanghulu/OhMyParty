namespace OMG.NetworkEvents
{
    internal interface INetworkEvent
    {
        internal ulong EventID { get; }
        internal void Invoke(NetworkEventParams eventParams);
    }
}
