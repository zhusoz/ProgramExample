using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataSharing.Common
{
    /// <summary>
    /// 克隆的帮助类
    /// </summary>
    public static class CloneHelper
    {
        /// <summary>
        /// 克隆对象list，注意：要给引用类型实体加上[Serializable]特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> Clone<T>(this List<T> list)
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, list);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (List<T>)formatter.Deserialize(objectStream);
            }
        }
    }
}
