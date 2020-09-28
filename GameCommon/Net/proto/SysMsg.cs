public class MsgPing : MsgBase
{
    public MsgPing()
    {
        protoName = "MsgPing";
    }

}
public class MsgPong : MsgBase
{
    public MsgPong()
    {
        protoName = "MsgPong";
    }

}
public class MsgLogin : MsgBase
{
    public MsgLogin()
    {
        protoName = "MsgLogin";
    }
    public string username;
    public string password;
    public bool isSuccess;
    public string result;
}

public class MsgRegistry : MsgBase
{
    public MsgRegistry()
    {
        protoName = "MsgRegistry";                 
    }
    public string username;
    public string password;
    public bool isSuccess;
    public string result;
}

public class MsgKick : MsgBase
{
    public MsgKick() { protoName = "MsgKick"; }
    //原因（0-其他人登陆同一账号）
    public int reason = 0;
}
