using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace CRM.Defence
{
    public class ProtectSqlIAndXssAttack : Attribute
    {
        private string pattern = @"<[^>]*>";

        // энкодер для категории, компании и статуса, заменяет символы из паттерна на null
        public string Encoder(string input)
        {
            if (input != null)
            {
                var value = Regex.Replace(input, pattern, String.Empty);
                return value;
            }
            return input;
            
        }
    }

    // 
    internal static class XssProtectHeader
    {
        
        private static string GetHeaderValue(XssProtectOption option)
        {
            switch (option)
            {
                case XssProtectOption.EnabledWithModeBlock:
                    return "1; mode=block";
                case XssProtectOption.Enabled:
                    return "1";
                case XssProtectOption.Disabled:
                    return "0";
                default:
                    throw new ArgumentOutOfRangeException(nameof(option));
            }
        }
    }
    internal static class HeaderConstants
    {
        public const string XssProtection = "X-Xss-Protection";
        public const string XFrameOptions = "X-Frame-Options";
        public const string StrictTransportSecurity = "Strict-Transport-Security";
        public const string Location = "Location";
        public const string XContentTypeOptions = "X-Content-Type-Options";
        public const string ContentSecurityPolicy = "Content-Security-Policy";
        public const string ContentSecurityPolicyReportOnly = "Content-Security-Policy-Report-Only";
    }
}