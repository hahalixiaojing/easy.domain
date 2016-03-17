using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Easy.Domain.Validators
{
    public static class Validators
    {
        /// <summary>
        /// 电子邮件格式验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean EmailValidate(String value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return true;
            }
            Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
            if (r.IsMatch(value))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 日期验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean DateTimeValidate(String value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return true;
            }
            DateTime dt;
            if (DateTime.TryParse(value, out dt))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 数字验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean NumberValidate(String value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return true;
            }
            Regex r = new Regex(@"^\+?\-?[0-9]*$");
            if (r.IsMatch(value))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 验证字符串中不应该包括的字符，如果包括了，则返回false
        /// </summary>
        /// <param name="value"></param>
        /// <param name="excludeChars"></param>
        /// <returns></returns>
        public static Boolean ExcludeCharsValidate(String value, Char[] excludeChars)
        {
            if (String.IsNullOrEmpty(value))
            {
                return true;
            }
            foreach (Char _character in value)
            {
                if (excludeChars.Contains(_character))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 验证字符串只包含的字符
        /// </summary>
        /// <param name="value"></param>
        /// <param name="includeChars"></param>
        /// <returns></returns>
        public static Boolean IncludeCharsValidate(String value, Char[] includeChars)
        {
            if (String.IsNullOrEmpty(value))
            {
                return true;
            }
            StringBuilder charStrings = new StringBuilder(); 
            foreach (Char c in includeChars)
            {
                charStrings.Append(c.ToString());
            }
            String regex = "^[" + charStrings + "]+$";

            return Regex.IsMatch(value, regex);
        }
        /// <summary>
        /// 数字范围验证
        /// </summary>
        /// <param name="value">要验证的值</param>
        /// <param name="left">起始范围</param>
        /// <param name="right">结束范围</param>
        /// <param name="boundary">是否包括边界</param>
        /// <returns></returns>
        public static Boolean NumberRangeValidate(Int32 value,Int32 left, Int32 right, Boolean boundary)
        {
            if (value > left && value < right)
            {
                return true;
            }
            if (boundary && (value == left || value == right))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 验证字符串的最大长度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static Boolean MaxStringLengthValidate(String value, Int32 maxLength)
        {
            if (String.IsNullOrEmpty(value))
            {
                return true;
            }
            if (value.Length <= maxLength)
            {
                return true;
            }
            return false;
        }
        public static Boolean EmptyValidate(String value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 日期范围验证，不包括时间
        /// </summary>
        /// <param name="value">验证的时间</param>
        /// <param name="begin">起始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="boundary">是否包括边界</param>
        /// <returns></returns>
        public static Boolean DateRange(DateTime value, DateTime begin, DateTime end, Boolean boundary)
        {
            if (value.Date > begin.Date && value.Date < end.Date)
            {
                return true;
            }
            if (boundary && (value.Date == begin.Date || value.Date == end.Date))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 验证字符串的长度，对于字符串的双字节字符，其长度为2
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean StringLengthValidate(String value,Int32 miniLength,Int32 maxLength)
        {
            if (String.IsNullOrEmpty(value))
            {
                return true;
            }
            Int32 strlen = 0;
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] strBytes = encoding.GetBytes(value);
            for (Int32 i = 0; i <= strBytes.Length - 1; i++)
            {
                if (strBytes[i] == 63)
                {//双字节都将编码为ASCII编码63
                    strlen++;
                }
                strlen++;
            }

            if (strlen < miniLength || strlen > maxLength)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 验证用户名(字母、数字、下划线、汉字组成并且不能以下划线结尾开头）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean UserNameValidate(String value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return true;
            }
            Regex r = new Regex("^(?!_)(?!.*?_$)[a-zA-Z0-9_\u4e00-\u9fa5]+$");
            if (r.IsMatch(value))
            {
                return true;
            }
            return false;
        }
    }
}
