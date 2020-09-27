
using GameCommon.Ioc.Annotation;
using System;

namespace GameCommon
{
    [Component]
    class TestController
    {
        

        [AutoWired]
        public TestService service;


        public void Hello()
        {
            service.Hello();
        }
    }
}
