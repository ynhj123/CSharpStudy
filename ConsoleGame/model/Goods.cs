namespace ConsoleGame.model
{
    class Goods
    {
        private int id;
        private string name;
        private int price;
        private GoodsType type;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Price { get => price; set => price = value; }
        internal GoodsType Type { get => type; set => type = value; }
    }
    enum GoodsType
    {
        EQUIPMENT,
        BOOK,

    }
}
