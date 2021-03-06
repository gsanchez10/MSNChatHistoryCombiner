﻿using System;
using System.Text.RegularExpressions;

namespace MsnHistoryCore
{
    public static class MsnExtensions
    {
        public static string ToMsnUniversalString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffK");
        }

        private const string MSN_FILE_PATTERN = @"^(?<name>[\w\.-]+)\s*(-\s*Archive(\s*\(\d*\))?)?\.xml$";
        public static bool IsSameFriendHistory(this string fileName, string otherFileName)
        {
            var regex = new Regex(MSN_FILE_PATTERN, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var leftMatch = regex.Match(fileName);
            var rightMatch = regex.Match(otherFileName);
            var result = false;

            if (leftMatch.Success && rightMatch.Success)
            {
                var leftUserName = leftMatch.Groups["name"].Value;
                var rightUserName = rightMatch.Groups["name"].Value;

                if (string.Compare(leftUserName, rightUserName, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public static string GetHistoryFileUniqueName(this string fileName)
        {
            var regex = new Regex(MSN_FILE_PATTERN, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var match = regex.Match(fileName);

            if (match.Success)
            {
                return match.Groups["name"].Value + ".xml";
            }

            return string.Empty;
        }

        public static bool IsMatchedMsnHistoryFile(this string fileName)
        {
            var regex = new Regex(MSN_FILE_PATTERN, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var match = regex.Match(fileName);

            return match.Success;

        }
    }
}
