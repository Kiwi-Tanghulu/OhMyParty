using Unity.Netcode;

namespace OMG.Timers
{
    public struct TimerValue : INetworkSerializable
    {
        public float ratio;
        public float single;

        public TimerValue(float ratio, float single)
        {
            this.ratio = ratio;
            this.single = single;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref ratio);
            serializer.SerializeValue(ref single);
        }
    }
}