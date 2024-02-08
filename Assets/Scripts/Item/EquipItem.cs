using MK1.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "equipment", menuName = "Item/Equipment")]
public class EquipItem : Item
{
    [SerializeField] EQUIP_TYPE equipType;
    [SerializeField] int level;
    [SerializeField] int power;
    [SerializeField] int defence;

    public override string itemCode => CreateID(itemType, equipType, number);
    protected override ITEMTYPE itemType => ITEMTYPE.EQUIPMENT;
    public override int maxCount => 1;

    public override int compare => (int)itemType * 1000 + ((int)equipType * 100) + number;

    public override Item Copy(int count)
    {
        EquipItem newItem = new EquipItem();
        newItem.Count = Mathf.Clamp(count, 1, maxCount);
        newItem.itemName = itemName;
        newItem.context = context;
        newItem.sprite = sprite;
        newItem.number = number;
        newItem.price = price;
        newItem.equipType = equipType;
        newItem.level = level;
        newItem.power = power;
        newItem.defence = defence;
        return newItem;
    }
}
