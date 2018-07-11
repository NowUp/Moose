using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Moose.Model
{
    public class CommonEnum
    {
        public enum InstrumentEnum:int
        {
            /// <summary>
            /// 动态配气仪
            /// </summary>
            [Description("动态配气仪")]
            GasSampleCompounderInfo = 0,
            /// <summary>
            /// 电磁阀模块
            /// </summary>
            [System.ComponentModel.Description("电磁阀模块")]
            SolenoidValveInfo = 1,
            /// <summary>
            /// 工装板
            /// </summary>
            [System.ComponentModel.Description("工装板")]
            ConverterPlate = 2
        }
    }
}
