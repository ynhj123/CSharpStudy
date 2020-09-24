using System;
using System.Collections.Generic;

public class MsgMove : MsgBase
{
    public MsgMove()
    {
        protoName = "MsgMove";
    }
    public string spriteId = "";
    public int x = 0;
    public int y = 0;
    public int z = 0;
    public int veloctity = (int)ConsoleKey.UpArrow;

}
public class MsgAttack : MsgBase
{
    public MsgAttack()
    {
        protoName = "MsgAttack";
    }
    public string playId = "";

}
public class MsgEnter : MsgBase
{
    public MsgEnter()
    {
        protoName = "MsgEnter";
    }
    public string playId = "";
    public int x = 0;
    public int y = 0;
    public int z = 0;
    public int hp = 0;
    public int veloctity = 0;
    public string style;
    public List<MsgEnter> players;
}

public class MsgLeave : MsgBase
{
    public MsgLeave()
    {
        protoName = "MsgLeave";
    }
    public string playId = "";
}