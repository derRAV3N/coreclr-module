using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using AltV.Net.Elements.Entities;

namespace AltV.Net.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MValue
    {
        // The MValue param needs to be an List type
        public delegate void Function(ref MValue args, ref MValue result);

        public enum Type : byte
        {
            NIL = 0,
            BOOL = 1,
            INT = 2,
            UINT = 3,
            DOUBLE = 4,
            STRING = 5,
            LIST = 6,
            DICT = 7,
            ENTITY = 8,
            FUNCTION = 9
        }

        internal static readonly int Size = Marshal.SizeOf<MValue>();

        public static MValue Nil = new MValue(0, IntPtr.Zero);

        public static MValue? CreateFromObject(object obj)
        {
            switch (obj)
            {
                case IEntity entity:
                    return Create(entity);
                case bool value:
                    return Create(value);
                case int value:
                    return Create(value);
                case uint value:
                    return Create(value);
                case long value:
                    return Create(value);
                case ulong value:
                    return Create(value);
                case double value:
                    return Create(value);
                case string value:
                    return Create(value);
                case MValue value:
                    return value;
                case MValue[] value:
                    return Create(value);
                case Dictionary<string, object> value:
                    var dictMValues = new List<MValue>();
                    foreach (var dictValue in value.Values)
                    {
                        var dictMValue = CreateFromObject(dictValue);
                        if (!dictMValue.HasValue) continue;
                        dictMValues.Add(dictMValue.Value);
                    }

                    return Create(dictMValues.ToArray(), value.Keys.ToArray());
                case Invoker value:
                    return Create(value);
                case Function value:
                    return Create(value);
                case object[] value:
                    return Create((from objArrayValue in value
                        select CreateFromObject(objArrayValue)
                        into objArrayMValue
                        where objArrayMValue.HasValue
                        select objArrayMValue.Value).ToArray());
                case Net.Function function:
                    return Create(function.call);
            }

            return null;
        }

        public static MValue Create(bool value)
        {
            var mValue = Nil;
            Alt.MValueCreate.MValue_CreateBool(value, ref mValue);
            return mValue;
        }

        public static MValue Create(long value)
        {
            var mValue = Nil;
            Alt.MValueCreate.MValue_CreateInt(value, ref mValue);
            return mValue;
        }

        public static MValue Create(ulong value)
        {
            var mValue = Nil;
            Alt.MValueCreate.MValue_CreateUInt(value, ref mValue);
            return mValue;
        }

        public static MValue Create(double value)
        {
            var mValue = Nil;
            Alt.MValueCreate.MValue_CreateDouble(value, ref mValue);
            return mValue;
        }

        public static MValue Create(string value)
        {
            var mValue = Nil;
            Alt.MValueCreate.MValue_CreateString(value, ref mValue);
            return mValue;
        }

        public static MValue Create(MValue[] values)
        {
            var mValue = Nil;
            Alt.MValueCreate.MValue_CreateList(values, (ulong) values.Length, ref mValue);
            return mValue;
        }

        public static MValue Create(MValue[] values, string[] keys)
        {
            if (values.Length != keys.Length) throw new ArgumentException("values length != keys length");
            var mValue = Nil;
            Alt.MValueCreate.MValue_CreateDict(values, keys, (ulong) values.Length, ref mValue);
            return mValue;
        }

        public static MValue Create(IEntity entity)
        {
            var mValue = Nil;
            Alt.MValueCreate.MValue_CreateEntity(entity.NativePointer, ref mValue);
            return mValue;
        }

        public static MValue Create(Function function)
        {
            var mValue = Nil;
            Alt.MValueCreate.MValue_CreateFunction(Alt.MValueCreate.Invoker_Create(function), ref mValue);
            return mValue;
        }

        public static MValue Create(Invoker invoker)
        {
            var mValue = Nil;
            Alt.MValueCreate.MValue_CreateFunction(invoker.NativePointer, ref mValue);
            return mValue;
        }

        public readonly Type type;
        public readonly IntPtr storagePointer;

        public MValue(Type type, IntPtr storagePointer)
        {
            this.type = type;
            this.storagePointer = storagePointer;
        }

        public bool GetBool()
        {
            return Alt.MValueGet.MValue_GetBool(ref this);
        }

        public long GetInt()
        {
            return Alt.MValueGet.MValue_GetInt(ref this);
        }

        public ulong GetUint()
        {
            return Alt.MValueGet.MValue_GetUInt(ref this);
        }

        public double GetDouble()
        {
            return Alt.MValueGet.MValue_GetDouble(ref this);
        }

        public void GetString(ref string value)
        {
            Alt.MValueGet.MValue_GetString(ref this, ref value);
        }

        public string GetString()
        {
            var value = string.Empty;
            Alt.MValueGet.MValue_GetString(ref this, ref value);
            return value;
        }

        public void GetEntityPointer(ref IntPtr entityPointer)
        {
            Alt.MValueGet.MValue_GetEntity(ref this, ref entityPointer);
        }

        public IntPtr GetEntityPointer()
        {
            var entityPointer = IntPtr.Zero;
            Alt.MValueGet.MValue_GetEntity(ref this, ref entityPointer);
            return entityPointer;
        }

        public void GetList(ref MValueArray mValueArray)
        {
            Alt.MValueGet.MValue_GetList(ref this, ref mValueArray);
        }

        public MValue[] GetList()
        {
            var mValueArray = MValueArray.Nil;
            Alt.MValueGet.MValue_GetList(ref this, ref mValueArray);
            return mValueArray.ToArray();
        }

        public Dictionary<string, MValue> GetDictionary()
        {
            var stringViewArrayRef = StringViewArray.Nil;
            var valueArrayRef = MValueArray.Nil;
            Alt.MValueGet.MValue_GetDict(ref this, ref stringViewArrayRef, ref valueArrayRef);
            var stringViewArray = stringViewArrayRef.ToArray();
            var dictionary = new Dictionary<string, MValue>();
            var valueArray = valueArrayRef.ToArray();
            var i = 0;
            foreach (var key in stringViewArray)
            {
                dictionary[key.Text] = valueArray[i++];
            }

            return dictionary;
        }

        public Function GetFunction()
        {
            return Alt.MValueGet.MValue_GetFunction(ref this);
        }

        public MValue CallFunction(MValue[] args)
        {
            var result = Nil;
            Alt.MValueCall.MValue_CallFunction(ref this, args, (ulong) args.Length, ref result);
            return result;
        }

        public override string ToString()
        {
            switch (type)
            {
                case Type.NIL:
                    return "Nil";
                case Type.BOOL:
                    return GetBool().ToString();
                case Type.INT:
                    return GetInt().ToString();
                case Type.UINT:
                    return GetUint().ToString();
                case Type.DOUBLE:
                    return GetDouble().ToString(CultureInfo.InvariantCulture);
                case Type.STRING:
                    return GetString();
                case Type.LIST:
                    return GetList().Length.ToString();
                case Type.DICT:
                    return GetDictionary().Count.ToString();
                case Type.ENTITY:
                    return "MValue<Entity>";
                case Type.FUNCTION:
                    return "MValue<Function>";
            }

            return "MValue<>";
        }

        public object ToObject()
        {
            switch (type)
            {
                case Type.NIL:
                    return null;
                case Type.BOOL:
                    return GetBool();
                case Type.INT:
                    return GetInt();
                case Type.UINT:
                    return GetUint();
                case Type.DOUBLE:
                    return GetDouble();
                case Type.STRING:
                    return GetString();
                case Type.LIST:
                    var mValueArray = MValueArray.Nil;
                    Alt.MValueGet.MValue_GetList(ref this, ref mValueArray);
                    var arrayValue = mValueArray.data;
                    var arrayValues = new object[mValueArray.size];
                    for (var i = 0; i < arrayValues.Length; i++)
                    {
                        arrayValues[i] = Marshal.PtrToStructure<MValue>(arrayValue).ToObject();
                        arrayValue += Size;
                    }

                    return arrayValues;
                case Type.DICT:
                    var stringViewArrayRef = StringViewArray.Nil;
                    var valueArrayRef = MValueArray.Nil;
                    Alt.MValueGet.MValue_GetDict(ref this, ref stringViewArrayRef, ref valueArrayRef);
                    var stringViewArray = stringViewArrayRef.ToArray();
                    var dictionary = new Dictionary<string, object>();
                    var dictValue = valueArrayRef.data;
                    var length = (int) valueArrayRef.size;
                    for (var i = 0; i < length; i++)
                    {
                        dictionary[stringViewArray[i].Text] = Marshal.PtrToStructure<MValue>(dictValue).ToObject();
                        dictValue += Size;
                    }

                    return dictionary;
                case Type.ENTITY:
                    var entityPointer = IntPtr.Zero;
                    GetEntityPointer(ref entityPointer);
                    if (Server.Instance.EntityPool.Get(entityPointer, out var entity))
                    {
                        return entity;
                    }

                    return null;
                case Type.FUNCTION:
                    return GetFunction();
                default:
                    return null;
            }
        }
    }
}