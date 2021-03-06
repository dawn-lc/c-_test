﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace c_____test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 生成GUID
        /// </summary>
        public static Guid GetRandomGuid()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// 生成GUID（HashCode）
        /// </summary>
        public static int GetRandomGuidHashCode()
        {
            return Guid.NewGuid().GetHashCode();
        }

        /// <summary>
        /// 生成GUID（String）
        /// </summary>
        public static string GetRandomGuidString()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 使用GUID生成真随机数
        /// </summary>
        public static string GetRandomByGuid(int a, int b)
        {
            Random random = new Random(GetRandomGuidHashCode());
            return random.Next(a, b).ToString();
        }

        ///<summary>
        ///生成随机字符串 
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字，true=包含，默认为包含</param>
        ///<param name="useLow">是否包含小写字母，true=包含，默认为不包含</param>
        ///<param name="useUpp">是否包含大写字母，true=包含，默认为包含</param>
        ///<param name="useSpe">是否包含特殊字符，true=包含，默认为不包含</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串</returns>
        public static string GetRandomString(int length=6, bool useNum=true, bool useLow=false, bool useUpp=true, bool useSpe=false, string custom="")
        {
            byte[] b = new byte[4];
            Random r = new Random(GetRandomGuidHashCode());
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }



        ///<summary>
        ///生成随机字符串 (用于图片验证码)
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字（排除0、1），true=包含，默认为包含</param>
        ///<param name="useLow">是否包含小写字母（排除i、l、o），true=包含，默认为不包含</param>
        ///<param name="useUpp">是否包含大写字母（排除I、L、O），true=包含，默认为包含</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串</returns>
        public static string GetRandomString(int length = 6, bool useNum = true, bool useLow = false, bool useUpp = true, string custom = "")
        {
            byte[] b = new byte[4];
            Random r = new Random(GetRandomGuidHashCode());
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghjkmnpqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHJKMNPQRSTUVWXYZ"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }

        /// <summary>
        /// 图像转换为字节数组
        /// </summary>
        public static byte[] BitmapToByte(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }

        /// 将 Stream 转成 byte[]
        public byte[] StreamToBytes(Stream stream)

        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        //将 Stream 写入文件
        public void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[] 
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 写入文件 
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// 将 byte[] 转成 Stream
        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary> 
        /// 将一个object对象序列化，返回一个byte[]         
        /// </summary> 
        /// <param name="obj">能序列化的对象</param>         
        /// <returns></returns> 
        public static byte[] ObjectToBytes(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter(); formatter.Serialize(ms, obj); return ms.GetBuffer();
            }
        }


        // <summary>
        /// 生成各种类型的验证码
        /// </summary>
        public static object GetVerificationCode(int a)
        {
            object VerificationCode_out = null;
            switch (a)
            {
                case 0:
                    VerificationCode_out = GetRandomByGuid(0, 9) + GetRandomByGuid(0, 9) + GetRandomByGuid(0, 9) + GetRandomByGuid(0, 9) + GetRandomByGuid(0, 9) + GetRandomByGuid(0, 9);
                    break;
                case 1:
                    //VerificationCode_out = GetImgVerificationCode();
                    break;
                default:
                    VerificationCode_out = "err";
                    break;
            }
            return VerificationCode_out;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DrawValidationCode c = new DrawValidationCode(CodeHeight:60, CodeFontMinSize: 26, CodeFontMaxSize:26,CodeRandomStringCount:50);
            FileStream t = new FileStream(@"C:\Users\lc990\Desktop\2.png", FileMode.Create);
            byte[] b = c.CreateImage(GetRandomString(6,true,false,true,""));
            t.Write(b, 0, b.Length);
            t.Close();

        }
    }
}
