using System.Text;

namespace DeveloperSample.Algorithms
{
    // I'm only making existing tests to pass, no handling for edge cases, integer overflow, etc.
    // I assume adding test cases and adjusting the implementation accordingly will be the part of the 
    // follow-up technical interview...
    public static class Algorithms 
    {
        public static int GetFactorial(int n)
        {
            var result = 1;
            for (var i = 2; i <= n; i++)
            {
                result *= i;
            }

            return result;
        }

        public static string FormatSeparators(params string[] items)
        {
            var result = new StringBuilder(items[0]);
            for (var i = 1; i < items.Length - 1; i++)
            {
                result.Append(", ").Append(items[i]);
            }

            result.Append(" and ").Append(items[^1]);
            return result.ToString();
        }
    }
}