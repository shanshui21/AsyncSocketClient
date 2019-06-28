using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiSocket.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpSocketTest tcpSocketTest = new TcpSocketTest();
            tcpSocketTest.Init();
            tcpSocketTest.TestConnect();
            tcpSocketTest.TestSendReceive();


            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
