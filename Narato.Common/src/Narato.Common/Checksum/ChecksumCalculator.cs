using Narato.Common.Interfaces;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Narato.Common.Checksum
{
    public class ChecksumCalculator : IChecksumCalculator
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
