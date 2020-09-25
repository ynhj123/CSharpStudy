using GameCommon.Annotation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameCommon
{
    [Component]
    class TestController
    {
        public void hello()
        {
            Console.WriteLine("hello world");
        }
    }
}
