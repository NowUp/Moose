using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.IO;

using static System.Console;
using System.Net;
using System.Threading;
using System.Net.NetworkInformation;

using System.ComponentModel;

namespace Moose
{
    class Program
    {
        static void Main(string[] args)
        {
            DictionaryTest();

            //修改测试
            lstComputer = new List<string>();
            //IP();
            Ping ping = new Ping();
            PingReply result = ping.Send("192.168.1.11");

            if (result.Status == IPStatus.Success)
            {
                WriteLine("连接成功");
                IPHostEntry iPHostEntry =  Dns.GetHostEntry("192.168.1.11");
                WriteLine(iPHostEntry.HostName);
            }

            ReadLine();

            //ImageTest();
            //HttpClientSample httpClientSample = new HttpClientSample();
            //httpClientSample.GetDataSimpleAsync().Wait();

            //Console.WriteLine("单精度浮点数Float：最大值：{0}，最小值：{1}", float.MaxValue, float.MinValue);

            //Console.WriteLine("双精度浮点数Double：最大值：{0}，最小值：{1}", double.MaxValue, double.MinValue);

            //Console.WriteLine("十进制数Decimal：最大值：{0}，最小值：{1}", decimal.MaxValue, decimal.MinValue);
            //decimal de1 = 111M;

            //if (double.MaxValue > float.MaxValue)
            //{
            //    Console.WriteLine("Double>Float");
            //}
            
            //List<test> tests = new List<test>()
            //    {
            //        new test(){ x = "2018-05-22  05:39:58.076739",y = "43 "},
            //        new test(){ x = "2018-05-22  05:39:58.076739",y = "43 "},
            //        new test(){ x = "2018-05-22  05:39:58.076739",y = "43 "},
            //        new test(){ x = "2018-05-22  05:39:58.076739",y = "43 "}
            //    };

            //DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(test));
            //MemoryStream msObj = new MemoryStream();
            ////将序列化之后的Json格式数据写入流中
            //js.WriteObject(msObj, tests);
            //msObj.Position = 0;
            ////从0这个位置开始读取流中的数据
            //StreamReader sr = new StreamReader(msObj, Encoding.UTF8);
            //string json = sr.ReadToEnd();
            //sr.Close();
            //msObj.Close();
            //Console.WriteLine(json);

            //Relations.Common.JsonHepler jsonHelper = new Relations.Common.JsonHepler();
            //string s1 = jsonHelper.ListToJson<test>(tests);

            //JavaScriptSerializer js = new JavaScriptSerializer();
            //string jsonData = js.Serialize(tests);//序列化
            //Console.WriteLine(jsonData);

            Console.ReadKey();

        }

