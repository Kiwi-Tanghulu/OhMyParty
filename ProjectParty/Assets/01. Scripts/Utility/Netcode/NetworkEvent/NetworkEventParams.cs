using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace OMG.NetworkEvents
{
    public class NetworkEventParams : INetworkSerializable
    {
        public virtual void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter { }
    }

    public class IntParams : NetworkEventParams
    {
        public int Value;
        public IntParams(int value) { Value = value; }
        public override void NetworkSerialize<T>(BufferSerializer<T> serializer) { serializer.SerializeValue(ref Value); }
    }

    public class BoolParams : NetworkEventParams
    {
        public bool Value;
        public BoolParams(bool value) { Value = value; }
        public override void NetworkSerialize<T>(BufferSerializer<T> serializer) { serializer.SerializeValue(ref Value); }
    }

    public class StringParams : NetworkEventParams
    {
        public FixedString128Bytes Value;
        public StringParams(string value) { Value = value; }
        public override void NetworkSerialize<T>(BufferSerializer<T> serializer) { serializer.SerializeValue(ref Value); }
    }

    public class UlongParams : NetworkEventParams
    {
        public ulong Value;
        public UlongParams(ulong value) { Value = value; }
        public override void NetworkSerialize<T>(BufferSerializer<T> serializer) { serializer.SerializeValue(ref Value); }
    }

    public class Vector3Params : NetworkEventParams
    {
        public Vector3 Value;
        public Vector3Params(Vector3 value) { Value = value; }
        public override void NetworkSerialize<T>(BufferSerializer<T> serializer) { serializer.SerializeValue(ref Value); }
    }

    public class Vector2Params : NetworkEventParams
    {
        public Vector2 Value;
        public Vector2Params(Vector2 value) { Value = value; }
        public override void NetworkSerialize<T>(BufferSerializer<T> serializer) { serializer.SerializeValue(ref Value); }
    }
}
