using MK1.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : Item
{
    public float value;
    public override string itemCode => CreateID(itemType, number);
    protected override ITEMTYPE itemType => ITEMTYPE.USEABLE;
    public override int maxCount => 99;

    public override int compare => (int)itemType * 1000 + number;

    public override Item Copy(int count)
    {
        UseItem newItem = new UseItem();
        newItem.Count = Mathf.Clamp(count, 1, maxCount);
        newItem.itemName = itemName;
        newItem.context = context;
        newItem.sprite = sprite;
        newItem.number = number;
        newItem.price = price;
        return newItem;
    }
}
