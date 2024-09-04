using System;
using System.Collections.Generic;
using System.Linq;

namespace Test2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Good iPhone12 = new Good("IPhone 12");
            Good iPhone11 = new Good("IPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 1);

            //Вывод всех товаров на складе с их остатком

            Cart cart = shop.Cart();
            cart.Add(iPhone12, 4);
            cart.Add(iPhone11, 1); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе

            //Вывод всех товаров в корзине

            Console.WriteLine(cart.Order().Paylink);

            cart.Add(iPhone12, 9); //Ошибка, после заказа со склада убираются заказанные товары
        }
    }

    public class Cart
    {
        private Shop _shop;
        private List<Cell> _cart;

        public Cart(Shop shop)
        {
            _shop = shop;
            _cart = new List<Cell>();
        }

        public void Add(Good good, int count)
        {
            Cell cell = _shop.Warehouse.Cells.FirstOrDefault(cells => cells.Good == good);

            if (cell == null)
                throw new ArgumentNullException(nameof(cell));

            if (count > cell.Count)
                throw new ArgumentOutOfRangeException(nameof(count));

            Cell cartCell = _cart.FirstOrDefault(cells => cells.Good == good);

            if (_cart.Contains(cartCell))
            {
                _cart.Insert(_cart.IndexOf(cartCell), new Cell(good, cartCell.Count + count));
                _cart.RemoveAt(_cart.IndexOf(cartCell));
            }
            else
            {
                _cart.Add(new Cell(good, count));
            }

            _shop.Warehouse.Remove(cell, count);
        }

        public Order Order()
        {
            string order = $"Ваша корзина\n";

            foreach (Cell cell in _cart)
                order += $"{cell.Good.Name}: {cell.Count}\n";

            return new Order(order);
        }
    }

    public class Shop
    {
        public Warehouse Warehouse { get; private set; }

        public Shop(Warehouse warehouse) => Warehouse = warehouse ?? throw new ArgumentNullException(nameof(warehouse));

        public Cart Cart() => new Cart(this);
    }

    public class Warehouse
    {
        private readonly List<Cell> _cells;

        public Warehouse() => _cells = new List<Cell>();

        public IReadOnlyList<Cell> Cells => _cells;

        public void Delive(Good good, int count) => _cells.Add(new Cell(good, count));

        public void Remove(Cell cell, int count) => _cells.Insert(_cells.IndexOf(cell), new Cell(cell.Good, cell.Count - count));
    }

    public class Cell
    {
        public Good Good { get; private set; }
        public int Count { get; private set; }

        public Cell(Good good, int count)
        {
            Good = good ?? throw new ArgumentNullException(nameof(good));
            Count = count >= 0 ? count : throw new ArgumentOutOfRangeException(nameof(count));
        }
    }

    public class Good
    {
        public Good(string name) => Name = name;

        public string Name { get; private set; }
    }

    public class Order
    {
        public Order(string paylink) => Paylink = paylink;

        public string Paylink { get; private set; }
    }
}
