using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    static ItemDB instance;
    public static ItemDB Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject newObject = new GameObject("ItemDB");
                instance = newObject.AddComponent<ItemDB>();
                instance.items = Resources.LoadAll<Item>("Item");
            }
            return instance;
        }
    }

    private Item[] items;
    public Item GetItem(string id, int count = 1)
    {
        Item search = null;
        foreach (Item item in items)
        {
            if (item.EquipID(id))
            {
                search = item.Copy(count);
                break;
            }
        }
        return search;
    }

  
}
