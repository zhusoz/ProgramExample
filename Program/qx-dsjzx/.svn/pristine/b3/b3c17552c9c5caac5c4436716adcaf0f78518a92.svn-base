using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.Common
{
    public static class DtConversionList<T> where T : new()
    {
        /// <summary>
        /// datatable转list
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertToModel( DataTable dt)
        {

            List<T> ts = new List<T>();// 定义集合
            Type type = typeof(T); // 获得此模型的类型
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                PropertyInfo[] propertys = t.GetType().GetProperties();// 获得此模型的公共属性
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    string propType = pi.PropertyType.FullName.ToLower();
                    if (dt.Columns.Contains(tempName))
                    {
                        if (!pi.CanWrite) continue;
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                        {
                            if (propType.Contains("int"))
                            {
                                if (propType.Contains("64"))
                                {
                                    pi.SetValue(t, Convert.ToInt64(value), null);
                                }
                                else
                                {
                                    if (value.ToString() == "")
                                    {
                                        pi.SetValue(t, 0, null);
                                    }
                                    else {
                                        int val = Convert.ToInt32(value);
                                        pi.SetValue(t, val, null);
                                    }
                                }
                            }
                            else if (propType.Contains("single"))
                            {
                                float val = Convert.ToSingle(value);
                                pi.SetValue(t, val, null);
                            }
                            else if (propType.Contains("datetime"))
                            {
                                string str = Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss");
                                DateTime val = Convert.ToDateTime(str);
                                pi.SetValue(t, val, null);
                            }
                            else if (propType.Contains("double"))
                            {
                                double val = Math.Round(Convert.ToDouble(value),2);
                                pi.SetValue(t, val, null);
                            }
                            else if (propType.Contains("bool"))
                            {
                                bool val = Convert.ToBoolean(value);
                                pi.SetValue(t, val, null);
                            }
                            else
                            {
                                pi.SetValue(t, value, null);
                            }
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
    }
}
