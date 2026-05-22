using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputContactAttachment
    {
        [JsonIgnore]
        public string? VcfInfo { get { return Payload.VcfInfo; } }

        [JsonIgnore]
        public string? VcfHash { get { return Payload.VcfHash; } }

        /// <summary>
        /// Информация о пользователе
        /// </summary>
        [JsonIgnore]
        public ApiInputUser? MaxInfo => Payload.MaxInfo;

        [JsonInclude]
        [JsonPropertyName("payload")]
        internal PayloadData Payload { get; init; } = new();


        public bool IsVcfValid(IMaxBotClient maxBotClient)
        {
            if (string.IsNullOrWhiteSpace(VcfInfo) || string.IsNullOrWhiteSpace(VcfHash))
            {
                return false;
            }

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(maxBotClient.Token));
            var computedHash = BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(VcfInfo.Replace("\r\n", Environment.NewLine)))).Replace("-", "");

            return computedHash.Equals(VcfHash, StringComparison.OrdinalIgnoreCase);
        }

        public bool TryGetVcfPhone(IMaxBotClient maxBotClient, [MaybeNullWhen(false)] out string phone)
        {
            var isValid = IsVcfValid(maxBotClient);

            if (isValid)
            {
                //"BEGIN:VCARD\r\nVERSION:3.0\r\nPRODID:ez-vcard 0.10.3\r\nTEL;TYPE=cell:79990000000\r\nFN:Ivan Ivanov\r\nEND:VCARD\r\n"

                foreach (var line in VcfInfo!.Split("\r\n", StringSplitOptions.RemoveEmptyEntries))
                {
                    if (line.StartsWith("TEL;", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var tmp = line.Split(":").LastOrDefault();
                        if (!string.IsNullOrWhiteSpace(tmp))
                        {
                            phone = tmp;
                            return true;
                        }
                    }
                }
            }

            phone = default;
            return false;
        }

        internal class PayloadData
        {
            [JsonPropertyName("vcf_info")]
            public string? VcfInfo { get; set; }

            [JsonPropertyName("hash")]
            public string? VcfHash { get; set; }

            [JsonPropertyName("max_info")]
            public ApiInputUser? MaxInfo { get; set; }
        }
    }
}
