using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Utils
{
    public static class StringUtils
    {
        public static string Truncate(this string s/*requires: s != null*/, int length/*can exceed s.Length*/)
        {
            return length < 0 ? s : s.Substring(0, Math.Max(Math.Min(length, s.Length) - 1, 0));
        }

        static CultureInfo culture = new CultureInfo("en");//System.Threading.Thread.CurrentThread.CurrentCulture;

        public static string SentenceCase(this string s)
            // Doesn't know about proper nouns
        {
            return string.IsNullOrEmpty(s) ? s : culture.TextInfo.ToUpper(s[0]) + (s.Length == 1 ? "" : culture.TextInfo.ToLower(s.Substring(1)));
        }

        public static string FirstLower(this string s)
        {
            return string.IsNullOrEmpty(s) ? s : culture.TextInfo.ToLower(s[0]) + (s.Length == 1 ? "" : s.Substring(1));
        }

        public static string TitleCase(this string s)
        {
            return culture.TextInfo.ToTitleCase(s);
        }

        public static string OrDefault(this string s, string @default)
        {
            return string.IsNullOrEmpty(s) ? @default : s;
        }


        public static Guid GuidOrEmpty(this string s)
        {
            try
            {
                if (string.IsNullOrEmpty(s))
                    return Guid.Empty;
                return new Guid(s);
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public static string PropertyName(this string p)
        {
            return p.Replace('_', ' ');
        }
        public static string PropertyName<E>(E p) where E : struct, IConvertible
        {
            return p.ToString().Replace('_', ' ');
        }

        public static string RemovePre(this string str, string prefix)
        {
            return prefix != null && str.StartsWith(prefix) ? str.Substring(prefix.Length) : str;
        }

        public static string RemovePost(this string str, string suffix)
        {
            return suffix != null && str.EndsWith(suffix) ? str.Remove(str.Length - suffix.Length) : str;
        }

        static string[] numbers = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve" };
        public static string Number(this int n)
        {
            if (n > 12)
                return n.ToString();
            else
                return numbers[n];
        }
        public static string Number(this int n, bool titleCase)
        {
            return titleCase ? TitleCase(Number(n)) : Number(n);
        }
        static string[] numbersadj = new string[] { "zeroth", "first", "second", "third", "fourth", "fifth", "sixth", "seventh", "eighth", "nineth", "tenth", "eleventh", "twelth", "hundredth", "thousandth", "millionth", "billionth" };
        public static string NumberAdj(this int n)
        {
            string N;
            char n1;
            if (n < numbersadj.Length)
                return numbersadj[n];
            else
                return (N = n.ToString()) + ((n1 = N[N.Length - 1]) == '2' ? "nd" : n1 == '3' ? "rd" : "th");
        }
        public static string NumberAdj(this int n, bool titleCase)
        {
            return titleCase ? TitleCase(NumberAdj(n)) : NumberAdj(n);
        }
        static string[] numbersadv = new string[] { "never", "once", "twice" };
        public static string NumberAdv(this int n)
        {
            if (n < numbersadv.Length)
                return numbersadv[n];
            else
                return Number(n) + " times";
        }
        public static string NumberAdv(this int n, bool titleCase)
        {
            return titleCase ? TitleCase(NumberAdv(n)) : NumberAdv(n);
        }
        public static string Pl(this int n, string s)
        {
            return Pl(n, s, "");
        }
        public static string Pl(this int n, string pl, string sng)
        {
            return n > 1 ? pl : sng;
        }

        public static Regex FilterRegex(string filter)
        {
            filter = filter.Replace(".", @"\.").Replace("*", ".*").Replace(").*", ")*");
            if (filter.Contains("%"))
                filter = filter.Replace("%", @"\.");
            else if (!filter.TrimStart().StartsWith("^"))
                filter = @"(?=.*\.(?=[^.]*$))?.*" + filter + "[^.]*$";
            return new Regex(filter, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /*public static OpenFunction<string,string>.Signature Replace(string match, string substitute)
        {
            return delegate(string s) { return s.Replace(match, substitute); };
        }

        public static OpenFunction<Match, string>.Signature Replace(string substitute)
        {
            return delegate(Match m) { return m.Result(substitute); };
        }*/

        public static string NsUnescape(this string p)
        {
            return (p ?? "").Replace("__", "-").Replace("_.", " ").Replace("_", " ");
        }

        public static string NsEscape(this string p)
            // this does not invert NsUnescape in the case of nulls or namespaces ending in _, which makes them become part of the apparent class name
        {
            return (p ?? "").Replace(" ", "_").Replace("-", "__");
        }

        public static string ToString(this object o, string p)
        {
            string s;
            return o == null ? p : string.IsNullOrEmpty(s = o.ToString()) ? p : s;
        }

    }
}
