using System;
using System.Collections.Generic;
using System.Text;

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

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if ((obj.GetType().Equals(this.GetType())) == false)
            {
                return false;
            }
            Node temp = null;
            temp = (Node)obj;

            return this.Type == temp.Type;

        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
           
            return base.GetHashCode();
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
        purple
    }
    enum Status
    {
        stop,
        moving,
        reduce,
    }
}
