
//房间信息
public class RoomInfo
{
    public int id = 0;      //房间id
    public int count = 0;   //人数
    public int maxCount = 0; // 最大人数
    public int status = 0;	//状态0-准备中 1-战斗中
}

//请求房间列表
public class MsgGetRoomList : MsgBase
{
    public MsgGetRoomList() { protoName = "MsgGetRoomList"; }
    //服务端回
    public RoomInfo[] rooms;
}

//创建房间
public class MsgCreateRoom : MsgBase
{
    public MsgCreateRoom() { protoName = "MsgCreateRoom"; }
    //服务端回 创建成功返回0
    public int result = 0;
}




//进入房间
public class MsgEnterRoom : MsgBase
{
    public MsgEnterRoom() { protoName = "MsgEnterRoom"; }
    //客户端发 房间id
    public int id = 0;
    //服务端回 进入成功返回0
    public int result = 0;
}


//玩家信息
public class PlayerInfo
{
    public string id = "lpy";   //账号
    public string name = "";
    public int isOwner = 0;		//是否是房主
}

//获取房间信息
public class MsgGetRoomInfo : MsgBase
{
    public MsgGetRoomInfo() { protoName = "MsgGetRoomInfo"; }
    //服务端回
    public PlayerInfo[] players;
}

//离开房间
public class MsgLeaveRoom : MsgBase
{
    public MsgLeaveRoom() { protoName = "MsgLeaveRoom"; }
    //服务端回 为0离开成功
    public int result = 0;
}
public class MsgLeaveGame : MsgBase
{
    public MsgLeaveGame() { protoName = "MsgLeaveGame"; }
    //用户id
    public string id = "";
}
