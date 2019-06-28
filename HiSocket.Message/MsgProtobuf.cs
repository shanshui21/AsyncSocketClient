using ProtoBuf;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace HiSocket.Message
{
    public class MsgProtobuf : MsgBase
    {
        public MsgProtobuf() : base()
        {

        }
        public MsgProtobuf(IByteArray byteArray) : base(byteArray)
        {
        }

        public void Write<T>(T t)
        {
            var bytes = Serialize(t);
            ByteArray.Write(bytes);
        }

        public T Read<T>()
        {
            return Deserialize<T>(ByteArray.Read(ByteArray.Length));
        }

        public byte[] Serialize<T>(T t)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize<T>(stream, t);
                return stream.ToArray();
            }
        }
        public T Deserialize<T>(byte[] param)
        {
            using (MemoryStream stream = new MemoryStream(param))
            {
                T obj = default(T);
                obj = Serializer.Deserialize<T>(stream);
                return obj;
            }
        }
       
        /// <summary>
        /// 字节数组转换成结构体
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="len"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public  object BytesToStruct(byte[] buf,Type type)
        {
            int size = Marshal.SizeOf(type);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(buf, 0, buffer, size);
                return Marshal.PtrToStructure(buffer, type);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        //// <summary>
        /// 结构体转byte数组
        /// </summary>
        /// <param name="structObj">要转换的结构体</param>
        /// <returns>转换后的byte数组</returns>
        public  byte[] StructToBytes(object structObj)
        {
            //得到结构体的大小
            int size = Marshal.SizeOf(structObj);
            //创建byte数组
            byte[] bytes = new byte[size];
            //分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将结构体拷到分配好的内存空间
            Marshal.StructureToPtr(structObj, structPtr, false);
            //从内存空间拷到byte数组
            Marshal.Copy(structPtr, bytes, 0, size);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            //返回byte数组
            return bytes;
        }
    }
}
