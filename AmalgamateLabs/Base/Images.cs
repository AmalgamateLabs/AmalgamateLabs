using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace AmalgamateLabs.Base
{
    public class Images
    {
        // www.binaryintellect.net/articles/e6d71127-1f12-4555-879b-6a859947eafa.aspx
        public static byte[] ConvertImageToBytes(string imagePath)
        {
            byte[] imageByteData = File.ReadAllBytes(imagePath);

            return imageByteData;
        }

        public static byte[] ConvertListOfImagesToArrayOfImages(List<byte[]> images)
        {
            // Convert list of byte arrays in string array.
            string[] arr = images.Select(ai => Convert.ToBase64String(ai)).ToArray();

            // Convert object to byte array.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, arr);

                return memoryStream.ToArray();
            }
        }

        public static List<byte[]> ConvertArrayOfImagesToListOfImages(byte[] images)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Convert byte array to object.
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                memoryStream.Write(images, 0, images.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);
                object obj = binaryFormatter.Deserialize(memoryStream);

                // Convert object to string array.
                string[] arr = ((IEnumerable)obj).Cast<object>().Select(x => x.ToString()).ToArray();

                // Convert string array to list of byte arrays.
                return arr.Select(ap => Convert.FromBase64String(ap)).ToList();
            }
        }
    }
}
