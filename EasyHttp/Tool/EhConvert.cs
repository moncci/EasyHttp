using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyHttp.Tool
{
    /// <summary>
    /// 转换工具
    /// </summary>
    internal class EhConvert
    {
        /// <summary>
        /// 返回字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetNoNullString(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return obj.ToString();
        }
        public static string GetNullableString(object obj)
        {
            if (obj == null || obj.ToString() == "")
            {
                return null;
            }
            return obj.ToString();
        }
        /// <summary>
        /// 返回整数型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetNoNullInt(object obj, int defaultValue)
        {
            return ObjectToInt(obj, defaultValue);
        }
        public static int GetNoNullInt32(object obj)
        {
            if (obj != null)
            {
                return GetNoNullInt(obj, 0);
            }
            return 0;
        }

        public static long GetNoNullLong(object obj)
        {
            if (obj != null)
            {
                return ObjectToLong(obj, 0);
            }
            return 0;
        }
        public static long? GetNullabelLong(object obj)
        {
            return ObjectToNullabelLong(obj);
        }
        public static int? GetNullabelInt(object obj)
        {
            return ObjectToNullabelInt(obj);
        }
        /// <summary>
        /// 返回时间
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime GetNoNullDateTime(object obj, DateTime defaultValue)
        {
            DateTime result;
            try
            {
                result = Convert.ToDateTime(obj);
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }
        public static DateTime GetNoNullDateTime(object obj)
        {
            return GetNoNullDateTime(obj, DateTime.Parse("1900-01-01"));
        }
        public static DateTime? GetNullabelDateTime(object obj)
        {
            DateTime? result;
            try
            {
                result = new DateTime?(Convert.ToDateTime(obj));
            }
            catch
            {
                result = null;
            }
            return result;
        }
        /// <summary>
        /// 返回小数型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal GetNoNullDecimal(object obj, decimal defaultValue)
        {
            if (obj == null)
            {
                return defaultValue;
            }
            decimal result;
            if (decimal.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }
        public static decimal GetNoNullDecimal(object obj)
        {
            return GetNoNullDecimal(obj, 0m);
        }
        public static decimal? GetNullabelDecimal(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            decimal value;
            if (decimal.TryParse(obj.ToString(), out value))
            {
                return new decimal?(value);
            }
            return null;
        }
        /// <summary>
        /// 返回逻辑型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool GetNoNullBool(object obj, bool defaultValue)
        {
            if (obj == null)
            {
                return defaultValue;
            }
            bool result;
            if (bool.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }
        public static bool GetNoNullBool(object obj)
        {
            return GetNoNullBool(obj, false);
        }
        public static bool? GetNullabelBool(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            bool value;
            if (bool.TryParse(obj.ToString(), out value))
            {
                return new bool?(value);
            }
            return null;
        }
        public static string ToDateString(object var)
        {
            return ToDateString(var, null);
        }
        public static string ToDateString(object var, string defaultValue)
        {
            string result = defaultValue;
            DateTime dateTime;
            if (var != null && DateTime.TryParse(var.ToString(), out dateTime))
            {
                result = dateTime.ToString("yyyy-MM-dd");
            }
            return result;
        }
        public static string ToDateTimeString(object var)
        {
            return ToDateTimeString(var, null);
        }
        public static string ToDateTimeString(object var, string defaultValue)
        {
            string result = defaultValue;
            DateTime dateTime;
            if (var != null && DateTime.TryParse(var.ToString(), out dateTime))
            {
                result = dateTime.ToString("yyyy-MM-dd hh:mm");
            }
            return result;
        }
        public static object GetDBNullValue(object obj)
        {
            if (obj == null)
            {
                return DBNull.Value;
            }
            return obj;
        }
        public static List<T> ToList<T>(DataTable dt) where T : class, new()
        {
            List<T> list = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                List<PropertyInfo> list2 = new List<PropertyInfo>();
                Type typeFromHandle = typeof(T);
                PropertyInfo[] properties = typeFromHandle.GetProperties();
                PropertyInfo[] array = properties;
                for (int i = 0; i < array.Length; i++)
                {
                    PropertyInfo propertyInfo = array[i];
                    if (dt.Columns.Contains(propertyInfo.Name))
                    {
                        list2.Add(propertyInfo);
                    }
                }
                list = new List<T>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    T t = Activator.CreateInstance<T>();
                    foreach (PropertyInfo current in list2)
                    {
                        if (dataRow[current.Name] != DBNull.Value)
                        {
                            current.SetValue(t, dataRow[current.Name], null);
                        }
                    }
                    list.Add(t);
                }
            }
            return list;
        }
        public static T ToClass<T>(DataRow row) where T : class, new()
        {
            T t = Activator.CreateInstance<T>();
            if (row != null)
            {
                List<PropertyInfo> list = new List<PropertyInfo>();
                Type typeFromHandle = typeof(T);
                PropertyInfo[] properties = typeFromHandle.GetProperties();
                PropertyInfo[] array = properties;
                for (int i = 0; i < array.Length; i++)
                {
                    PropertyInfo propertyInfo = array[i];
                    if (row.Table.Columns.Contains(propertyInfo.Name))
                    {
                        list.Add(propertyInfo);
                    }
                }
                foreach (PropertyInfo current in list)
                {
                    if (row[current.Name] != DBNull.Value)
                    {
                        current.SetValue(t, row[current.Name], null);
                    }
                }
            }
            return t;
        }
        public static DataTable ToDataTable<T>(IEnumerable<T> value) where T : class
        {
            List<PropertyInfo> list = new List<PropertyInfo>();
            Type typeFromHandle = typeof(T);
            DataTable dataTable = new DataTable();
            PropertyInfo[] properties = typeFromHandle.GetProperties();
            PropertyInfo[] array = properties;
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo propertyInfo = array[i];
                list.Add(propertyInfo);
                dataTable.Columns.Add(propertyInfo.Name, propertyInfo.PropertyType);
            }
            foreach (T current in value)
            {
                DataRow dataRow = dataTable.NewRow();
                PropertyInfo[] array2 = properties;
                for (int j = 0; j < array2.Length; j++)
                {
                    PropertyInfo propertyInfo2 = array2[j];
                    dataRow[propertyInfo2.Name] = propertyInfo2.GetValue(current, null);
                }
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }
        /// <summary>
        /// 将一个类型转换为另一个类型。
        /// </summary>
        /// <typeparam name="TValue">输出的类型。</typeparam>
        /// <param name="value">要转换的值。</param>
        /// <returns>返回 <see cref="!:TValue" /></returns>
        public static TValue ConvertTo<TValue>(object value)
        {
            if (value == null || DBNull.Value.Equals(value))
            {
                return default(TValue);
            }
            Type type = typeof(TValue);
            if (type.IsGenericType && type.IsValueType)
            {
                type = type.GetGenericArguments()[0];
            }
            if (type.IsEnum)
            {
                return (TValue)((object)System.Enum.Parse(type, value.ToString(), true));
            }
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    value = Convert.ToBoolean(value);
                    goto IL_1A4;
                case TypeCode.Char:
                    value = Convert.ToChar(value);
                    goto IL_1A4;
                case TypeCode.SByte:
                    value = Convert.ToSByte(value);
                    goto IL_1A4;
                case TypeCode.Byte:
                    value = Convert.ToByte(value);
                    goto IL_1A4;
                case TypeCode.Int16:
                    value = Convert.ToInt16(value);
                    goto IL_1A4;
                case TypeCode.UInt16:
                    value = Convert.ToUInt16(value);
                    goto IL_1A4;
                case TypeCode.Int32:
                    value = Convert.ToInt32(value);
                    goto IL_1A4;
                case TypeCode.UInt32:
                    value = Convert.ToUInt32(value);
                    goto IL_1A4;
                case TypeCode.Int64:
                    value = Convert.ToInt64(value);
                    goto IL_1A4;
                case TypeCode.UInt64:
                    value = Convert.ToUInt64(value);
                    goto IL_1A4;
                case TypeCode.Single:
                    value = Convert.ToSingle(value);
                    goto IL_1A4;
                case TypeCode.Double:
                    value = Convert.ToDouble(value);
                    goto IL_1A4;
                case TypeCode.Decimal:
                    value = Convert.ToDecimal(value);
                    goto IL_1A4;
                case TypeCode.DateTime:
                    value = Convert.ToDateTime(value);
                    goto IL_1A4;
                case TypeCode.String:
                    value = Convert.ToString(value);
                    goto IL_1A4;
            }
            value = Convert.ChangeType(value, type);
        IL_1A4:
            return (TValue)((object)value);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ConvertTo(Type type, object value)
        {
            if (value == null || DBNull.Value.Equals(value))
            {
                return null;
            }
            if (value.GetType().Equals(type))
            {
                return value;
            }
            if (type.IsGenericType && type.IsValueType)
            {
                type = type.GetGenericArguments()[0];
            }
            if (type.IsEnum)
            {
                return System.Enum.Parse(type, value.ToString(), true);
            }
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    value = Convert.ToBoolean(value);
                    return value;
                case TypeCode.Char:
                    value = Convert.ToChar(value);
                    return value;
                case TypeCode.SByte:
                    value = Convert.ToSByte(value);
                    return value;
                case TypeCode.Byte:
                    value = Convert.ToByte(value);
                    return value;
                case TypeCode.Int16:
                    value = Convert.ToInt16(value);
                    return value;
                case TypeCode.UInt16:
                    value = Convert.ToUInt16(value);
                    return value;
                case TypeCode.Int32:
                    value = Convert.ToInt32(value);
                    return value;
                case TypeCode.UInt32:
                    value = Convert.ToUInt32(value);
                    return value;
                case TypeCode.Int64:
                    value = Convert.ToInt64(value);
                    return value;
                case TypeCode.UInt64:
                    value = Convert.ToUInt64(value);
                    return value;
                case TypeCode.Single:
                    value = Convert.ToSingle(value);
                    return value;
                case TypeCode.Double:
                    value = Convert.ToDouble(value);
                    return value;
                case TypeCode.Decimal:
                    value = Convert.ToDecimal(value);
                    return value;
                case TypeCode.DateTime:
                    value = Convert.ToDateTime(value);
                    return value;
                case TypeCode.String:
                    value = Convert.ToString(value);
                    return value;
            }
            value = Convert.ChangeType(value, type);
            return value;
        }
        /// <summary>
        /// object型转换为bool型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool ObjectToBool(object obj)
        {
            return obj != null && StrToBool(GetNullableString(obj), false);
        }
        /// <summary>
        /// object型转换为bool型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool ObjectToBool(object obj, bool defValue)
        {
            if (obj != null)
            {
                return StrToBool(GetNullableString(obj), defValue);
            }
            return defValue;
        }
        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(string str)
        {
            return StrToBool(str, true);
        }
        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(string str, bool defValue)
        {
            if (str != null)
            {
                if (string.Compare(str, "true", true) == 0 || string.Compare(str, "1", true) == 0)
                {
                    return true;
                }
                if (string.Compare(str, "false", true) == 0 || string.Compare(str, "0", true) == 0)
                {
                    return false;
                }
            }
            return defValue;
        }
        /// <summary>
        ///             bool型转换为string型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string BoolToStr(bool expression)
        {
            if (expression)
            {
                return "1";
            }
            return "0";
        }
        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="obj">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ObjectToInt(object obj)
        {
            return ObjectToInt(obj, 0);
        }
        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="obj">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ObjectToInt(object obj, int defValue)
        {
            if (obj != null)
            {
                return StrToInt(GetNullableString(obj), defValue);
            }
            return defValue;
        }
        /// <summary>
        /// 将对象转换为long类型
        /// </summary>
        /// <param name="obj">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的long类型结果</returns>
        public static long ObjectToLong(object obj)
        {
            return ObjectToLong(obj, 0);
        }
        /// <summary>
        /// 将对象转换为long类型
        /// </summary>
        /// <param name="obj">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的long类型结果</returns>
        public static long ObjectToLong(object obj, long defValue)
        {
            if (obj != null)
            {
                return StrToLong(GetNullableString(obj), defValue);
            }
            return defValue;
        }
        /// <summary>
        /// 将字符转换为Int32类型,转换失败返回0
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(string str)
        {
            return StrToInt(str, 0);
        }

        /// <summary>
        /// 将字符转换为long类型,转换失败返回0
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的long类型结果</returns>
        public static long StrToLong(string str)
        {
            return StrToLong(str, 0);
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(string str, int defValue)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 11 || !Regex.IsMatch(str.Trim(), "^([-]|[0-9])[0-9]*(\\.\\w*)?$"))
            {
                return defValue;
            }
            int result;
            if (int.TryParse(str, out result))
            {
                return result;
            }
            return Convert.ToInt32(StrToFloat(str, (float)defValue));
        }

        /// <summary>
        /// 将对象转换为Long类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的long类型结果</returns>
        public static long StrToLong(string str, long defValue)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 20 || !Regex.IsMatch(str.Trim(), "^([-]|[0-9])[0-9]*(\\.\\w*)?$"))
            {
                return defValue;
            }
            long result;
            if (long.TryParse(str, out result))
            {
                return result;
            }
            return defValue;
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="obj">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int? ObjectToNullabelInt(object obj)
        {
            return StrToNullabelInt(GetNullableString(obj));
        }
        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int? StrToNullabelInt(string str)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 11 || !Regex.IsMatch(str.Trim(), "^([-]|[0-9])[0-9]*(\\.\\w*)?$"))
            {
                return null;
            }
            int value;
            if (int.TryParse(str, out value))
            {
                return new int?(value);
            }
            return new int?(Convert.ToInt32(StrToNullabelFloat(str)));
        }
        /// <summary>
        /// 将对象转换为long类型
        /// </summary>
        /// <param name="obj">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的long类型结果</returns>
        public static long? ObjectToNullabelLong(object obj)
        {
            return StrToNullabelLong(GetNullableString(obj));
        }
        /// <summary>
        /// 将对象转换为long类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的long类型结果</returns>
        public static long? StrToNullabelLong(string str)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 20 || !Regex.IsMatch(str.Trim(), "^([-]|[0-9])[0-9]*(\\.\\w*)?$"))
            {
                return null;
            }
            long value;
            if (long.TryParse(str, out value))
            {
                return new long?(value);
            }
            return new long?(Convert.ToInt64(StrToNullabelFloat(str)));
        }
        /// <summary>
        /// 对象型转换为float型
        /// </summary>
        /// <param name="obj">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float ObjectToFloat(object obj)
        {
            if (obj == null)
            {
                return 0f;
            }
            return StrToFloat(GetNullableString(obj), 0f);
        }
        /// <summary>
        /// 对象型转换为float型
        /// </summary>
        /// <param name="obj">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float ObjectToFloat(object obj, float defValue)
        {
            if (obj == null)
            {
                return defValue;
            }
            return StrToFloat(GetNullableString(obj), defValue);
        }
        /// <summary>
        /// string型转换为Double型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(string str)
        {
            if (str == null)
            {
                return 0f;
            }
            return StrToFloat(str.ToString(), 0f);
        }
        
        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(string str, float defValue)
        {
            if (str == null || str.Length > 10)
            {
                return defValue;
            }
            float result = defValue;
            if (str != null)
            {
                bool flag = Regex.IsMatch(str, "^([-]|[0-9])[0-9]*(\\.\\w*)?$");
                if (flag)
                {
                    float.TryParse(str, out result);
                }
            }
            return result;
        }
        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="obj">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float? ObjectToNullabelFloat(object obj)
        {
            return StrToNullabelFloat(GetNullableString(obj));
        }
        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float? StrToNullabelFloat(string str)
        {
            if (str == null || str.Length > 10)
            {
                return null;
            }
            if (str != null)
            {
                bool flag = Regex.IsMatch(str, "^([-]|[0-9])[0-9]*(\\.\\w*)?$");
                float value;
                if (flag && float.TryParse(str, out value))
                {
                    return new float?(value);
                }
            }
            return null;
        }
        /// <summary>
        /// object型转换为Double型
        /// </summary>
        /// <param name="obj">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的Double类型结果</returns>
        public static double ObjectToDouble(object obj)
        {
            return StrToDouble(GetNullableString(obj), 0.0);
        }
        /// <summary>
        /// object型转换为Double型
        /// </summary>
        /// <param name="obj">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的Double类型结果</returns>
        public static double ObjectToDouble(object obj, double defValue)
        {
            return StrToDouble(GetNullableString(obj), defValue);
        }
        /// <summary>
        /// string型转换为Double型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的Double类型结果</returns>
        public static double StrToDouble(string str)
        {
            if (str == null)
            {
                return 0.0;
            }
            return StrToDouble(str.ToString(), 0.0);
        }
        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static double StrToDouble(string str, double defValue)
        {
            if (str == null)
            {
                return defValue;
            }
            double result = defValue;
            if (str != null)
            {
                bool flag = Regex.IsMatch(str, "^([-]|[0-9])[0-9]*(\\.\\w*)?$");
                if (flag)
                {
                    double.TryParse(str, out result);
                }
            }
            return result;
        }
        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="obj">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static double? ObjectToNullabelDouble(object obj)
        {
            return StrToNullabelDouble(GetNullableString(obj));
        }
        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static double? StrToNullabelDouble(string str)
        {
            if (str == null)
            {
                return null;
            }
            if (str != null)
            {
                bool flag = Regex.IsMatch(str, "^([-]|[0-9])[0-9]*(\\.\\w*)?$");
                double value;
                if (flag && double.TryParse(str, out value))
                {
                    return new double?(value);
                }
            }
            return null;
        }

        #region 获取开始日期
        /// <summary>
        /// 获取开始日期
        /// </summary>
        /// <param name="strDateTimeRange">选择的日期范围</param>
        /// <returns>开始日期</returns>
        public static DateTime? GetDateTimeBeginByAdminLTE(string strDateTimeRange)
        {
            DateTime? date = null;
            if (!string.IsNullOrEmpty(strDateTimeRange))
            {
                if (strDateTimeRange.IndexOf(" - ") > 0)
                {
                    string tmp = strDateTimeRange.Replace(" - ", "@");
                    string[] dateArray = tmp.Split('@');
                    if (dateArray.Length > 0)
                    {
                        date = GetNullabelDateTime(dateArray[0]);
                    }
                }
            }
            return date;
        }
        #endregion

        #region 获取结束日期
        /// <summary>
        /// 获取结束日期
        /// </summary>
        /// <param name="strDateTimeRange">选择的日期范围</param>
        /// <returns>开始日期</returns>
        public static DateTime? GetDateTimeEndByAdminLTE(string strDateTimeRange)
        {
            DateTime? date = null;
            if (!string.IsNullOrEmpty(strDateTimeRange))
            {
                if (strDateTimeRange.IndexOf(" - ") > 0)
                {
                    string tmp = strDateTimeRange.Replace(" - ", "@");
                    string[] dateArray = tmp.Split('@');
                    if (dateArray.Length > 1)
                    {
                        date = GetNullabelDateTime(dateArray[1]);
                    }
                }
            }
            return date;
        }
        #endregion

        /// <summary>
        /// 返回Double
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double GetNoNullDouble(object obj, double defaultValue)
        {
            if (obj == null)
            {
                return defaultValue;
            }
            double result;
            if (double.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 返回Double
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double GetNoNullDouble(object obj)
        {
            return GetNoNullDouble(obj, 0d);
        }

        /// <summary>
        /// 返回Double
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double? GetNullabelDouble(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            double value;
            if (double.TryParse(obj.ToString(), out value))
            {
                return new double?(value);
            }
            return null;
        }
    }
}
