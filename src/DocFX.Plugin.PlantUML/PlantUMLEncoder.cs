using System;
using System.IO;
using System.IO.Compression;

namespace docfx_plantuml
{
    public static class PlantUMLEncoder
    {
        /// <summary>
        /// Encodes the plant uml.
        /// </summary>
        /// <param name="plantUML">The plant uml.</param>
        /// <returns></returns>
        public static string EncodePlantUML(this string plantUML)
        {
            // TODO: Remove @startuml and @enduml tags if they exist

            // Strip out \r and any newline at end of string
            plantUML = plantUML.Replace("\r\n", "\n").TrimEnd('\n');
            
            using (MemoryStream output = new MemoryStream())
            {
                using (DeflateStream deflate = new DeflateStream(output, CompressionMode.Compress))
                {
                    using (StreamWriter writer = new StreamWriter(deflate, System.Text.Encoding.ASCII))
                    {
                        writer.Write(plantUML);
                    }
                }                

                return ToBase64String(output.ToArray());
            }
        }

        private static string ToBase64String(byte[] inArray)
        {
            var base64 = string.Empty;
            for (int i = 0; i < inArray.Length; i += 3)
            {
                if (i + 2 == inArray.Length)
                {
                    base64 += Append3bytes(inArray[i], inArray[i + 1], 0);
                }
                else if (i + 1 == inArray.Length)
                {
                    base64 += Append3bytes(inArray[i], 0, 0);
                }
                else
                {
                    base64 += Append3bytes(inArray[i], inArray[i + 1], inArray[i + 2]);
                }
            }
            return base64;
        }

        private static string Append3bytes(int b1, int b2, int b3)
        {
            var c1 = b1 >> 2;
            var c2 = ((b1 & 0x3) << 4) | (b2 >> 4);
            var c3 = ((b2 & 0xF) << 2) | (b3 >> 6);
            var c4 = b3 & 0x3F;

            var bytes = string.Empty;
            bytes += Encode6bit(c1 & 0x3F);
            bytes += Encode6bit(c2 & 0x3F);
            bytes += Encode6bit(c3 & 0x3F);
            bytes += Encode6bit(c4 & 0x3F);

            return bytes;
        }

        private static string Encode6bit(int b)
        {
            if (b < 10)
            {
                return Char.ConvertFromUtf32(48 + b);
            }
            b -= 10;
            if (b < 26)
            {
                return Char.ConvertFromUtf32(65 + b);
            }
            b -= 26;
            if (b < 26)
            {
                return Char.ConvertFromUtf32(97 + b);
            }
            b -= 26;
            if (b == 0)
            {
                return "-";
            }
            if (b == 1)
            {
                return "_";
            }
            return "?";
        }
    }
}
