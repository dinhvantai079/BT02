using System;
using System.Collections.Generic;

class Bai01
{
    static void Main(string[] args)
    {
        List<Item> items = new List<Item>
        {
            new Item { Name = "Item1", Value = 60, Weight = 10 },
            new Item { Name = "Item2", Value = 100, Weight = 20 },
            new Item { Name = "Item3", Value = 120, Weight = 30 },
            new Item { Name = "Item4", Value = 140, Weight = 40 }
        };

        int capacity = 70;

        List<Item> selectedItems;
        double maxValue = GreedyKnapsack(capacity, items, out selectedItems);

        Console.WriteLine("Maximum value in Knapsack = " + maxValue);
        Console.WriteLine("Items included in the Knapsack:");
        foreach (var item in selectedItems)
        {
            Console.WriteLine($"Name: {item.Name}, Value: {item.Value}, Weight: {item.Weight}, Ratio: {item.Ratio}");
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public int Weight { get; set; }
        public double Ratio { get { return (double)Value / Weight; } }
    }

    public static double GreedyKnapsack(int capacity, List<Item> items, out List<Item> selectedItems)
    {
        // Sort items by value-to-weight ratio in descending order
        items.Sort((x, y) => y.Ratio.CompareTo(x.Ratio));

        double totalValue = 0;
        int currentWeight = 0;
        selectedItems = new List<Item>();

        foreach (var item in items)
        {
            // If the item can fit into the remaining capacity, take it whole
            if (currentWeight + item.Weight <= capacity)
            {
                currentWeight += item.Weight;
                totalValue += item.Value;
                selectedItems.Add(item);
            }
            else
            {
                // Take the fraction of the remaining capacity
                int remainingWeight = capacity - currentWeight;
                totalValue += item.Value * ((double)remainingWeight / item.Weight);
                // Add the fraction of the item to the selected items list
                selectedItems.Add(new Item { Name = item.Name, Value = (int)(item.Value * ((double)remainingWeight / item.Weight)), Weight = remainingWeight });
                break;
            }
        }

        return totalValue;
    }
}
