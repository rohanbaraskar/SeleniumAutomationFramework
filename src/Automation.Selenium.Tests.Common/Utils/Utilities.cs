using System;
using Automation.Selenium.Tests.Common.Helpers;

namespace Automation.Selenium.Tests.Common.Utils
{
    public static class Utilities
    {
        /// <summary>
        ///     Trim and Lower the case of the text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string TrimAndToLower(string text)
        {
            return text != null ? text.Trim().ToLower() : null;
        }

        public static string GetRgbaColour(Constants.Colour colour)
        {
            switch (colour)
            {
                case Constants.Colour.Red:
                {
                    return Constants.RgbaColour.Red;
                }
            }
            return null;
        }

        /// <summary>
        ///     Convert a string to an enum (helpful when using dynamic create set)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ParseEnum<T>(string value)
        {
            return (T) Enum.Parse(typeof (T), value, true);
        }

        /// <summary>
        ///     Run an update SQL command against the database
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="connectionString"></param>
        public static void UpdateDatabase(string sqlcmd, string connectionString)
        {
            using (var sqlHelper = new SqlHelper(connectionString))
            {
                sqlHelper.ExecNonQuery(sqlcmd);
                sqlHelper.Dispose();
            }
        }

    }
}