using System.Linq;

namespace ObjectOriented.OverrideAndInterface
{
    class DiscountOrder : OrderForm
    {
        //折扣值
        double discount;

        public DiscountOrder(double discount)
        {
            if (discount > 1)
            {
                discount = 1;
            }
            this.discount = discount;
        }

        public override double totalPrice()
        {
            double orignPirce = GoodsList.Sum(goods => goods.Num * goods.Price);
            return orignPirce * discount;
        }
    }
}
