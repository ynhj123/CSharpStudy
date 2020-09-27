using System;
using System.Collections.Generic;
using System.Text;

namespace GameCommon.Ioc.Annotation
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AutoWired : Attribute
    {
    }
}
