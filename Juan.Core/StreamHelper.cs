using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Image= System.Drawing.Image;

namespace Juan.Core
{
    public static class StreamHelper
    {
        // Methods
        public static byte[] ToByte(this Stream stream)
        {
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream.Seek(0L, SeekOrigin.Begin);
            return buffer;
        }

        public static Image ToImage(this Stream stream)
        {
            Image image = Image.FromStream(stream);
            stream.Seek(0L, SeekOrigin.Begin);
            return image;
        }

        public static byte[] ToResponseByte(this Stream stream)
        {
            int num;
            MemoryStream stream2 = new MemoryStream();
            byte[] buffer = new byte[0x10000];
            while ((num = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                stream2.Write(buffer, 0, num);
            }
            byte[] buffer2 = stream2.ToArray();
            stream2.Close();
            return buffer2;
        }

        public static string ToText(this Stream stream, Encoding encode = null)
        {
            if (encode == null)
            {
                encode = Encoding.UTF8;
            }
            using (StreamReader reader = new StreamReader(stream, encode))
            {
                return reader.ReadToEnd();
            }
        }
    }



}
