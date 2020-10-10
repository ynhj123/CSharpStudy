
public class MsgPing : MsgBase
{
    public MsgPing() { protoName = "MsgPing"; }
}


public class MsgPong : MsgBase
{
    public MsgPong() { protoName = "MsgPong"; }
}

public class MsgEnter : MsgBase
{
    public MsgEnter() { protoName = "MsgEnter"; }
    public string id;
    public string name;
}

