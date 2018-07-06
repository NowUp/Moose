using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional
{
    public abstract class AbstractDemo
    {
        protected abstract void Method1();

        internal abstract void Mehtod2();

        public abstract void Method3();

        internal protected abstract void Method4();

        private void Method5()
        {
            string Test = string.Empty;
        }

        int Count = -1;

        public string MyTest { get; set; }
        public void AddMethod()
        {

        }

    }
    public class AbstractDemoObject : AbstractDemo
    {
        public override void Method3()
        {
            throw new NotImplementedException();
        }

        protected override void Method1()
        {
            throw new NotImplementedException();
        }

        protected internal override void Method4()
        {
            throw new NotImplementedException();
        }

        internal override void Mehtod2()
        {
            throw new NotImplementedException();
        }

    }

    public class OBJ
    {
        public void Method1()
        {
            AbstractDemoObject abstractDemo = new AbstractDemoObject();
            abstractDemo.AddMethod();
        }
    }


}
