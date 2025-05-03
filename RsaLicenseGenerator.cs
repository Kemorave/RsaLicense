using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
namespace RsaLicense
{
    public class RsaLicenseGenerator
    {
        public RsaLicenseGenerator() { }


        public static RsaKeyData GenerateKeyPair(RsaKeySize rsaKeySize)
        {
            int keySize = (int)rsaKeySize;
            using (var rsa = RSA.Create(keySize))
            {
                var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
                var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());

                return new RsaKeyData()
                {
                    PrivateKey = privateKey,
                    PublicKey = publicKey,
                    KeySize = rsaKeySize
                };
            }
        }
        public string GenerateSignedLicense(LicenseData data, string base64PrivateKey)
        {
            var json = JsonSerializer.Serialize(data);
            byte[] dataBytes = Encoding.UTF8.GetBytes(json);

            using var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(base64PrivateKey), out _);
            var signature = rsa.SignData(dataBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            var licensePackage = new LicensePackage
            {
                payload = Convert.ToBase64String(dataBytes),
                signature = Convert.ToBase64String(signature)
            };

            return JsonSerializer.Serialize(licensePackage);
        }

    }

}

