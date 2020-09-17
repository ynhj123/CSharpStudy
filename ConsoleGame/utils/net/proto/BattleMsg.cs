public class MsgMove : MsgBase
{
    public MsgMove()
    {
        protoName = "MsgMove";
    }
    public int x = 0;
    public int y = 0;
    public int z = 0;
}
public class MsgAttack : MsgBase
{
    public MsgAttack()
    {
        protoName = "MsgAttack";
    }
    public string des = "127.0.0.7:6543";
}