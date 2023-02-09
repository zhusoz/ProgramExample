using NPOI.HPSF;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Common.EncryptionToDecrypt
{
    public static class XORDEncrypt
    {
        /* 密钥 */
        private static byte KEY = 0X03;
        /// <summary>
        /// 异或加密
        /// </summary>
        /// <param name="oldStr">加密串</param>
        /// <returns></returns>
        public static string encrypt(string oldStr)
        {
            if (oldStr != null && oldStr.Trim().Length > 0)
            {
                byte[] bytes = null;
                try
                {
                    bytes = Encoding.UTF8.GetBytes(oldStr);
                }
                catch (UnsupportedEncodingException e)
                {
                    Console.WriteLine(e.Message);
                }
                StringBuilder buffer = new StringBuilder();
                String tmpStr;
                if (bytes != null)
                {
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        //加密
                        tmpStr = (bytes[i] & 0XFF ^ KEY).ToString("x2");
                        if (tmpStr.Length == 1)
                        {
                            buffer.Append("0").Append(tmpStr);
                        }
                        else
                        {
                            buffer.Append(tmpStr);
                        }
                    }
                }
                return buffer.ToString().ToUpper();
            }
            return oldStr;
        }

        /// <summary>
        /// 异或解密
        /// </summary>
        /// <param name="hexStr">待解密字符串</param>
        /// <returns></returns>
        public static string decrypt(string hexStr)
        {
            // 判断待解密字符串是否合法，长度非偶不处理
            if (hexStr != null && hexStr.Trim().Length > 0 && hexStr.Length % 2 == 0)
            {
                byte[] bytes = new byte[hexStr.Length / 2];
                for (int i = 0; i < hexStr.Length; i += 2)
                {
                    bytes[i / 2] = (byte)((byte)Convert.ToInt32(hexStr.Substring(i,2),16) & 0XFF ^ KEY);
                }
                try
                {
                    return Encoding.UTF8.GetString(bytes);
                }
                catch (UnsupportedEncodingException e)
                {
                    
                    Console.WriteLine(e.Message);

                    return hexStr;
                }
            }
            return hexStr;
        }

    }
}
