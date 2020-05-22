using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface ISerializer
    {
        byte[] Serialize(object obj);

        object Deserialize(byte[] data);
    }
}
