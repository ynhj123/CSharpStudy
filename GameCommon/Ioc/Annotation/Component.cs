﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCommon.Annotation
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class Component : Attribute
    {
    }
}
