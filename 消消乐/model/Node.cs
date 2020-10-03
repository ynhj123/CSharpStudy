using System;

namespace 消消乐.model
{
    class Node
    {
        Type type;
        Status status;
        Position position;

        internal Type Type { get => type; set => type = value; }
        internal Status Status { get => status; set => status = value; }
        internal Position Position { get => position; set => position = value; }

        public override bool Equals(object obj)
        {
            return obj is Node node &&
                   type == node.type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(type);
        }


    }
    enum Type
    {
        red,
        orign,
        yello,
        green,
        gray,
        blue,
        purple,

    }
    enum Status
    {
        stop,
        moving,
        reduce,
    }
}
