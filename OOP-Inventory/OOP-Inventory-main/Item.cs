using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Inventory
{
    internal class Item
    {
        public string name;
        public string description;
        public int rarity;
        public int maximum;
        
        public Item()
        {
            name = " ";
            description = " ";
            rarity = 0;
        }
        public Item(string aName, string aDescription, int aMaximum, int aRarity) 
        {
            name = aName;
            description = aDescription;
            maximum = aMaximum;
            rarity = aRarity;
        }
    }

    internal class Inventory
    {
        public string name;
        public int size;
        public Item[] itemsArray;
        public int[] countArray;
        public Inventory(string aName, int aSize)
        {
            name = aName;
            size = aSize;
            itemsArray = new Item[size];
            countArray = new int[size];
            
            for (int i = 0; i < itemsArray.Length; i++) 
            {
                itemsArray[i] = new Item();
                countArray[i] = 0;
            }
        }

        public void AddItem(Item added, int amount)
        {
            bool done = false;
            for (int i = 0; i < countArray.Length; i++)
            {
                if (countArray[i] == 0 && !done)
                {
                    itemsArray[i] = added;
                    if (added.maximum < amount) 
                    { 
                        countArray[i] = added.maximum; 
                        amount -= added.maximum;
                    }
                    else
                    {
                        countArray[i] = amount;
                        done = true;
                    }
                }
            }
        }
        
        public void InvToConsole()
        {
            for (int i = 0; i < this.itemsArray.Length; i++)
            {
                if (!(this.countArray[i] == 0))
                {
                    Console.WriteLine("--------------------ITEM No. " + (i + 1).ToString() + "--------------------");
                    Console.Write(" | Name: " + this.itemsArray[i].name);
                    Console.Write(" | Description: " + this.itemsArray[i].description);
                    Console.Write(" | Rarity: " + this.itemsArray[i].rarity.ToString());
                    Console.Write(" | Amount: " + this.countArray[i].ToString());
                    Console.Write("\n\n");
                }
                else
                {
                    Console.Write("|Empty Slot|");
                }
            }
        }
    }
}
