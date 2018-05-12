namespace CRM.Defence
{
    /// <summary>
    /// Specified the allowed options for the X-XSS-Protect header.
    /// </summary>
    public enum XssProtectOption
    {
        /// <summary>
        /// Represents the option "1; mode=block" (recommended)
        /// </summary>
        EnabledWithModeBlock,
        /// <summary>
        /// Represents the option "1"
        /// </summary>
        Enabled,
        /// <summary>
        /// Represents the option "0" (not recommended)
        /// </summary>
        Disabled
    }
}