        static void ImageTest()
        {
            string[] Names;
            string[] Files;

            Files = System.IO.Directory.GetFiles(@"E:\Relations文档\图像识别资料\20180319\汇总识别后\2（173个 58-230）\排序后\排序小");
            for (int i = 0; i < Files.Length; i++)
            {

                System.IO.File.Copy(Files[i], @"E:\Relations文档\图像识别资料\20180319\汇总识别后\2（173个 58-230）\排序后\test\" + i.ToString().PadLeft(3, '0') + ".jpg",true);
            }

            Files = System.IO.Directory.GetFiles(@"E:\Relations文档\图像识别资料\20180319\汇总识别后\2（173个 58-230）\排序后\test");

            Console.WriteLine("共统计：{0}！！！",Files.Length);
            string temp = string.Empty;
            double dtemp ;
            digitOCR.ditgitOCR dto = new digitOCR.ditgitOCR();

            for (int i = 0; i < Files.Length; i++)
            {
                try
                {
                    Names = Files[i].Split('\\');

                    temp = dto.Recognize(Files[i]);

                    if (double.TryParse(temp, out dtemp))
                    {
                        Console.WriteLine("{0}:{1}",Names[Names.Length-1],dtemp);
                    }
                    else
                    {
                        Console.WriteLine("非数字");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            
            }


         
        }

        static void ByteConvert()
        {
            byte[] Bytes = new byte[] { 12,03 ,07 ,47 };


            string SerialNumber = string.Empty;
            SerialNumber += Bytes[0].ToString().PadLeft(2, '0');
            SerialNumber += Bytes[1].ToString().PadLeft(2, '0');
            SerialNumber += Bytes[2].ToString().PadLeft(2, '0');
            SerialNumber += Bytes[3].ToString().PadLeft(2, '0');

            Console.WriteLine(SerialNumber);
        }

        static void ConvertSysten()
        {
            int _Number = 0xFF;

            Console.WriteLine(Convert.ToString(_Number, 2));

            Console.WriteLine(Convert.ToString(_Number, 8));

            Console.WriteLine(Convert.ToString(_Number, 10));

            Console.WriteLine(Convert.ToString(_Number, 16));

            Console.WriteLine(Convert.ToInt32("11111111", 2));
        }

        static void TesetIsAs()
        {
            object obj = new test();
            if (obj is test)
            {
                test a = obj as test;
            }
        }

        static void  Switch()
        {
            object a = "123";
            switch (a)
            {
                case int b when b < 100:

                    break;
                case int b:

                    break;
            }
            string a2 = null;

            string a1 = a2 ?? "1";
        }

        private static List<string> lstComputer;
        static void IP()
        {
            string gate = "192.168.1.";//网关
            for (int index = 190; index <= 200; index++)
            {
                IPAddress address = IPAddress.Parse(gate + index.ToString());
                Thread thread = new Thread(new ThreadStart(
                //匿名方法
                delegate ()
     {
         Ping ping = new Ping();
         PingReply result = ping.Send(address);
         if (result.Status == IPStatus.Success)
         {
             lock (lstComputer)//防止2个线程去ping同一个IP，节约时间
             {
                 lstComputer.Add(Dns.GetHostEntry(address).HostName + " : " + address.ToString());//ping通的结果保存到ListBox中，即在线的人
             }
         }
     }
                ));
                thread.IsBackground = true;//设为后台线程
                thread.Start();//启动线程
            }
        }

        static void DictionaryTest()
        {
            Dictionary<string, byte> CommandWord = new Dictionary<string, byte>()
            {

            };
            CommandWord.Add("test1", 11);
            CommandWord.Add("test2", 22);
            CommandWord.Add("test3", 33);
            CommandWord.Add("test4", 44);

            byte b =  CommandWord["test1"];
            Dictionary<Command, byte> CommandWord1 = new Dictionary<Command, byte>();

            CommandWord1.Add(Command.ReadSamplingValue,0x34);
            CommandWord1.Add(Command.ReadConcentrationValue, 0x35);
            CommandWord1.Add(Command.ReadConcentrationValueCalibration, 0x36);

            foreach (KeyValuePair<Command, byte> kvp in CommandWord1)
            {
                Console.WriteLine("Index {0} Value:{1}", kvp.Key, kvp.Value);
            }


            foreach (KeyValuePair<Command, byte> kvp in CommandWord1)
            {

                if (kvp.Value == 0x34)
                {
                    Console.WriteLine(kvp.Key);
                    break;
                }
            }

            Command command = Command.ReadConcentrationValue;
            switch (command)
            {
                case Command.ReadConcentrationValue:

                    byte byte1 =  CommandWord1[command];
                    break;
            }
        }

        enum Command
        {
            Undefined = 0,
            /// <summary>
            /// 读取气体采样值
            /// </summary>
            [Description("读取气体采样值")]
            ReadSamplingValue = 0x87,

            /// <summary>
            /// 读取气体浓度值
            /// </summary>
            [Description("读取气体浓度值")]
            ReadConcentrationValue = 0x86,

            /// <summary>
            /// 读取校准浓度值
            /// </summary>
            [Description("读取校准浓度值")]
            ReadConcentrationValueCalibration = 0x34
        }
    }

    class test
    {
        public string x { get; set; }

        public string y { get; set; }
    }

}

