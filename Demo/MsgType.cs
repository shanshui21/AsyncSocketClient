using ProtoBuf;
using System;
using System.IO;
using System.Runtime.InteropServices;

[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public class Head
{
    public byte MsgId { get; set; }//主消息号
    public ushort Leng { get; set; }//数据字节长度
    public int HashCode { get; set; }//数据哈希值
   
}



