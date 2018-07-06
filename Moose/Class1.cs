using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moose
{
    class Class1
    {
        public int Book{get; }

        private int book;
        public int Book1 { get { return book; } }

        public bool IsSquare(string param)
        {
            return false;
        }
        public bool IsFalse(string param) => false;

        public static void Method1()
        {
            object a = new Class1();
            if (a is Class1)
            {
            }
        }
    }
}
