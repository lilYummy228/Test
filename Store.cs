Good iPhone12 = new Good("IPhone 12");
Good iPhone11 = new Good("IPhone 11");

Warehouse warehouse = new Warehouse();

Shop shop = new Shop(warehouse);

int IPhone12Count = 10;
int IPhone11Count = 1;
int requiredIPhone12Count = 4;
int requiredIPhone11Count = 3;

warehouse.Delive(iPhone12, IPhone12Count);
warehouse.Delive(iPhone11, IPhone11Count);

Cart cart = shop.Cart();

cart.Add(iPhone12, GetCount(requiredIPhone12Count, IPhone12Count));
cart.Add(iPhone11, GetCount(requiredIPhone11Count, IPhone11Count)); 

Console.WriteLine(cart.Order().Paylink);

warehouse.Remove(iPhone12, requiredIPhone12Count);

public int GetCount(int requiredCount, int count)
{
    if (count < requiredCount)
        throw new ArgumentOutOfRangeException(nameof(requiredCount));

    return count;
}