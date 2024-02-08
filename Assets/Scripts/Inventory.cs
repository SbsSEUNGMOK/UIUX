using UnityEngine;
using MK1.Item;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] InventoryUI invenUI;

    Item[] items;
    Item selectedItem;

    private void Start()
    {
        items = new Item[100];
        selectedItem = null;
        invenUI.Setup(this);

        AddItem(ItemDB.Instance.GetItem(Item.CreateID(ITEMTYPE.EQUIPMENT, EQUIP_TYPE.CLOTH, 0), 3));
        AddItem(ItemDB.Instance.GetItem(Item.CreateID(ITEMTYPE.EQUIPMENT, EQUIP_TYPE.CLOTH, 0)));
        AddItem(ItemDB.Instance.GetItem(Item.CreateID(ITEMTYPE.EQUIPMENT, EQUIP_TYPE.SHOES, 0)));
        AddItem(ItemDB.Instance.GetItem(Item.CreateID(ITEMTYPE.MATERIAL, 0), 30), true);
        AddItem(ItemDB.Instance.GetItem(Item.CreateID(ITEMTYPE.MATERIAL, 0), 40), true);
        AddItem(ItemDB.Instance.GetItem(Item.CreateID(ITEMTYPE.MATERIAL, 0), 50), true);
        AddItem(ItemDB.Instance.GetItem(Item.CreateID(ITEMTYPE.MATERIAL, 1), 50), true);
        AddItem(ItemDB.Instance.GetItem(Item.CreateID(ITEMTYPE.MATERIAL, 1), 50), true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            invenUI.SwitchInventory();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            SplitPopup.Instance.ShowPopup(20, null);
        }

    }

    public void AddItem(Item item, bool isPushBlankForce = false)
    {
        if (item == null)
        {
            Debug.Log("아이템이 없습니다.");
            return;
        }

        // 중첩시도
        if (item.Count > 1 && !isPushBlankForce)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null || !items[i].EquipID(item))
                    continue;

                // i번째 아이템에 중첩을 시도하고 중첩을 끝났다면 리턴한다.
                if (items[i].Overlap(item))
                {
                    UpdateUI();
                    return;
                }
            }
        }

        // 중청 시도 이후에도 아이템의 개수가 남았다. 새로운 칸에 대입하자.
        int blankIndex = Array.IndexOf(items, null);
        if (blankIndex != -1)
            items[blankIndex] = item;

        UpdateUI();
    }        
    public void SelectItem(int index, bool isInside, bool isSplit)
    {
        // 최초 선택 : 아이템 슬롯을 누른 경우.
        if (selectedItem == null && index >= 0 && isInside)
        {
            SelectNewItem();
        }
        else if (selectedItem != null)
        {
            // 인벤토리 내부에서 클릭.
            if (isInside)
            {
                MoveItem();
            }
            else
            {
                DropItem();
            }
        }
                
        void SelectNewItem()
        {
            Item clickItem = items[index];
            if(isSplit && clickItem.Count > 1)
            {
                SplitPopup.Instance.ShowPopup(clickItem.Count, (split) => {
                    if (split > 0)
                    {
                        var group = clickItem.Divide(split);
                        items[index] = group.origin;
                        selectedItem = group.split;
                        UpdateUI();
                    }
                });
            }
            else
            {
                // 그냥 선택
                selectedItem = clickItem;
                items[index] = null;
                UpdateUI();
            }            
        }
        void MoveItem()
        {
            if (index < 0)
                return;

            // 해당 슬롯이 비어있다면...
            if (items[index] == null)
            {
                if (isSplit && selectedItem.Count > 1)
                {
                    SplitPopup.Instance.ShowPopup(selectedItem.Count, (split) => {
                        if (split > 0)
                        {
                            var group = selectedItem.Divide(split);
                            selectedItem = group.origin;
                            items[index] = group.split;
                            UpdateUI();
                        }
                    });
                }
                else
                {
                    items[index] = selectedItem;
                    selectedItem = null;
                }
            }

            // 같은 종류의 아이템을 클릭했을 경우.
            else if (items[index].EquipID(selectedItem))
            {
                if (isSplit && selectedItem.Count > 1)
                {
                    SplitPopup.Instance.ShowPopup(selectedItem.Count, (split) => {
                        if (split > 0)
                        {
                            // 먼저 사용자가 원하는 만큼 아이템을 나눈다.
                            var group = selectedItem.Divide(split);
                            selectedItem = group.origin;

                            // 나눠진 아이템을 index번째에 대입해보고
                            // 만약 남으면 다시 선택 아이템에 병합한다.
                            if (!items[index].Overlap(group.split))
                                selectedItem.Overlap(group.split);

                            UpdateUI();
                        }
                    });
                }
                else
                {
                    // 선택 아이템이 슬롯에 다 들어갔을 경우.
                    if (items[index].Overlap(selectedItem))
                        selectedItem = null;
                }
            }

            // 다른 종류의 아이템을 클릭했을 경우.
            else
            {
                Item dummy = items[index];
                items[index] = selectedItem;
                selectedItem = dummy;
            }

            UpdateUI();
        }
        void DropItem()
        {
            if (isSplit && selectedItem.Count > 1)
            {
                SplitPopup.Instance.ShowPopup(selectedItem.Count, (split) => {
                    if (split > 0)
                    {
                        var group = selectedItem.Divide(split);
                        selectedItem = group.origin;
                        UpdateUI();
                    }
                });
            }
            else
            {
                selectedItem = null;
                UpdateUI();
            }
        }
    }
    public void SortItem()
    {
        Array.Sort(items, new ItemHandler());
        UpdateUI();
    }


    private void UpdateUI()
    {
        invenUI.UpdateItem(items, selectedItem);
    }
}

public static class InventoryHandler
{
    public static (Item origin, Item split) Divide(this Item item, int count)
    {
        Debug.Log(item);
        return (item.Copy(item.Count - count), item.Copy(count));
    }
}
