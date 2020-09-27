
using GameCommon.Ioc.Annotation;

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
