
using GameCommon.Ioc.Annotation;
using System;

namespace GameCommon
{
    [Component]
    class TestService
    {
        private string str;
        public string Str { get => str; set => str = value; }
        public void Hello()
        {
            Console.WriteLine("hello {0}", Str);
        }
    }
}
