using Demo.CMD;

namespace Demo.CMD
{
    public static class ConsoleHelper
    {
        // int, DateTime
        public static string GetStringFromConsole(string fieldName)
        {
            Console.WriteLine($"Please enter {fieldName}");
            string value = Console.ReadLine();

            return value;
        }

        public static int GetIntFromConsole(string fieldName)
        {
            string value = GetStringFromConsole(fieldName);
            return int.Parse(value);
        }
        public static uint GetUIntFromConsole(string fieldName)
        {
            string value = GetStringFromConsole(fieldName);
            return uint.Parse(value);
        }

        public static DateTime GetDateTimeFromConsole(string fieldName)
        {
            string value = GetStringFromConsole(fieldName);
            return DateTime
                .ParseExact(value, ConsoleConstants.DatePattern, null);
        }
    }
}
