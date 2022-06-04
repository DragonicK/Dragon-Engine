using System.Reflection;

namespace Dragon.Network.Messaging;
public class MessageSerializer : ISerializer {

    public byte[] Serialize<T>(T type) {
        var buffer = new ByteBuffer();

        WriteClass(type, buffer);

        return buffer.ToArray();
    }

    public object Deserialize(byte[] buffer, Type type) {
        var _buffer = new ByteBuffer(buffer);
        var t = Activator.CreateInstance(type)!;

        ReadClass(t, _buffer);

        _buffer.Dispose();

        return t;
    }

    private void ReadClass<T>(T type, ByteBuffer buffer) {
        var properties = GetOrderedProperties(type);
        ReadProperties(properties, type, buffer);
    }

    private void WriteClass<T>(T type, ByteBuffer buffer) {
        var properties = GetOrderedProperties(type);
        WriteProperties(properties, type, buffer);
    }

    private void ReadProperties<T>(IEnumerable<PropertyInfo> properties, T type, ByteBuffer buffer) {
        foreach (var property in properties) {
            ReadProperty(property, type, buffer);
        }
    }

    private void WriteProperties<T>(IEnumerable<PropertyInfo> properties, T type, ByteBuffer buffer) {
        foreach (var property in properties) {
            WriteProperty(property, type, buffer);
        }
    }

    private void ReadProperty<T>(PropertyInfo property, T obj, ByteBuffer buffer) {
        var type = property.PropertyType;

        if (type == typeof(bool)) {
            property.SetValue(obj, buffer.ReadBoolean());
        }
        else if (type == typeof(byte)) {
            property.SetValue(obj, buffer.ReadByte());
        }
        else if (type == typeof(short)) {
            property.SetValue(obj, buffer.ReadInt16());
        }
        else if (type == typeof(int)) {
            property.SetValue(obj, buffer.ReadInt32());
        }
        else if (type == typeof(string)) {
            property.SetValue(obj, buffer.ReadString());
        }
        else if (type.IsEnum) {
            property.SetValue(obj, buffer.ReadInt32());
        }
        else if (type.IsArray) {
            ReadArray(property, obj, buffer);
        }
        else if (type.IsClass || type.IsValueType) {
            var t = Activator.CreateInstance(type);
            ReadClass(t, buffer);
            property.SetValue(obj, t);
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

    private void ReadArray<T>(PropertyInfo propertyInfo, T obj, ByteBuffer buffer) {
        var array = propertyInfo.GetValue(obj, null) as Array;

        var arrayType = array!.GetType().GetElementType()!;

        int length = buffer.ReadInt32();

        array = Array.CreateInstance(arrayType, length);

        for (var i = 0; i < length; i++) {
            if (arrayType == typeof(bool)) {
                array.SetValue(buffer.ReadBoolean(), i);
            }
            else if (arrayType == typeof(byte)) {
                array.SetValue(buffer.ReadByte(), i);
            }
            else if (arrayType == typeof(short)) {
                array.SetValue(buffer.ReadInt16(), i);
            }
            else if (arrayType == typeof(int)) {
                array.SetValue(buffer.ReadInt32(), i);
            }
            else if (arrayType == typeof(string)) {
                array.SetValue(buffer.ReadString(), i);
            }
            else if (arrayType.IsEnum) {
                array.SetValue(buffer.ReadInt32(), i);
            }
            else {
                var value = Activator.CreateInstance(arrayType);
                var properties = GetOrderedProperties(value);

                foreach (var property in properties) {
                    var type = property.PropertyType;

                    if (type == typeof(bool)) {
                        property.SetValue(value, buffer.ReadBoolean());
                    }
                    else if (type == typeof(byte)) {
                        property.SetValue(value, buffer.ReadByte());
                    }
                    else if (type == typeof(short)) {
                        property.SetValue(value, buffer.ReadInt16());
                    }
                    else if (type == typeof(int)) {
                        property.SetValue(value, buffer.ReadInt32());
                    }
                    else if (type == typeof(string)) {
                        property.SetValue(value, buffer.ReadString());
                    }
                    else if (type.IsEnum) {
                        property.SetValue(value, buffer.ReadInt32());
                    }
                    else if (type.IsArray) {
                        ReadArray(property, value, buffer);
                    }
                    else if (type.IsClass || type.IsValueType) {
                        var t = Activator.CreateInstance(type);
                        ReadClass(t, buffer);
                        property.SetValue(obj, t);
                    }

                    array.SetValue(value, i);
                }
            }
        }

        propertyInfo.SetValue(obj, array);
    }

    private IEnumerable<PropertyInfo> GetOrderedProperties<T>(T obj) {
        return obj!.GetType().GetRuntimeProperties();
    }
}