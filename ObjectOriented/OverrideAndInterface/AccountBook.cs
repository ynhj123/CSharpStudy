using System.Collections.Generic;
using System.Linq;

namespace ObjectOriented.OverrideAndInterface
{
    public class AccountBook
    {
        List<OrderForm> orderList = new List<OrderForm>();

        public List<OrderForm> OrderList { get => orderList; set => orderList = value; }

        public void add(OrderForm order)
        {
            orderList.Add(order);
        }
        public void remove(OrderForm order)
        {
            orderList.Remove(order);
        }
        public double totalPrice()
        {
            double total = orderList.Sum(order => order.totalPrice());
            return total;
        }
    }
}
