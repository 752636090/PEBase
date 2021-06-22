using System;
using System.Buffers;
using System.Net.Sockets.Kcp;
using System.Text;
using System.Threading;
using UnityEngine;
using Random = System.Random;

namespace KCPTest
{
    public class KCPTest
    {
        private static string GetByteString(byte[] bytes)
        {
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str += $"\n      [{i}]:{bytes[i]}";
            }
            return str;
        }

        public static void Start()
        {
            const uint conv = 123;

            KCPItem kcpServer = new KCPItem(conv, "server");
            KCPItem kcpClient = new KCPItem(conv, "client");

            Random random = new Random();

            kcpServer.SendOutCallbak((Memory<byte> buffer) =>
            {
                kcpClient.InputData(buffer.Span);
            });
            kcpClient.SendOutCallbak((Memory<byte> buffer) =>
            {
                int next = random.Next(100);
                if (next >= 80) // 丢包率80%
                {
                    Debug.Log($"Send Package Success:{GetByteString(buffer.ToArray())}");
                    kcpServer.InputData(buffer.Span);
                }
                else
                {
                    Debug.Log("丢包");
                }
            });

            byte[] data = Encoding.ASCII.GetBytes("www.飞机.com");
            kcpClient.SendMsg(data);

            //while (true)
            //{
            //    kcpServer.Update();
            //    kcpClient.Update();
            //    Thread.Sleep(10);
            //}
            OnUpdate = () =>
            {
                kcpServer.Update();
                kcpClient.Update();
            };
        }

        private static Action OnUpdate;
        public static void Update()
        {
            OnUpdate?.Invoke();
        }
    }

    class KCPItem
    {
        public string ItemName;
        public KCPHandle Handle;
        public Kcp Kcp;

        
        /// <param name="conv">conversation id</param>
        public KCPItem(uint conv, string itemName)
        {
            Handle = new KCPHandle();
            Kcp = new Kcp(conv, Handle);
            Kcp.NoDelay(1, 10, 2, 1);
            Kcp.WndSize(64, 64);
            Kcp.SetMtu(512);

            ItemName = itemName;
        }

        public void InputData(Span<byte> data)
        {
            Kcp.Input(data);
        }

        public void SendOutCallbak(Action<Memory<byte>> itemSender)
        {
            Handle.Out = itemSender;
        }

        public void SendMsg(byte[] data)
        {
            Debug.Log($"{ItemName} 输入数据：{GetByteString(data)}");
            Kcp.Send(data.AsSpan());
        }

        public void Update()
        {
            Kcp.Update(DateTime.UtcNow);
            int len;
            while ((len = Kcp.PeekSize()) > 0)
            {
                byte[] buffer = new byte[len];
                if (Kcp.Recv(buffer) >= 0)
                {
                    Debug.Log($"{ItemName} 收到数据：{GetByteString(buffer)}");
                }
            }
        }

        private static string GetByteString(byte[] bytes)
        {
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str += $"\n      [{i}]:{bytes[i]}";
            }
            return str;
        }
    }

    class KCPHandle : IKcpCallback
    {
        public Action<Memory<byte>> Out;

        public void Output(IMemoryOwner<byte> buffer, int avalidLength)
        {
            using (buffer)
            {
                Out(buffer.Memory.Slice(0, avalidLength));
            }
        }
    }
}
