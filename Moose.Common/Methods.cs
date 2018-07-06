using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

namespace Moose.Common
{
    public class Methods
    {
        public static void TestPorts()
        {
            List<string> _PortNames = new List<string>();
            _PortNames.AddRange(System.IO.Ports.SerialPort.GetPortNames());

            SerialPortHelper _SerialPortHelper;
            Moose.Model.SerialPortInfo _SerialPortInfo;
            string _InstrumentName = string.Empty;
            byte[] _SendData;

            foreach (Moose.Model.CommonEnum.InstrumentEnum item in Enum.GetValues(typeof(Moose.Model.CommonEnum.InstrumentEnum)))
            {
                _InstrumentName = GetEnumDescription(item as Enum);
                if (item == Moose.Model.CommonEnum.InstrumentEnum.GasSampleCompounderInfo|| item == Moose.Model.CommonEnum.InstrumentEnum.SolenoidValveInfo)
                {
                    continue;
                }
                Console.WriteLine($"开始查询{_InstrumentName}仪器串口！！！");
                for (int i = 0; i < _PortNames.Count; i++)
                {
                    Console.WriteLine($"开始测试{_PortNames[i]}！！！");
                    _SerialPortInfo = new Model.SerialPortInfo();
                    _SerialPortInfo.PortName = _PortNames[i];

                    switch (item)
                    {
                        case Model.CommonEnum.InstrumentEnum.SolenoidValveInfo:
                        case Model.CommonEnum.InstrumentEnum.ConverterPlate:
                        case Model.CommonEnum.InstrumentEnum.GasSampleCompounderInfo:
                            _SerialPortInfo.BaudRate = 9600;
                            _SerialPortInfo.DataBits = 8;
                            _SerialPortInfo.Parity = System.IO.Ports.Parity.None;
                            _SerialPortInfo.StopBits = System.IO.Ports.StopBits.One;
                            break;
                    }
                    _SerialPortHelper = new SerialPortHelper();
                    _SerialPortHelper.IniInfo(_SerialPortInfo);
                    _SerialPortHelper.S_Port_DataReceived += _SerialPortHelper_S_Port_DataReceived;

                    if (_SerialPortHelper.IsOpen)
                    {
                        Console.WriteLine("当前串口已打开！！！");
                        continue;
                    }
                    else
                    {
                        _SerialPortHelper.Start();
                        switch (item)
                        {
                            case Model.CommonEnum.InstrumentEnum.SolenoidValveInfo:
                                _SendData = new byte[] { 0x03, 0x01, 0x00, 0x00, 0x00, 0x10, 0x3C, 0x24 };
                                break;
                            case Model.CommonEnum.InstrumentEnum.ConverterPlate:
                                //                        01 04 10 00 00 0A 74 CD
                                _SendData = new byte[] { 0x01, 0x04, 0x10, 0x00, 0x00, 0x0A, 0x74, 0xCD };
                                break;
                            case Model.CommonEnum.InstrumentEnum.GasSampleCompounderInfo:
                                //_SendData = new byte[] { (byte)Convert.ToInt32(_PortNames[i].Replace("COM", ""), 16), 0x04, 0x10, 0x00, 0x00, 0x0A, 0x74, 0xCD };
                                _SendData = new byte[] { 0, 0, 0 };
                                break;
                            default:
                                _SendData = new byte[] { 0, 0, 0 };
                                break;
                        }

                        _SerialPortHelper.AddSendOrder(_SendData);
                        Thread.Sleep(5000);
                        _SerialPortHelper.Stop();
                        if (_SerialPortHelper.IsReturn)
                        {
                            Console.WriteLine(_InstrumentName + "串口为：" + _PortNames[i]);
                            break;
                        }
                        else if (i == _PortNames.Count - 1)
                        {
                            Console.WriteLine(_InstrumentName + "串口未发现");
                        }
                    }
                }
            }
            Console.WriteLine("查询完毕！！！");
        }

        private static void _SerialPortHelper_S_Port_DataReceived(string obj)
        {
            Console.WriteLine(obj);
        }

        /// <summary>
        ///通过枚举类型拿到描述
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum enumValue)
        {
            string str = enumValue.ToString();
            System.Reflection.FieldInfo field = enumValue.GetType().GetField(str);
            object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (objs == null || objs.Length == 0) return str;
            System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)objs[0];
            return da.Description;
        }
        /// <summary>
        ///通过枚举描述取枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Description"></param>
        /// <returns>若为-1，表示不存在；否则请转换为枚举类型</returns>
        public static int GetEnum<T>(string Description)
        {
            foreach (T item in Enum.GetValues(typeof(T)))
            {
                if (GetEnumDescription(item as System.Enum) == Description)
                {
                    return Convert.ToInt32(item);
                }
            }
            return -1;
        }

        public static void ListCommon()
        {
            List<String> list = new List<String>();
            list.Add("aaa");
            list.Add("bbb");
            list.Add("aaa");
            list.Add("aba");
            list.Add("aaa");

            ISet<string> set = new HashSet<string>(list);
            List<string> newList = new List<string>(new HashSet<string>(list));



            //foreach (string item in list)
            //{
            //    if (set.Add(item))
            //    {
            //        newList.Add(item);
            //    }
            //}



        }
    }
}
