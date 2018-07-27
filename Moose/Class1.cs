using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moose
{

    public delegate void DelegateMethod1(int a, int b);
    public  class Class1
    {

        public static void AddMethod(int a,int b)
        {
            Console.WriteLine(a+b);
            Console.WriteLine("Class1");
        }

        public static void Mehod1()
        {
            Class2 class2 = new Class2();
            class2.TestMethod( AddMethod);

        }
    }

    public class Class2
    {
        public delegate void Event_Add(int a,int b);
        Event_Add aaaa;
        public void TestMethod(Action<int, int> method)
        {
             aaaa = new Event_Add(method);
        }

        public void AA()
        {
            Console.WriteLine("Class2");
            aaaa(1,2);
        }
    }
}
