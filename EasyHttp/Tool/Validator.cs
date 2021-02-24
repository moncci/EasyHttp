using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyHttp.Tool
{
    /// <summary>
    /// 验证器
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// 检查一个字符串是否可以转化为日期，一般用于验证用户输入日期的合法性
        /// </summary>
        /// <param name="_value">需验证的字符串</param>
        /// <returns>是否可以转化为日期的bool值</returns>
        public static bool IsStringDate(string _value)
        {
            try
            {
                DateTime.Parse(_value);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 快速验证一个字符串是否符合制定的正则表达式
        /// </summary>
        /// <param name="_express">正则表达式的内容</param>
        /// <param name="_value">需验证的字符串</param>
        /// <returns>是否合法的bool值</returns>
        public static bool QuickValidate(string _express, string _value)
        {
            Regex regex = new Regex(_express);
            return _value.Length != 0 && regex.IsMatch(_value);
        }
        /// <summary>
        /// 检查一个字符串是否纯数字构成的，一般用于查询字符串参数的有效性验证
        /// </summary>
        /// <param name="_value">需验证字符串</param>
        /// <returns></returns>
        public static bool IsNumeric(string _value)
        {
            return Validator.QuickValidate("^[1-9]*[0-9]*$", _value);
        }
        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object expression)
        {
            return expression != null && Validator.IsNumeric(expression.ToString());
        }
        /// <summary>
        /// 检查一个字符串是否是纯字母和数字构成的，一般用于查询字符串参数的有效验证
        /// </summary>
        /// <param name="_value">需验证的字符串</param>
        /// <returns>是否合法的bool值</returns>
        public static bool IsLetterOrNamber(string _value)
        {
            return Validator.QuickValidate("^[a-zA-Z0-9]*$", _value);
        }
        /// <summary>
        /// 判断是否是数字，包括小数和整数
        /// </summary>
        /// <param name="_value">需验证的字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string _value)
        {
            return Validator.QuickValidate("^(0|([0-9]+[0-9]*))(.[0-9]+)?$", _value);
        }
        /// <summary>
        /// 判断一个字符串是否为邮件
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static bool IsEmail(string _value)
        {
            Regex regex = new Regex("^\\w+(-+.\\w+)*@(\\w+[-.]\\w+)*\\.)+([a-zA-Z]+)+$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }
        /// <summary>
        /// 判断是否是身份证号ID号
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static bool IsIDCard(string _value)
        {
            if (_value.Length != 15 && _value.Length != 18)
            {
                return false;
            }
            Regex regex;
            string[] array;
            bool result;
            if (_value.Length == 15)
            {
                regex = new Regex("^(\\d{6})(\\d{2})(=d{2})(\\d{2})(\\d{3})$");
                if (!regex.Match(_value).Success)
                {
                    return false;
                }
                array = regex.Split(_value);
                try
                {
                    new DateTime(int.Parse("19" + array[2]), int.Parse(array[3]), int.Parse(array[4]));
                    result = true;
                    return result;
                }
                catch
                {
                    result = false;
                    return result;
                }
            }
            regex = new Regex("^(\\d{6})(\\d{2})(=d{2})(\\d{2})(\\d{3})([0-9Xx])$");
            if (!regex.Match(_value).Success)
            {
                return false;
            }
            array = regex.Split(_value);
            try
            {
                new DateTime(int.Parse(array[2]), int.Parse(array[3]), int.Parse(array[4]));
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 判断一个字符串是否为Int
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static bool IsInt(string _value)
        {
            Regex regex = new Regex("^(-){0,1}\\d+$");
            return regex.Match(_value).Success && long.Parse(_value) <= 2147483647L && long.Parse(_value) >= -2147483648L;
        }
        /// <summary>
        /// 判断一个字符串是否为手机号码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsMobileNum(string _value)
        {
            Regex regex = new Regex("^13\\d{9}$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }
        /// <summary>
        /// 判断一个字符串是否为电话号码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsPhoneNum(string _value)
        {
            Regex regex = new Regex("^(86)?(-)?(0\\d{2,3})?(-)?(\\d{7,8})(-)?(\\d{3,5})?$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }
        /// <summary>
        /// 判断一个字符串是否为网址
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsUrl(string _value)
        {
            Regex regex = new Regex("(http://)?([\\w-]+\\.)*[\\w-]+(/[\\w- ./?%&=]*)?", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }
        /// <summary>
        /// 判断一个字符串是否为字母加数字
        /// Regex("[a-zA-Z0-9]?"
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsWordAndNum(string _value)
        {
            Regex regex = new Regex("[a-zA-Z0-9]?");
            return regex.Match(_value).Success;
        }
        /// <summary>
        /// 是否为Double类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsDouble(object expression)
        {
            return expression != null && Regex.IsMatch(expression.ToString(), "^([0-9])[0-9]*(\\.\\w*)?$");
        }
        /// <summary>
        /// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// </summary>
        /// <param name="strNumber">要确认的字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsNumericArray(string[] strNumber)
        {
            if (strNumber == null)
            {
                return false;
            }
            if (strNumber.Length < 1)
            {
                return false;
            }
            for (int i = 0; i < strNumber.Length; i++)
            {
                string value = strNumber[i];
                if (!Validator.IsNumeric(value))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
