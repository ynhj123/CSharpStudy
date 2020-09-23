
using ConsoleGame.utils.net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
/**
 * 待处理 粘包分包，线程冲突
 */
public static class NetManagerEvent
{
    static Socket socket;
    static ByteArray readBuff;
    static Queue<ByteArray> writeQueue;
    public delegate void MsgListener(MsgBase msgBase);
    public delegate void EventListener(string str);
    //代理事件
    private static Dictionary<NetEvent, EventListener> eventListeners = new Dictionary<NetEvent, EventListener>();
    static bool isConnecting = false;
    static bool isClosing = false;
    public static bool isUserPing = true;
    public static int PingInterval = 5;
    static long lastPingTime = 0;
    static long lastPongTime = 0;
    private static Dictionary<string, MsgListener> listeners = new Dictionary<string, MsgListener>();
    static List<MsgBase> msgList = new List<MsgBase>();
    static int msgCount = 0;
    readonly static int MAX_MESSAGE_FIRE = 10;

    public static void AddEventListen(NetEvent netEvent, EventListener listener)
    {
        if (eventListeners.ContainsKey(netEvent))
        {
            eventListeners[netEvent] += listener;
        }
        else
        {
            eventListeners[netEvent] = listener;
        }
    }

    public static void RemoveEventListen(NetEvent netEvent, EventListener listener)
    {
        if (eventListeners.ContainsKey(netEvent))
        {
            eventListeners[netEvent] -= listener;
            if (eventListeners[netEvent] == null)
            {
                eventListeners.Remove(netEvent);
            }
        }

    }
    private static void FireEvent(NetEvent netEvent, string err)
    {
        if (eventListeners.ContainsKey(netEvent))
        {
            eventListeners[netEvent](err);
        }
    }
    public static void AddListener(string msgName, MsgListener listener)
    {
        if (listeners.ContainsKey(msgName))
        {
            listeners[msgName] += listener;
        }
        else
        {
            listeners[msgName] = listener;
        }
    }

    public static void RemoveListener(string msgName, MsgListener listener)
    {
        if (listeners.ContainsKey(msgName))
        {
            listeners[msgName] -= listener;
            if (listeners[msgName] == null)
            {
                listeners.Remove(msgName);
            }
        }

    }
    private static void FireMsg(string msgName, MsgBase msgBase)
    {
        if (listeners.ContainsKey(msgName))
        {
            listeners[msgName](msgBase);
        }
    }

    public static string GetDesc()
    {
        if (socket == null)
        {
            return "";
        }
        if (!socket.Connected)
        {
            return "";
        }
        return socket.LocalEndPoint.ToString();
    }
    public static void Connect(string ip, int port)
    {
        if (socket != null && socket.Connected)
        {
            Debug.WriteLine("Connect fail,already connected");
            return;
        }
        if (isConnecting)
        {
            Debug.WriteLine("Connect fail,isConnecting");
            return;
        }
        InitState();
        socket.NoDelay = true;
        isConnecting = true;
        socket.BeginConnect(ip, port, ConnectCallback, socket);
    }

