


using System.Net;

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




