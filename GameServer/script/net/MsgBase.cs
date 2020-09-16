using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using System.Web.Script.Serialization;

public class MsgBase {
    public string protoName = "";
    static JavaScriptSerializer js =  new JavaScriptSerializer();
    public static byte[] Encode(MsgBase msgBase)
    {
        string s = js.Serialize(msgBase);
        return System.Text.Encoding.UTF8.GetBytes(s);
    }
    public static T Decode<T>(string  protoName, byte[] bytes,int offset,int count)
    {
        string s = System.Text.Encoding.UTF8.GetString(bytes,offset,count);
        T msgBase = (T)js.Deserialize(s, Type.GetType(protoName));
        return msgBase;
    }
    public static byte[] EncodeName(MsgBase msgBase)
    {
        byte[] nameBytes = System.Text.Encoding.UTF8.GetBytes(msgBase.protoName);
        Int16 len = (Int16)nameBytes.Length;
        byte[] bytes = new byte[2 + len];
        bytes[0] = (byte)(len % 256);
        bytes[1] = (byte)(len / 256);
        Array.Copy(nameBytes, 0, bytes, 2, len);
        return bytes;
    }

    public static string DecodeName(byte[] bytes,int offset, out int count)
    {
        count = 0;
        if(offset + 2 > bytes.Length)
        {
            return "";
        }
        Int16 len = (Int16)((bytes[offset + 1] << 8) | bytes[offset]);
        if(offset+2+len > bytes.Length)
        {
            return "";
        }
        count = 2 + len;
        return System.Text.Encoding.UTF8.GetString(bytes, offset+2, count);
    }
    
}
