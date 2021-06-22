using System;
using System.Buffers;
using System.Collections.Generic;
using System.Net.Sockets.Kcp;
using System.Text;

namespace KCPNet
{
    public class KCPHandle : IKcpCallback
    {
        public Action<Memory<byte>> Out;

        public void Output(IMemoryOwner<byte> buffer, int avalidLength)
        {
            using (buffer)
            {
                Out(buffer.Memory.Slice(0, avalidLength));
            }
        }

        public Action<byte[]> OnReceive;
        public void Receive(byte[] buffer)
        {
            OnReceive?.Invoke(buffer);
        }
    }
}