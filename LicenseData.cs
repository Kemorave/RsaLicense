using System;
namespace RsaLicense
{
    public class LicenseData
    {
        public string LicenseId { get; set; }         // Unique license key identifier (GUID)
        public string IssuedTo { get; set; }          // User's email or name
        public DateTime IssuedAt { get; set; }        // When it was issued
        public DateTime Expiry { get; set; }          // Expiration date
        public string LicenseType { get; set; }       // e.g. trial, standard, pro
        public string[] Features { get; set; }        // e.g. ["offline", "pro-analytics"]

        public string DeviceHash { get; set; }        // (Optional) Hashed device ID for binding
        public string AppId { get; set; }             // App identifier (for multi-app licenses)
        public string MinimumVersion { get; set; }           // Licensed app version
    }

}

