using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace KCPNet
{
    public class KCPTool
    {
        public static Action<string> LogFunc;
        public static Action<ConsoleColor, string> ColorLogFunc;
        public static Action<string> WarningFunc;
        public static Action<string> ErrorFunc;

        public static void Log(string msg, params object[] args)
        {
            msg = string.Format(msg, args);
            if (LogFunc != null)
            {
                LogFunc(msg);
            }
            else
            {
                ConsoleLog(msg);
            }
        }
        public static void ColorLog(ConsoleColor color, string msg, params object[] args)
        {
            msg = string.Format(msg, args);
            if (ColorLogFunc != null)
            {
                ColorLogFunc(color, msg);
            }
            else
            {
                ConsoleLog(msg, color);
            }
        }
        public static void Warning(string msg, params object[] args)
        {
            msg = string.Format(msg, args);
            if (WarningFunc != null)
            {
                WarningFunc(msg);
            }
            else
            {
                ConsoleLog(msg, ConsoleColor.DarkYellow);
            }
        }
        public static void Error(string msg, params object[] args)
        {
            msg = string.Format(msg, args);
            if (ErrorFunc != null)
            {
                ErrorFunc(msg);
            }
            else
            {
                ConsoleLog(msg, ConsoleColor.DarkRed);
            }
        }
        private static void ConsoleLog(string msg, ConsoleColor color = ConsoleColor.Gray)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            msg = $"Thread:{threadId} {msg}";

            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static byte[] Serialize<T>(T msg) where T : KCPMsg
        {
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, msg);
                    ms.Seek(0, SeekOrigin.Begin);
                    return ms.ToArray();
                }
                catch (SerializationException e)
                {
                    Error("序列化失败：" + e.Message);
                    throw;
                }
            }
        }
        public static T Deserialize<T>(byte[] bytes) where T : KCPMsg
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    T msg = (T)bf.Deserialize(ms);
                    return msg;
                }
                catch (SerializationException e)
                {
                    Error($"反序列化失败：{e.Message} bytesLen:{bytes.Length}");
                    throw;
                }
            }
        }

        public static byte[] Compress(byte[] input) // 一般用第三方类库
        {
            using (MemoryStream outMs = new MemoryStream())
            {
                using (GZipStream gzs = new GZipStream(outMs, CompressionMode.Compress, true))
                {
                    gzs.Write(input, 0, input.Length);
                    gzs.Close();
                    return outMs.ToArray();
                }
            }
        }
        public static byte[] Decompress(byte[] input)
        {
            using (MemoryStream inputMs = new MemoryStream(input))
            {
                using (MemoryStream outMs = new MemoryStream())
                {
                    using (GZipStream gzs = new GZipStream(inputMs, CompressionMode.Decompress))
                    {
                        byte[] bytes = new byte[1024];
                        int len = 0;
                        while ((len = gzs.Read(bytes, 0, bytes.Length)) > 0)
                        {
                            outMs.Write(bytes, 0, len);
                        }
                        gzs.Close();
                        return outMs.ToArray();
                    } 
                }
            }
        }

        private static readonly DateTime utcStart = new DateTime(1970, 1, 1);
        public static ulong GetUtcStartMilliseconds()
        {
            TimeSpan timeSpan = DateTime.UtcNow - utcStart;
            return (ulong)timeSpan.TotalMilliseconds;
        }
    }
}
