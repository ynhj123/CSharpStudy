using NetWorkUtils.Server;

public class Player
{
    //id
    public string id = "";
    public string name = "";
    //指向ClientState
    public ClientState state;
    public int roomId;
    //构造函数
    public Player(ClientState state)
    {
        this.state = state;
    }

    //发送信息
    public void Send(MsgBase msgBase)
    {
        NetManager.Send(state, msgBase);
    }

}