    private static void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            Socket socket = (Socket)ar.AsyncState;
            socket.EndConnect(ar);
            Debug.WriteLine("Socket Connect Succ");
            FireEvent(NetEvent.ConnectSucc, "");
            isConnecting = false;
            //接受数据
            socket.BeginReceive(readBuff.bytes, readBuff.writeIdx, readBuff.remain, 0, ReceiveCallBack, socket);

        }
        catch (SocketException ex)
        {
            Debug.WriteLine("Socket Connect Fail" + ex.ToString());
            FireEvent(NetEvent.ConnectFail, ex.ToString());
            isConnecting = false;

        }
    }

    private static void InitState()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        readBuff = new ByteArray();
        writeQueue = new Queue<ByteArray>();
        isConnecting = false;
        msgList = new List<MsgBase>();
        msgCount = 0;
        lastPingTime = GetTimeStamp();
        lastPongTime = GetTimeStamp();
        //pong
        if (!listeners.ContainsKey("MsgPong"))
        {
            AddListener("MsgPong", OnMsgPong);
        }
    }
    private static void OnMsgPong(MsgBase msgBase)
    {
        lastPongTime = GetTimeStamp();
    }
    private static void PingUpdate()
    {
        if (!isUserPing)
        {
            return;
        }
        //send
        if (GetTimeStamp() - lastPingTime > PingInterval)
        {
            MsgPing msgPing = new MsgPing();
            Send(msgPing);
            lastPingTime = GetTimeStamp();
        }
        //pong
        if (GetTimeStamp() - lastPongTime > PingInterval * 4)
        {
            Close();
        }
    }
    public static void Close()
    {
        if (socket == null || !socket.Connected)
        {
            return;
        }
        if (isConnecting)
        {
            return;
        }
        if (writeQueue.Count > 0)
        {
            isClosing = true;
        }
        else
        {
            socket.Close();
            FireEvent(NetEvent.Close, "");
        }
    }

    private static void ReceiveCallBack(IAsyncResult result)
    {
        try
        {
            Socket socket = (Socket)result.AsyncState;

            int count = socket.EndReceive(result);
            if (count == 0)
            {
                Close();
                return;
            }
            readBuff.writeIdx += count;
            OnReceiveData();
            if (readBuff.remain < 8)
            {
                readBuff.MoveBytes();
                readBuff.Resize(readBuff.length * 2);
            }

            /*string recvStr = System.Text.Encoding.Default.GetString(readBuff, 0, count);
            //msgList.Add(recvStr);
            UnityEngine.Debug.Log("client Receive:" + recvStr);
            msgQueue.Enqueue(recvStr);*/
            socket.BeginReceive(readBuff.bytes, readBuff.writeIdx, readBuff.remain, 0, ReceiveCallBack, socket);
        }
        catch (Exception ex)
        {

            Debug.WriteLine("Socket Receive fail" + ex.ToString());
        }
    }

    private static void OnReceiveData()
    {
        if (readBuff.length <= 2)
        {
            return;
        }
        // header
        int readIdx = readBuff.readIdx;
        byte[] bytes = readBuff.bytes;
        Int16 bodyLength = (Int16)((bytes[readIdx + 1] << 8) | bytes[readIdx]);
        if (readBuff.length < bodyLength)
        {
            return;
        }
        readBuff.readIdx += 2;
        // name
        int nameCount = 0;
        string protoName = MsgBase.DecodeName(readBuff.bytes, readBuff.readIdx, out nameCount);
        if (protoName == "")
        {
            Debug.WriteLine("OnReceiveData msgBase.Decodename fail");
            return;
        }
        Debug.WriteLine("OnReceiveData msgBase {0}", protoName);

        readBuff.readIdx += nameCount;
        //body
        int bodyCount = bodyLength - nameCount;
        MsgBase msgBase = MsgBase.Decode<MsgBase>(protoName, readBuff.bytes, readBuff.readIdx, bodyCount);
        readBuff.readIdx += bodyCount;
        readBuff.CheckAndMoveBytes();
        //add list
        lock (msgList)
        {
            msgList.Add(msgBase);

        }
        msgCount++;
        if (readBuff.length > 2)
        {
            OnReceiveData();
        }

    }

    public static void Send(MsgBase msg)
    {
        if (socket == null || !socket.Connected)
        {
            return;
        }
        if (isConnecting)
        {

            return;
        }
        if (isClosing)
        {
            return;
        }

        byte[] nameBytes = MsgBase.EncodeName(msg);
        byte[] bodyBytes = MsgBase.Encode(msg);
        int len = nameBytes.Length + bodyBytes.Length;
        byte[] sendBytes = new byte[2 + len];
        sendBytes[0] = (byte)(len % 256);
        sendBytes[1] = (byte)(len / 256);
        Array.Copy(nameBytes, 0, sendBytes, 2, nameBytes.Length);
        Array.Copy(bodyBytes, 0, sendBytes, 2 + nameBytes.Length, bodyBytes.Length);
        ByteArray byteArray = new ByteArray(sendBytes);
        int count = 0;
        lock (writeQueue)
        {
            writeQueue.Enqueue(byteArray);
            count = writeQueue.Count;
        }
        if (count == 1)
        {
            socket.BeginSend(sendBytes, 0, sendBytes.Length, 0, SendCallback, socket);
        }

    }

    private static void SendCallback(IAsyncResult ar)
    {
        Socket socket = (Socket)ar.AsyncState;
        if (socket == null || !socket.Connected)
        {
            return;
        }
        int count = socket.EndSend(ar);
        ByteArray byteArray;
        lock (writeQueue)
        {
            //先读取队列
            byteArray = writeQueue.First();
        }
        byteArray.readIdx += count;
        //如果读完了
        if (byteArray.length == 0)
        {
            lock (writeQueue)
            {
                //将元素弹出
                writeQueue.Dequeue();
                //顺便看看还有没有要处理的
                if (writeQueue.Count == 0)
                {
                    byteArray = null;
                }
                else
                {
                    byteArray = writeQueue.First();

                }

            }
        }
        if (byteArray != null)
        {
            socket.BeginSend(byteArray.bytes, byteArray.readIdx, byteArray.length, 0, SendCallback, socket);
        }
        else if (isClosing)
        {
            socket.Close();
        }
    }

    public static void Update()
    {
        MsgUpdate();
        PingUpdate();
    }
    public static void MsgUpdate()
    {
        if (msgCount == 0)
        {
            return;
        }
        for (int i = 0; i < MAX_MESSAGE_FIRE; i++)
        {
            MsgBase msgBase = null;
            lock (msgList)
            {
                if (msgList.Count > 0)
                {
                    msgBase = msgList[0];
                    msgList.RemoveAt(0);
                    msgCount--;
                }
            }
            if (msgBase != null)
            {

                FireMsg(msgBase.protoName, msgBase);
            }
            else
            {
                break;
            }
        }
    }
    public static long GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds);
    }

}

public enum NetEvent
{
    ConnectSucc = 1,
    ConnectFail = 2,
    Close = 3,
}