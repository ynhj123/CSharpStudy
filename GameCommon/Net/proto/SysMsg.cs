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
public class LoginMsg : MsgBase
{
    public LoginMsg()
    {
        protoName = "MsgLogin";
    }
    public string username;
    public string password;
    public bool isSuccess;
    public string result;
}

public class RegistryMsg : MsgBase
{
    public RegistryMsg()
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
