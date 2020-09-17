
using System.Collections.Generic;

namespace ObjectOriented.OverrideAndInterface
{
    public abstract class OrderForm
    {
        List<Goods> goodsList = new List<Goods>();

        public List<Goods> GoodsList { get => goodsList; set => goodsList = value; }

        public void add(Goods goods)
        {
            goodsList.Add(goods);
        }
        public void remove(Goods goods)
        {
            goodsList.Remove(goods);
        }
        public abstract double totalPrice();
    }
}
