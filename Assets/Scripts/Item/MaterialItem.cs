using MK1.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "materialItem", menuName = "Item/Material")]
public class MaterialItem : Item
{
    public override string itemCode => CreateID(itemType, number);
    protected override ITEMTYPE itemType => ITEMTYPE.MATERIAL;
    public override int maxCount => 99;
    public override int compare => (int)itemType * 1000 + number;

    public override Item Copy(int count)
    {
        MaterialItem newItem = new MaterialItem();
        newItem.Count = Mathf.Clamp(count, 1, maxCount);
        newItem.itemName = itemName;
        newItem.context = context;
        newItem.sprite = sprite;
        newItem.number = number;
        newItem.price = price;
        return newItem;
    }
}
