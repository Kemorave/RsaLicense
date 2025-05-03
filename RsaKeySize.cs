namespace RsaLicense
{
    /// <summary>
    /// Enumerates the possible sizes for RSA keys.
    /// </summary>
    public enum RsaKeySize
    {
        /// <summary>
        /// A low-security RSA key size, suitable for testing or development purposes.
        /// </summary>
        low = 1024,

        /// <summary>
        /// A commonly used RSA key size, providing a good balance between security and performance.
        /// </summary>
        common = 2048,

        /// <summary>
        /// A high-security RSA key size, suitable for production environments where security is a top priority.
        /// </summary>
        high = 3072,

        /// <summary>
        /// A very high-security RSA key size, suitable for extremely sensitive applications or environments.
        /// </summary>
        veryHigh = 4096
    }

}

