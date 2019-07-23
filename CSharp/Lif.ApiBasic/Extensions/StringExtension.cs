namespace Lif.ApiBasic.Extensions
{
    public static class StringExtension
    {
        public static string ToCamelCase(this string s)
        {
            if (string.IsNullOrEmpty(s) || !char.IsUpper(s[0]))
            {
                return s;
            }

            var chars = s.ToCharArray();

            for (var i = 0; i < chars.Length; i++)
            {
                if (i == 1 && !char.IsUpper(chars[i]))
                {
                    break;
                }

                var hasNext = (i + 1 < chars.Length);
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                {
                    if (char.IsSeparator(chars[i + 1]))
                    {
                        chars[i] = chars[i].ToLower();
                    }

                    break;
                }

                chars[i] = chars[i].ToLower();
            }

            return new string(chars);
        }

        public static char ToLower(this char c)
        {
            c = char.ToLowerInvariant(c);
            return c;
        }
    }
}
