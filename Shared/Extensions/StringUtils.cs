namespace Rental.WebApi.Shared.Extensions
{
    public static class StringUtils
    {
        public static string OnlyNumber(this string str, string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }
    }
}
