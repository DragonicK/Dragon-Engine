using System.Reflection;

using Dragon.Network.Pool;

namespace Dragon.Network.Messaging;

public sealed class MessageSerializer : ISerializer {

    public IEngineBufferWriter Serialize<T>(T type, IEngineBufferWriter buffer) {
        AddEmptyBytesOfPacketSize(buffer);

        WriteClass(type, buffer);

        return AddCheckSumPadding(buffer);
    }

    private static void AddEmptyBytesOfPacketSize(IEngineBufferWriter buffer) {
        buffer.Write(0);
    }

    /// <summary>
    /// Add padding to checksum and cipher.
    /// </summary>
    /// <param name="buffer"></param>
    private static IEngineBufferWriter AddCheckSumPadding(IEngineBufferWriter buffer) {
        var firstBytes = sizeof(int);

        var length = buffer.Length - firstBytes;

        length += 8 - length % 8;

        var remaining = length - buffer.Length;

        buffer.WriteEmptyBytes(remaining + firstBytes);

        return buffer;
    }

    public object Deserialize(IEngineBufferReader buffer, Type type) {
        var instance = Activator.CreateInstance(type)!;

        buffer.ResetPosition();

        ReadClass(instance, buffer);

        return instance;
    }

    #region Read 

    private void ReadClass<T>(T instance, IEngineBufferReader buffer) {
        ReadProperties(GetRuntimeProperties(instance), instance, buffer);
    }

    private void ReadProperties<T>(IEnumerable<PropertyInfo> properties, T instance, IEngineBufferReader buffer) {
        foreach (var property in properties) {
            ReadProperty(property, instance, buffer);
        }
    }

    private void ReadProperty<T>(PropertyInfo property, T instance, IEngineBufferReader buffer) {
        var type = property.PropertyType;

        if (type == typeof(bool)) {
            property.SetValue(instance, buffer.ReadBoolean());
        }
        else if (type == typeof(byte)) {
            property.SetValue(instance, buffer.ReadByte());
        }
        else if (type == typeof(short)) {
            property.SetValue(instance, buffer.ReadInt16());
        }
        else if (type == typeof(int)) {
            property.SetValue(instance, buffer.ReadInt32());
        }
        else if (type == typeof(long)) {
            property.SetValue(instance, buffer.ReadInt64());
        }
        else if (type == typeof(string)) {
            property.SetValue(instance, buffer.ReadString());
        }
        else if (type == typeof(float)) {
            property.SetValue(instance, buffer.ReadFloat());
        }
        else if (type.IsEnum) {
            property.SetValue(instance, buffer.ReadInt32());
        }
        else if (type.IsArray) {
            ReadArray(property, instance, buffer);
        }
        else if (type.IsClass || type.IsValueType) {
            var mInstance = Activator.CreateInstance(type);

            ReadClass(mInstance, buffer);

            property.SetValue(instance, mInstance);
        }
    }

    private unsafe void ReadArray<T>(PropertyInfo propertyInfo, T obj, IEngineBufferReader buffer) {
        var array = propertyInfo.GetValue(obj, null) as Array;
        var length = buffer.ReadInt32();

        if (length > 0) {
            if (array is not null) {
                var arrayType = array.GetType().GetElementType();

                if (arrayType is not null) {

                    array = Array.CreateInstance(arrayType, length);

                    if (arrayType == typeof(bool)) {
                        var arr = (bool[])array;

                        fixed (bool* p = arr) {
                            buffer.MemoryCopy(p, length, length);
                        }
                    }
                    else if (arrayType == typeof(byte)) {
                        var arr = (byte[])array;

                        fixed (byte* p = arr) {
                            buffer.MemoryCopy(p, length, length);
                        }
                    }
                    else if (arrayType == typeof(short)) {
                        for (var i = 0; i < length; i++) {
                            array.SetValue(buffer.ReadInt16(), i);
                        }
                    }
                    else if (arrayType == typeof(int)) {
                        for (var i = 0; i < length; i++) {
                            array.SetValue(buffer.ReadInt32(), i);
                        }
                    }
                    else if (arrayType == typeof(long)) {
                        for (var i = 0; i < length; i++) {
                            array.SetValue(buffer.ReadInt64(), i);
                        }
                    }
                    else if (arrayType == typeof(float)) {
                        for (var i = 0; i < length; i++) {
                            array.SetValue(buffer.ReadFloat(), i);
                        }
                    }
                    else if (arrayType == typeof(string)) {
                        for (var i = 0; i < length; i++) {
                            array.SetValue(buffer.ReadString(), i);
                        }
                    }
                    else {
                        var mInstance = Activator.CreateInstance(arrayType);
                        var mProperties = GetRuntimeProperties(mInstance);

                        ReadProperties(mProperties, mInstance, buffer);
                    }

                    propertyInfo.SetValue(obj, array);
                }
            }
        }
    }

    #endregion

    #region Write 

    private void WriteClass<T>(T type, IEngineBufferWriter buffer) {
        var properties = GetRuntimeProperties(type);
        WriteProperties(properties, type, buffer);
    }

    private void WriteProperties<T>(IEnumerable<PropertyInfo> properties, T type, IEngineBufferWriter buffer) {
        foreach (var property in properties) {
            WriteProperty(property, type, buffer);
        }
    }

    private void WriteProperty<T>(PropertyInfo property, T obj, IEngineBufferWriter buffer) {
        var type = property.PropertyType;
        var value = property.GetValue(obj, null);

        if (value is not null) {
            if (type == typeof(bool)) {
                buffer.Write((bool)value);
            }
            else if (type == typeof(byte)) {
                buffer.Write((byte)value);
            }
            else if (type == typeof(short)) {
                buffer.Write((short)value);
            }
            else if (type == typeof(int)) {
                buffer.Write((int)value);
            }
            else if (type == typeof(long)) {
                buffer.Write((long)value);
            }
            else if (type == typeof(float)) {
                buffer.Write((float)value);
            }
            else if (type == typeof(string)) {
                buffer.Write((string)value);
            }
            else if (type.IsEnum) {
                buffer.Write((int)value);
            }
            else if (type.IsArray) {
                WriteArray((Array)value, buffer);
            }
            else if (type.IsClass || type.IsValueType) {
                WriteClass(value, buffer);
            }
        }
    }

    private void WriteArray(Array array, IEngineBufferWriter buffer) {
        buffer.Write(array.Length);

        for (var i = 0; i < array.Length; i++) {
            var item = array.GetValue(i);

            if (item is not null) {
                var type = item.GetType();

                if (type == typeof(bool)) {
                    buffer.Write((bool)item);
                }
                else if (type == typeof(byte)) {
                    buffer.Write((byte)item);
                }
                else if (type == typeof(short)) {
                    buffer.Write((short)item);
                }
                else if (type == typeof(int)) {
                    buffer.Write((int)item);
                }
                else if (type == typeof(long)) {
                    buffer.Write((long)item);
                }
                else if (type == typeof(float)) {
                    buffer.Write((float)item);
                }
                else if (type == typeof(string)) {
                    buffer.Write((string)item);
                }
                else if (type.IsEnum) {
                    buffer.Write((int)item);
                }
                else if (type.IsArray) {
                    WriteArray((Array)item, buffer);
                }
                else if (type.IsClass || type.IsValueType) {
                    WriteClass(item, buffer);
                }
            }
        }
    }

    #endregion

    private static IEnumerable<PropertyInfo> GetRuntimeProperties<T>(T obj) {
        return obj!.GetType().GetRuntimeProperties();
    }
}