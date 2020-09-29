


using System.Collections.Generic;
using System.Net;


public class MsgEndBattle : MsgBase
{
    public MsgEndBattle()
    {
        this.protoName = "MsgEndBattle";
    }
    public HttpStatusCode code;
    public string result;
    
    
}
public class MsgStartBattle : MsgBase
{
    public MsgStartBattle()
    {
        this.protoName = "MsgStartBattle";
    }
    public HttpStatusCode code;
    public string result;
    public List<StartPlay> startPlays;
    public class StartPlay
    {
        string id;
        int x;
        int y;
        int index;

        public string Id { get => id; set => id = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Index { get => index; set => index = value; }
    }
}
public class MsgPrepare : MsgBase
{
    public MsgPrepare()
    {
        this.protoName = "MsgPrepare";
    }
    public HttpStatusCode code;
    public string result;
}
public class MsgUnprepare : MsgBase
{
    public MsgUnprepare()
    {
        this.protoName = "MsgUnprepare";
    }
    public HttpStatusCode code;
    public string result;
}
public class MsgListRoom : MsgBase
{
    public MsgListRoom()
    {
        this.protoName = "MsgListRoom";


    }
    public HttpStatusCode code;
    public string result;
}


public class MsgEnterRoom : MsgBase
{
    public MsgEnterRoom()
    {
        this.protoName = "MsgEnterRoom";
    }
    public int roomId;
    public HttpStatusCode code;
    public string result;
}
public class MsgLeaveRoom : MsgBase
{
    public MsgLeaveRoom()
    {
        this.protoName = "MsgLeaveRoom";
    }
    public HttpStatusCode code;
    public string result;

}
public class MsgGetRoomInfo : MsgBase
{
    public MsgGetRoomInfo()
    {
        this.protoName = "MsgGetRoomInfo";
    }
    public int roomId;
    public HttpStatusCode code;
    public string result;
}




