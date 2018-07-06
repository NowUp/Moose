using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Moose.Model
{
    public class SerialPortInfo
    {
        public SerialPortInfo()
        {
            PortName = "COM1";
            BaudRate = 19200;
            StopBits = System.IO.Ports.StopBits.One;
            Parity = System.IO.Ports.Parity.None;
            DataBits = 8;
        }

        /// <summary>
        /// 串口名称
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { get; set; }

        /// <summary>
        /// 奇偶校验位
        /// </summary>
        public Parity Parity { get; set; }

        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits StopBits { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataBits { get; set; }
    }
}
