using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
namespace RsaLicense
{
    public class RdaLicenseValidator
    {
        public static bool ValidateLicense(string licenseJson, string publicKey, out LicenseData licenseData)
        {
            if (string.IsNullOrEmpty(licenseJson))
            {
                throw new ArgumentException($"'{nameof(licenseJson)}' cannot be null or empty.", nameof(licenseJson));
            }

            if (string.IsNullOrEmpty(publicKey))
            {
                throw new ArgumentException($"'{nameof(publicKey)}' cannot be null or empty.", nameof(publicKey));
            }

            licenseData = null;

            var package = JsonSerializer.Deserialize<LicensePackage>(licenseJson)!;
            var payloadBytes = Convert.FromBase64String(package.payload);
            var signatureBytes = Convert.FromBase64String(package.signature);

            using var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);

            bool isValid = rsa.VerifyData(payloadBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            if (!isValid) return false;

            licenseData = JsonSerializer.Deserialize<LicenseData>(Encoding.UTF8.GetString(payloadBytes));
            return licenseData?.Expiry > DateTime.UtcNow;
        }
    }

}

