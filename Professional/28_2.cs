using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Globalization;

namespace Professional
{
    public class _28_2
    {
        private static void DisplayNames(string title, IEnumerable<string> e)
        {
            WriteLine(title);
            WriteLine(string.Join("-",e));
            WriteLine();
        }

        /// <summary>
        /// 排序
        /// 在默认情况下，为排序而比较字符串的算法依赖于区域性。
        /// 例如：在芬兰，字符W和V是相同的。
        /// </summary>
        public static  void SortingDemo()
        {
            string[] Names = { "Alabama", "Texas", "Washington", "Virginia", "Wisconsin", "Wyoming", "Kentucky", "Missouri", "Utah", "Hawaii", "Kansas", "Louisiana", "Alaska", "Arizona" };

            //CultureInfo.CurrentCulture = new CultureInfo("en-US");

            //Array.Sort(Names);
            //DisplayNames("Sorted using the Finnish Culture:en-US", Names);


            CultureInfo.CurrentCulture = new CultureInfo("fi-FI");

            Array.Sort(Names);
            DisplayNames("Sorted using the Finnish Culture:fi-FI", Names);


            //进行独立于区域性的排序
            Array.Sort(Names, System.Collections.Comparer.DefaultInvariant);
            DisplayNames("Sorted using the Finnish Culture:DefaultInvariant", Names);
        }
    }
}
