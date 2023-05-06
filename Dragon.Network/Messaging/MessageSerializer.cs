using Dragon.Network.Pool;

using System.Reflection;

namespace Dragon.Network.Messaging;

public sealed class MessageSerializer : ISerializer {

    public byte[] Serialize<T>(T type) {
        var buffer = new ByteBuffer();

        // Write packet data.
        WriteClass(type, buffer);

        // Add padding to checksum and cipher.
        var length = buffer.Length() + 4;

        length += 8 - length % 8;

        var remaining = length - buffer.Length();

        buffer.WriteEmptyBytes(remaining);

        return buffer.ToArray();
    }

    public object Deserialize(IEngineBuffer buffer, Type type) {
        var instance = Activator.CreateInstance(type)!;

        buffer.Reader.PointToStart();

        ReadClass(instance, buffer);

        return instance;
    }

    #region Read Class, Arrays & Properties 

    private void ReadClass<T>(T instance, IEngineBuffer buffer) {
        ReadProperties(GetRuntimeProperties(instance), instance, buffer);
    }

    private void ReadProperties<T>(IEnumerable<PropertyInfo> properties, T instance, IEngineBuffer buffer) {
        foreach (var property in properties) {
            ReadProperty(property, instance, buffer);
        }
    }

    private void ReadProperty<T>(PropertyInfo property, T instance, IEngineBuffer buffer) {
        var type = property.PropertyType;
        var reader = buffer.Reader;

        if (type == typeof(bool)) {
            property.SetValue(instance, reader.ReadBoolean());
        }
        else if (type == typeof(byte)) {
            property.SetValue(instance, reader.ReadByte());
        }
        else if (type == typeof(short)) {
            property.SetValue(instance, reader.ReadInt16());
        }
        else if (type == typeof(int)) {
            property.SetValue(instance, reader.ReadInt32());
        }
        else if (type == typeof(string)) {
            property.SetValue(instance, reader.ReadString());
        }
        else if (type.IsEnum) {
            property.SetValue(instance, reader.ReadInt32());
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

    private unsafe void ReadArray<T>(PropertyInfo propertyInfo, T obj, IEngineBuffer buffer) {
        var array = propertyInfo.GetValue(obj, null) as Array;

        var reader = buffer.Reader;
        var length = reader.ReadInt32();

        if (length > 0) {
            if (array is not null) {
                var arrayType = array.GetType().GetElementType();

                if (arrayType is not null) {

                    array = Array.CreateInstance(arrayType, length);

                    if (arrayType == typeof(bool)) {
                        var arr = (bool[])array;

                        fixed (bool* p = arr) {
                            reader.MemoryCopy(p, length, length);
                        }
                    }
                    else if (arrayType == typeof(byte)) {
                        var arr = (byte[])array;

                        fixed (byte* p = arr) {
                            reader.MemoryCopy(p, length, length);
                        }
                    }
                    else if (arrayType == typeof(short)) {
                        for (var i = 0; i < length; i++) {
                            array.SetValue(reader.ReadInt16(), i);
                        }
                    }
                    else if (arrayType == typeof(int)) {
                        for (var i = 0; i < length; i++) {
                            array.SetValue(reader.ReadInt32(), i);
                        }
                    }
                    else if (arrayType == typeof(string)) {
                        for (var i = 0; i < length; i++) {
                            array.SetValue(reader.ReadString(), i);
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

    private void WriteClass<T>(T type, ByteBuffer buffer) {
        var properties = GetRuntimeProperties(type);
        WriteProperties(properties, type, buffer);
    }

    private void WriteProperties<T>(IEnumerable<PropertyInfo> properties, T type, ByteBuffer buffer) {
        foreach (var property in properties) {
            WriteProperty(property, type, buffer);
        }
    }

    private void WriteProperty<T>(PropertyInfo property, T obj, ByteBuffer buffer) {
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

    private void WriteArray(Array array, ByteBuffer buffer) {
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

    private IEnumerable<PropertyInfo> GetRuntimeProperties<T>(T obj) {
        return obj!.GetType().GetRuntimeProperties();
    }
}