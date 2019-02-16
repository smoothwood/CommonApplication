using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class CommonHelper
    {
        public static string GetMD5HashedValue(this string RawString)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(RawString));
                return Encoding.ASCII.GetString(result);
            }
        }

        public static JObject GetJObject(this Stream stream)
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string jsonString = reader.ReadToEnd();
            JObject jsonObj = (JObject)JsonConvert.DeserializeObject(jsonString);
            return jsonObj;
        }
    }
}
