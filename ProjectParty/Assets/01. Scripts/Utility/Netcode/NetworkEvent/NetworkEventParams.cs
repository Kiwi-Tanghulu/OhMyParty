using System;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace OMG.NetworkEvents
{
    internal struct NetworkEventPacket : INetworkSerializable
    {
        public ulong InstanceID;
        public ulong EventID;
        public FixedString128Bytes ParamsID;
        public byte[] Buffer;

        public NetworkEventPacket(ulong instanceID, ulong eventID, FixedString128Bytes paramsID, byte[] buffer)
        {
            InstanceID = instanceID;
            EventID = eventID;
            ParamsID = paramsID;
            Buffer = buffer;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref ParamsID);
            serializer.SerializeValue(ref InstanceID);
            serializer.SerializeValue(ref EventID);
            serializer.SerializeValue(ref Buffer);
        }
    }

    public abstract class NetworkEventParams
    {
        protected abstract ushort Size { get; }

        public void Deserialize(byte[] buffer)
        {
            FastBufferReader bufferReader = new FastBufferReader(buffer, Allocator.Temp);
            if(bufferReader.TryBeginRead(Size) == false)
                throw new OverflowException("Not enough space in the buffer");

            Deserialize(bufferReader);
        }

        public byte[] Serialize()
        {
            FastBufferWriter bufferWriter = new FastBufferWriter(Size, Allocator.Temp);
            if(bufferWriter.TryBeginWrite(Size) == false)
                throw new OverflowException("Not enough space in the buffer");

            Serialize(bufferWriter);
            return bufferWriter.ToArray();
        }

        protected abstract void Deserialize(FastBufferReader reader);
        protected abstract void Serialize(FastBufferWriter writer);
    }

    public class NoneParams : NetworkEventParams
    {
        protected override ushort Size => 0;
        protected override void Deserialize(FastBufferReader reader) { }
        protected override void Serialize(FastBufferWriter writer) { }
    }

    public class IntParams : NetworkEventParams
    {
        protected override ushort Size => sizeof(int);
        public int Value;

        protected override void Deserialize(FastBufferReader reader)
        {
            reader.ReadValue(out Value);
        }

        protected override void Serialize(FastBufferWriter writer)
        {
            writer.WriteValue(Value);
        }
    }

    public class FloatParams : NetworkEventParams
    {
        protected override ushort Size => sizeof(float);
        public float Value;

        protected override void Deserialize(FastBufferReader reader)
        {
            reader.ReadValue(out Value);
        }

        protected override void Serialize(FastBufferWriter writer)
        {
            writer.WriteValue(Value);
        }
    }

    public class UlongParams : NetworkEventParams
    {
        protected override ushort Size => sizeof(ulong);
        public ulong Value;

        protected override void Deserialize(FastBufferReader reader)
        {
            reader.ReadValue(out Value);
        }

        protected override void Serialize(FastBufferWriter writer)
        {
            writer.WriteValue(Value);
        }
    }

    public class BoolParams : NetworkEventParams
    {
        protected override ushort Size => sizeof(bool);
        public bool Value;

        protected override void Deserialize(FastBufferReader reader)
        {
            reader.ReadValue(out Value);
        }

        protected override void Serialize(FastBufferWriter writer)
        {
            writer.WriteValue(Value);
        }
    }

    public class Vector3Params : NetworkEventParams
    {
        protected override ushort Size => sizeof(float) * 3;
        public Vector3 Value;

        protected override void Deserialize(FastBufferReader reader)
        {
            reader.ReadValue(out Value);
        }

        protected override void Serialize(FastBufferWriter writer)
        {
            writer.WriteValue(Value);
        }
    }

    public class TransformParams : NetworkEventParams
    {
        protected override ushort Size => sizeof(float) * 6;
        public Vector3 Position;
        public Vector3 Rotation;

        protected override void Deserialize(FastBufferReader reader)
        {
            reader.ReadValue(out Position);
            reader.ReadValue(out Rotation);
        }

        protected override void Serialize(FastBufferWriter writer)
        {
            writer.WriteValue(Position);
            writer.WriteValue(Rotation);
        }
    }
}
