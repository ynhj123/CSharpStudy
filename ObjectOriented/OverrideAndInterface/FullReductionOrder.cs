using System.Collections.Generic;
using System.Linq;

namespace ObjectOriented.OverrideAndInterface
{
    class FullReductionOrder : OrderForm
    {
        static SortedDictionary<double, double> fullReduce = new SortedDictionary<double, double>() { { 10, 1 }, { 50, 3 }, { 100, 5 } };



        public override double totalPrice()
        {
            double orignPirce = GoodsList.Sum(goods => goods.Num * goods.Price);
            double total = 0;
            var dicSort = from objDic in fullReduce orderby objDic.Key ascending select objDic;
            foreach (KeyValuePair<double, double> item in dicSort)
            {
                if (orignPirce > item.Key)
                {
                    total -= item.Value;
                    break;
                }
            }
            return total;


        }
    }
}
