using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Moose.Common
{
    public class TxtHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public void WriteMessage1()
        {
            FileStream _Fs = new FileStream("", FileMode.Create);

            byte[] _Data = Encoding.Default.GetBytes("测试代码！！！");

            _Fs.Write(_Data, 0, _Data.Length);

            _Fs.Flush();
            _Fs.Close();
        }

        public void WriteMessage2()
        {
            FileStream _Fs = new FileStream("",FileMode.Create);
            StreamWriter _Sw = new StreamWriter(_Fs);

            _Sw.WriteLine("测试代码！！！");
            _Sw.Flush();
            _Sw.Close();
            _Fs.Close();
        }

        public void WriteMessage3()
        {
            //List<string> _List = new List<string>()
            //{
            //    "111","222","333","444","555"
            //};

            //var _Value = _List.Select((x) =>
            //{
            //    return "111";
            //});

            using (StreamWriter sw = File.CreateText(""))
            {
                sw.WriteLine("Hello");
                sw.WriteLine("And");
                sw.WriteLine("Welcome");
            }

            using (StreamReader sr = File.OpenText(""))
            {
                //创建一个StreamWrite，用于UTF-8编码格式。

                //追加到现有文件或不存在文件（）,存在附加、不存在创建
                StreamWriter Sw1 = File.AppendText("");

                //创建或打开  用于写入，存在覆盖、不存在创建
                StreamWriter Sw2 = File.CreateText("");

                StreamReader Sr1 = File.OpenText("");

                Sw2.WriteLine("Hello World!!!");
                Sw2.Flush();
                Sw2.Close();

                FileStream fileStream = new FileStream("", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            }

            StreamWriter Sw3 = File.CreateText(@"C:\Users\Chen\Desktop\tt.txt");
            
        }
    }
}
