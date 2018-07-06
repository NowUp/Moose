using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace Moose.Common
{
    public class SerialPortHelper
    {

        private System.IO.Ports.SerialPort SerialPortMng;

        private List<byte> ReceiveData;
        private Queue<byte[]> ReceiveDataQueue;
        private Queue<byte[]> SendOrderQueue;
        object ReceiveDataListLock;
        object SendOrderQueueLock;
        private bool ThreadRunFlag;
        public bool IsReturn = false;

        public event Action<string> S_Port_DataReceived;


        public bool IsOpen
        {
            get
            {
                if (SerialPortMng == null)
                {
                    return false;
                }
                else
                {
                    return SerialPortMng.IsOpen;
                }
            }
        }

        public void IniInfo(Moose.Model.SerialPortInfo serialPortInfo )
        {
            SerialPortMng = new SerialPort();
            SerialPortMng.PortName = serialPortInfo.PortName;
            SerialPortMng.BaudRate = serialPortInfo.BaudRate;
            SerialPortMng.Parity = serialPortInfo.Parity;
            SerialPortMng.DataBits = serialPortInfo.DataBits;
            SerialPortMng.StopBits = serialPortInfo.StopBits;
            SerialPortMng.DataReceived += _Port_DataReceived;
            ReceiveDataListLock = "ReceiveDataListLock";

            if (SendOrderQueue == null)
                SendOrderQueue = new Queue<byte[]>();
            if (ReceiveDataQueue == null)
                ReceiveDataQueue = new Queue<byte[]>();
            if (ReceiveData == null)
                ReceiveData = new List<byte>();

            SendOrderQueue.Clear();
            ReceiveDataQueue.Clear();

            ReceiveData.Clear();
        }

        public void Start()
        {
            try
            {
                IsReturn = false;
                SerialPortMng.Open();
                ThreadRunFlag = true;
                Thread _SendOrderTH = new Thread(new ThreadStart(Thread_SendOrder));
                _SendOrderTH.IsBackground = true;
                _SendOrderTH.Start();

                Thread _AnalysisDataTH = new Thread(new ThreadStart(Thread_AnalysisData));
                _AnalysisDataTH.IsBackground = true;
                _AnalysisDataTH.Start();
            }
            catch (Exception ex)
            {
            }
        }

        public void Stop()
        {
            try
            {
                ThreadRunFlag = false;

                if (SerialPortMng != null)
                {
                    SerialPortMng.DiscardOutBuffer();
                    SerialPortMng.DiscardInBuffer();
                    SerialPortMng.Close();
                    SerialPortMng.Dispose();
                }

                SendOrderQueue.Clear();
                ReceiveDataQueue.Clear();

                ReceiveData.Clear();

            }
            catch (Exception ex)
            {
            }
        }

        private void _Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int _DataLen = SerialPortMng.BytesToRead;
            if (_DataLen > 1)
            {
                byte[] _ReadDataByte = new byte[_DataLen];
                SerialPortMng.Read(_ReadDataByte, 0, _DataLen);
                lock (ReceiveDataListLock)
                {
                    ReceiveData.AddRange(_ReadDataByte);
                }
                IsReturn = true;

                S_Port_DataReceived?.Invoke("接收到数据");
            }
        }

        private void Thread_AnalysisData()
        {
            byte[] _DataItem, _CRCResult;
            Common.CRC16 _CRC16 = new Common.CRC16();
            bool _Result = false;
            _CRCResult = new byte[2];

            while (ThreadRunFlag)
            {
                if (ReceiveData.Count > 0)
                {
                    lock (ReceiveDataListLock)
                    {
                        _DataItem = new byte[ReceiveData.Count];
                        ReceiveData.CopyTo(0, _DataItem, 0, _DataItem.Length);
                        ReceiveData.Clear();
                    }
                    _Result = _CRC16.CheckResponse(_DataItem, ref _CRCResult);

                    if (_Result)
                    {
                        ReceiveDataQueue.Enqueue(_DataItem);
                    }
                    else
                    {
                        //验证失败
                    }

                }
                Thread.Sleep(2000);
            }
        }

        public void AddSendOrder(byte[] OrderItem)
        {
            if (SerialPortMng != null && SerialPortMng.IsOpen)
            {
                SendOrderQueue.Enqueue(OrderItem);
            }
        }

        private void Thread_SendOrder()
        {
            byte[] _OrderItem;
            while (ThreadRunFlag)
            {
                try
                {
                    //程工多判定一个发送间隔
                    if (SerialPortMng != null && SerialPortMng.IsOpen && SendOrderQueue.Count > 0)
                    {
                        _OrderItem = SendOrderQueue.Dequeue();
                        SerialPortMng.Write(_OrderItem, 0, _OrderItem.Length);
                    }
                }
                catch (Exception ex)
                {
                    //发送命令失败
                }
            }
        }
    }
}
