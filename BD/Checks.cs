using System.Text.RegularExpressions;

namespace BD
{
    public class Checks
    {
        static public bool DateCheck(string date)
        {
            if (DateTime.TryParse(date, out DateTime result))
            {
                return true;
            }
            return false;

        }

        static public bool NameCheck(string str)
        {
            if (Regex.IsMatch(str, @"^[a-zA-ZА-Яа-я]+$"))
            {
                return true;
            }
            return false;

        }

        static public bool IntCheck(string number)
        {
            if (int.TryParse(number, out int result))
            {
                if (result >= 0)
                    return true;
            }
            return false;

        }

        static public bool DoubleCheck(string number)
        {
            if (double.TryParse(number, out double result))
            {
                if (result >= 0)
                    return true;
            }
            return false;

        }
    }
}