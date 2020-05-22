using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Common
{
    public class BinarySerializeHelper : ISerializer
    {
        public object Deserialize(byte[] data)
        {
            var formatter = new BinaryFormatter();
            
            using (var stream = new MemoryStream(data))
            {
                return formatter.Deserialize(stream);
            }
        }

        public byte[] Serialize(object obj)
        {
            var formatter = new BinaryFormatter();

            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                return stream.ToArray();
            }
        }
    }
}
