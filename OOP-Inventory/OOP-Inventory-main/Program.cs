using OOP_Inventory;

Item fish = new Item("Fish", "A tasty snack", 40, 1);

Inventory ItemInv = new Inventory("Items", 20);
ItemInv.AddItem(fish, 89);

ItemInv.InvToConsole();

Console.ReadLine();