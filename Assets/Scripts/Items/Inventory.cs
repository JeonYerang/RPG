using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;

    private void Awake()
    {
        items = new List<Item>();
    }

    public void PushItem(Item item)
    {
        items.Add(item);
    }

    public Item PopItem(int num)
    {
        Item item = items[num];
        items.RemoveAt(num);
        return item;
    }
}
