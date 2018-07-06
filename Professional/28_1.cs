using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;

namespace Professional
{
    public class _28_1
    {
        public void Method1()
        {
            StringInfo.GetTextElementEnumerator("测试用");
        }

        public void Method2()
        {
            var cultureInfo = new CultureInfo("es-US");
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
        }
    }
}
