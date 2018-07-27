using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using System.Runtime.InteropServices;

namespace Moose.Common
{
    public class XmlHepler
    {
        public void Method1()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(@"E:\Work\NA1000S环境监控系统\trunk\SmartNA1000\SmartNA1000\bin\Debug\AppConfig.xml");

            XmlElement XmlElement1 = xmlDocument.CreateElement("HistorySetup");

            XmlElement xmlElement2;

            xmlElement2 = xmlDocument.CreateElement("OnOrOf");
            xmlElement2.InnerText = "1";
            XmlElement1.AppendChild(xmlElement2);

            xmlElement2 = xmlDocument.CreateElement("IntervalTime");
            xmlElement2.InnerText = "15";
            XmlElement1.AppendChild(xmlElement2);

            xmlElement2 = xmlDocument.CreateElement("ChangeSF6");
            xmlElement2.InnerText = "100.00";
            XmlElement1.AppendChild(xmlElement2);

            xmlElement2 = xmlDocument.CreateElement("ChangeO2");
            xmlElement2.InnerText = "1.00";
            XmlElement1.AppendChild(xmlElement2);

            xmlElement2 = xmlDocument.CreateElement("ChangeT");
            xmlElement2.InnerText = "0.00";
            XmlElement1.AppendChild(xmlElement2);

            xmlElement2 = xmlDocument.CreateElement("ChangeH");
            xmlElement2.InnerText = "0.00";
            XmlElement1.AppendChild(xmlElement2);

            xmlDocument.DocumentElement.AppendChild(XmlElement1);

            xmlDocument.Save(@"E:\Work\NA1000S环境监控系统\trunk\SmartNA1000\SmartNA1000\bin\Debug\AppConfig.xml");
        }

        public void Method2()
        {
            //int WINDOW_HANDLER = FindWindow(null, @"欲发送程序窗口的标题");
            int WINDOW_HANDLER = FindWindow(null, @"接收窗口的标题");
            if (WINDOW_HANDLER != 0)
            {
                byte[] sarr = System.Text.Encoding.Default.GetBytes("Text");
                int len = sarr.Length;
                COPYDATASTRUCT cds;
                cds.dwData = (IntPtr)100;
                cds.lpData = "Text";
                cds.cbData = len + 1;
                SendMessage(WINDOW_HANDLER, WM_COPYDATA, 0, ref cds);
            }

        }

        const int WM_COPYDATA = 0x004A;

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(
            int hWnd, // handle to destination window
            int Msg, // message
            int wParam, // first message parameter
            ref COPYDATASTRUCT lParam // second message parameter
        );

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);
    }
    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbData;
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpData;
    }

}
