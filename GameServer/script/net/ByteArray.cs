using System;

namespace GameServer.script.net
{
    public class ByteArray
    {
        const int DEFAULT_SIZE = 1024;
        int initSize = 0;
        public byte[] bytes;
        public int readIdx = 0;
        public int writeIdx = 0;
        private int capacity = 0;

        public int remain { get { return capacity - writeIdx; } }
        public int length { get { return writeIdx - readIdx; } }

        public ByteArray(byte[] defaultBytes)
        {
            this.bytes = defaultBytes;
            capacity = defaultBytes.Length;
            initSize = defaultBytes.Length;
            readIdx = 0;
            writeIdx = defaultBytes.Length;
        }

        public ByteArray(int size = DEFAULT_SIZE)
        {
            bytes = new byte[size];
            capacity = size;
            initSize = size;
            readIdx = 0;
            writeIdx = 0;
        }

        public void Resize(int size)
        {
            if (size < length)
            {
                return;
            }
            if (size < initSize)
            {
                return;
            }
            int n = 1;
            while (n < size)
            {
                n *= 2;
            }
            capacity = n;
            byte[] newBytes = new byte[capacity];
            Array.Copy(bytes, readIdx, newBytes, 0, writeIdx - readIdx);
            bytes = newBytes;
            writeIdx = length;
            readIdx = 0;

        }

        public void CheckAndMoveBytes()
        {
            if (length < 8)
            {
                MoveBytes();
            }
        }

        public void MoveBytes()
        {
            Array.Copy(bytes, readIdx, bytes, 0, length);
            writeIdx = length;
            readIdx = 0;
        }

        public int Write(byte[] bs, int offset, int count)
        {
            if (remain < count)
            {
                Resize(length + count);
            }
            Array.Copy(bs, offset, bytes, writeIdx, count);
            writeIdx += count;
            return count;
        }
        public int read(byte[] bs, int offset, int count)
        {
            //半包
            count = Math.Min(count, length);
            Array.Copy(bytes, 0, bs, offset, count);
            readIdx += count;
            CheckAndMoveBytes();
            return count;
        }
        /**
         * 解决大小端
         */
        public Int16 ReadInt16()
        {
            if (length < 2)
            {
                return 0;
            }
            Int16 ret = (Int16)((bytes[1] << 8) | bytes[0]);
            readIdx += 2;
            CheckAndMoveBytes();
            return ret;
        }
        /**
         * 解决大小端
         */
        public Int32 ReadInt32()
        {
            if (length < 4)
            {
                return 0;
            }
            Int32 ret = (Int32)((bytes[3] << 24) | bytes[2] << 16 | bytes[1] << 8 | bytes[0]);
            readIdx += 4;
            CheckAndMoveBytes();
            return ret;
        }

        public override string ToString()
        {
            return BitConverter.ToString(bytes, readIdx, length);
        }
        public string Debug()
        {
            return string.Format("readIdx({0}) writeIdx({1}) bytes({2})", readIdx, writeIdx, BitConverter.ToString(bytes, 0, bytes.Length));
        }
    }
}
