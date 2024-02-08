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
            Debug.Log("�������� �����ϴ�.");
            return;
        }

        // ��ø�õ�
        if (item.Count > 1 && !isPushBlankForce)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null || !items[i].EquipID(item))
                    continue;

                // i��° �����ۿ� ��ø�� �õ��ϰ� ��ø�� �����ٸ� �����Ѵ�.
                if (items[i].Overlap(item))
                {
                    UpdateUI();
                    return;
                }
            }
        }

        // ��û �õ� ���Ŀ��� �������� ������ ���Ҵ�. ���ο� ĭ�� ��������.
        int blankIndex = Array.IndexOf(items, null);
        if (blankIndex != -1)
            items[blankIndex] = item;

        UpdateUI();
    }        
    public void SelectItem(int index, bool isInside, bool isSplit)
    {
        // ���� ���� : ������ ������ ���� ���.
        if (selectedItem == null && index >= 0 && isInside)
        {
            SelectNewItem();
        }
        else if (selectedItem != null)
        {
            // �κ��丮 ���ο��� Ŭ��.
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
                // �׳� ����
                selectedItem = clickItem;
                items[index] = null;
                UpdateUI();
            }            
        }
        void MoveItem()
        {
            if (index < 0)
                return;

            // �ش� ������ ����ִٸ�...
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

            // ���� ������ �������� Ŭ������ ���.
            else if (items[index].EquipID(selectedItem))
            {
                if (isSplit && selectedItem.Count > 1)
                {
                    SplitPopup.Instance.ShowPopup(selectedItem.Count, (split) => {
                        if (split > 0)
                        {
                            // ���� ����ڰ� ���ϴ� ��ŭ �������� ������.
                            var group = selectedItem.Divide(split);
                            selectedItem = group.origin;

                            // ������ �������� index��°�� �����غ���
                            // ���� ������ �ٽ� ���� �����ۿ� �����Ѵ�.
                            if (!items[index].Overlap(group.split))
                                selectedItem.Overlap(group.split);

                            UpdateUI();
                        }
                    });
                }
                else
                {
                    // ���� �������� ���Կ� �� ���� ���.
                    if (items[index].Overlap(selectedItem))
                        selectedItem = null;
                }
            }

            // �ٸ� ������ �������� Ŭ������ ���.
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
