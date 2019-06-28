using HiSocket;
using HiSocket.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Demo
{
    class Example
    {
        private TcpConnection _tcp;

        // private TestServer _server = new TestServer();
        private bool _isConnected;
        // Use this for initialization
        private MsgProtobuf msgProtobuf;
       public void Start()
        {//==========
            msgProtobuf = new MsgProtobuf();
            //=============
               var ip = IPAddress.Parse("127.0.0.1");
            var iep = new IPEndPoint(ip, 6091);
            _tcp = new TcpConnection(new PackageExample());
            _tcp.OnConnecting += OnConnecting;
            _tcp.OnConnected += OnConnected;
            _tcp.OnReceive += OnReceive;

            _tcp.Connect(iep); //start connect
           
        }

        void OnConnecting()
        {
          //  Debug.Log("<color=green>connecting...</color>");
            Console.WriteLine("0000000000");
        }

        void OnConnected()
        {
           // Debug.Log("<color=green>connected</color>");
            Console.WriteLine(222222222222222);
            Update();
        }
       static string test = "test";

        Head head = new Head { Leng =1234, MsgId =45, HashCode =test.GetHashCode() };
       public void Update()
        {
            var buffer = msgProtobuf.StructToBytes(head);

            _tcp.Send(buffer);
            //if (_isConnected)
            //{
            //    //Debug.Log("send:" + _counter);
            //    Console.WriteLine("send:" + _counter);
            //    var data = BitConverter.GetBytes(_counter);
            //    _tcp.Send(data);
            //    _counter++;
            //    if (_counter > 1000)
            //        _isConnected = false;
            //}
        }


        void OnReceive(byte[] buffer)
        {
            if (buffer.Length==0)
            {
                Console.WriteLine("服务断开 启动重连！");

                return;
            }
            var head = new Head();
            head = (Head)msgProtobuf.BytesToStruct(buffer, head.GetType());

            Console.WriteLine("receive: " + head.HashCode);
        }

        void OnApplicationQuit()
        {
            _tcp.Dispose();
            //   _server.Close();
        }
    }
}
