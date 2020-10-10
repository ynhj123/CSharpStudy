using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NetWorkUtils.Client
{
    class NetEventHandler
    {
        public static void OnConnectSucc(string err)
        {
            Debug.WriteLine(err);
        }

        internal static void OnConnectFail(string err)
        {
            Debug.WriteLine(err);
        }

        internal static void OnClose(string err)
        {
            Debug.WriteLine(err);
        }
    }
}
