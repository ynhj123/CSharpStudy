using System;
using System.Collections.Generic;
using System.Text;

namespace GameCommon.Utils
{
    public class UUIDUtils
    {
        public static string GetUUID()
        {
            return System.Guid.NewGuid().ToString("N");
        }
    }
}
