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

        public IntParams() { }
        public IntParams(int value) { Value = value; }
        protected override void Deserialize(FastBufferReader reader) { reader.ReadValue(out Value); }
        protected override void Serialize(FastBufferWriter writer) { writer.WriteValue(Value); }

        public static implicit operator int(IntParams left) => left.Value;
        public static implicit operator IntParams(int left) => new IntParams(left);
    }

    public class FloatParams : NetworkEventParams
    {
        protected override ushort Size => sizeof(float);
        public float Value;

        public FloatParams() { }
        public FloatParams(float value) { Value = value; }
        protected override void Deserialize(FastBufferReader reader) { reader.ReadValue(out Value); }
        protected override void Serialize(FastBufferWriter writer) { writer.WriteValue(Value); }

        public static implicit operator float(FloatParams left) => left.Value;
        public static implicit operator FloatParams(float left) => new FloatParams(left);
    }

    public class UlongParams : NetworkEventParams
    {
        protected override ushort Size => sizeof(ulong);
        public ulong Value;

        public UlongParams() { }
        public UlongParams(ulong value) { Value = value; }
        protected override void Deserialize(FastBufferReader reader) { reader.ReadValue(out Value); }
        protected override void Serialize(FastBufferWriter writer) { writer.WriteValue(Value); }

        public static implicit operator ulong(UlongParams left) => left.Value;
        public static implicit operator UlongParams(ulong left) => new UlongParams(left);
    }

    public class BoolParams : NetworkEventParams
    {
        protected override ushort Size => sizeof(bool);
        public bool Value;

        public BoolParams() { }
        public BoolParams(bool value) { Value = value; }
        protected override void Deserialize(FastBufferReader reader) { reader.ReadValue(out Value); }
        protected override void Serialize(FastBufferWriter writer) { writer.WriteValue(Value); }

        public static implicit operator bool(BoolParams left) => left.Value;
        public static implicit operator BoolParams(bool left) => new BoolParams(left);
    }

    public class Vector3Params : NetworkEventParams
    {
        protected override ushort Size => sizeof(float) * 3;
        public Vector3 Value;

        public Vector3Params() { }
        public Vector3Params(Vector3 value) { Value = value; }
        protected override void Deserialize(FastBufferReader reader) { reader.ReadValue(out Value); }
        protected override void Serialize(FastBufferWriter writer) { writer.WriteValue(Value); }

        public static implicit operator Vector3(Vector3Params left) => left.Value;
        public static implicit operator Vector3Params(Vector3 left) => new Vector3Params(left);
    }

    public class TransformParams : NetworkEventParams
    {
        protected override ushort Size => sizeof(float) * 6;
        public Vector3 Position;
        public Vector3 Rotation;

        public TransformParams() { }
        public TransformParams(Vector3 position, Vector3 rotation) 
        { 
            Position = position; 
            Rotation = rotation; 
        }

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

        public static implicit operator TransformParams(Transform left) => new TransformParams(left.position, left.eulerAngles);
    }

    public class AttackParams : NetworkEventParams
    {
        protected override ushort Size => sizeof(ulong) + sizeof(float) + sizeof(float) * 3 * 2;
        public ulong AttackerID;
        public float Damage;
        public Vector3 Point;
        public Vector3 Normal;

        public AttackParams() { }
        public AttackParams(ulong attackerID, float damage, Vector3 point, Vector3 normal) 
        {
            AttackerID = attackerID;
            Damage = damage;
            Point = point;
            Normal = normal;
        }

        protected override void Deserialize(FastBufferReader reader) 
        { 
            reader.ReadValue(out AttackerID);
            reader.ReadValue(out Damage);
            reader.ReadValue(out Point);
            reader.ReadValue(out Normal);
        }

        protected override void Serialize(FastBufferWriter writer)
        {
            writer.WriteValue(AttackerID);
            writer.WriteValue(Damage);
            writer.WriteValue(Point);
            writer.WriteValue(Normal);
        }
    }
}
