using Bigon.Infrastructure.Services.Abstracts;
using Bigon.Infrastructure.Services.Configurations;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Bigon.Infrastructure.Services.Concrates
{
    public class CryptoService : ICryptoService
    {
        private readonly CryptoOptions options;

        public CryptoService(IOptions<CryptoOptions> options)
        {
            this.options = options.Value;
        }

        public string ToMd5(string text)
        {
            using (var provider = MD5.Create())
            {
                byte[] buffer = Encoding.UTF8.GetBytes($"{options.SaltKey}!{text}#2o2e_2)@3");
                byte[] hashedBuffer = provider.ComputeHash(buffer);

                return string.Join("", hashedBuffer.Select(b => b.ToString("x2")));
            }
        }

        public string ToSha1(string text)
        {
            using (var provider = SHA1.Create())
            {
                byte[] buffer = Encoding.UTF8.GetBytes($"{options.SaltKey}!{text}#2o2e_2)@3");
                byte[] hashedBuffer = provider.ComputeHash(buffer);

                return string.Join("", hashedBuffer.Select(b => b.ToString("x2")));
            }
        }

        public string Encrypt(string text, bool appliedUrlEncode = false)
        {
            using (var provider = new TripleDESCryptoServiceProvider())
            using (var md5 = MD5.Create())
            {
                try
                {
                    var keyBuffer = md5.ComputeHash(Encoding.UTF8.GetBytes($"#{options.SymmetricKey}!2023"));
                    var ivBuffer = md5.ComputeHash(Encoding.UTF8.GetBytes($"2023@{options.SymmetricKey}$"));

                    var transform = provider.CreateEncryptor(keyBuffer, ivBuffer);

                    using (var ms = new MemoryStream())
                    using (var cs = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                    {
                        var textBuffer = Encoding.UTF8.GetBytes(text);

                        cs.Write(textBuffer, 0, textBuffer.Length);
                        cs.FlushFinalBlock();

                        ms.Position = 0;
                        var result = new byte[ms.Length];

                        ms.Read(result, 0, result.Length);

                        var chiperText = Convert.ToBase64String(result);

                        if (appliedUrlEncode)
                        {
                            return HttpUtility.UrlEncode(chiperText);
                        }

                        return chiperText;
                    }
                }
                catch
                {
                    return "";
                }
            }
        }

        public string Decrypt(string text)
        {
            using (var provider = new TripleDESCryptoServiceProvider())
            using (var md5 = MD5.Create())
            {
                try
                {
                    var keyBuffer = md5.ComputeHash(Encoding.UTF8.GetBytes($"#{options.SymmetricKey}!2023"));
                    var ivBuffer = md5.ComputeHash(Encoding.UTF8.GetBytes($"2023@{options.SymmetricKey}$"));

                    var transform = provider.CreateDecryptor(keyBuffer, ivBuffer);

                    using (var ms = new MemoryStream())
                    using (var cs = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                    {
                        var textBuffer = Convert.FromBase64String(text);

                        cs.Write(textBuffer, 0, textBuffer.Length);
                        cs.FlushFinalBlock();

                        ms.Position = 0;
                        var result = new byte[ms.Length];

                        ms.Read(result, 0, result.Length);
                        return Encoding.UTF8.GetString(result);
                    }
                }
                catch
                {
                    return "";
                }
            }
        }
    }
}
