using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Narato.Common.Checksum
{
    public class ChecksumCalculator
    {
        public string CreateChecksum(object value)
        {
            var serializedObjectString = JsonConvert.SerializeObject(value);

            string hash;
            using (MD5 md5 = MD5.Create())
            {
                hash = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(serializedObjectString))).Replace("-", String.Empty);
            }

            return hash;
        }
    }
}
