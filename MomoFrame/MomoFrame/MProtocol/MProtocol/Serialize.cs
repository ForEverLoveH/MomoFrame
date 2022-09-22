using MProtocol;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class Serialize
{
    public static byte[] ToBytes<T>(T msg) where T : class
    {
        byte[] data = null;
        MemoryStream ms = new MemoryStream();
        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            bf.Serialize(ms, msg);
            ms.Seek(0, SeekOrigin.Begin);
            data = ms.ToArray();
        }
        catch (SerializationException e)
        {
            throw new Exception($"Faild to serialize.Reson:{e.Message}");
        }
        finally
        {
            ms.Close();
        }
        return data;
    }
    public static T FromBytes<T>(byte[] bytes) where T : class
    {
        T msg = null;
        MemoryStream ms = new MemoryStream(bytes);
        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            msg = (T)bf.Deserialize(ms);
        }
        catch (SerializationException e)
        {
            throw new Exception($"Faild to deserialize.Reson:{e.Message} bytesLen:{bytes.Length}.");
        }
        finally
        {
            ms.Close();
        }
        return msg;
    }
}
