using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] RectTransform itemSlotParent;      // ������ ���� �θ� ������Ʈ.
    [SerializeField] RectTransform slotInsideRect;      // ������ ���� ����.
    [SerializeField] ItemSlotUI previewSlot;            // �̸����� ����.

    Inventory inven;
    ItemSlotUI[] slots;

    public void Setup(Inventory inven)
    {
        this.inven = inven;

        slots = itemSlotParent.GetComponentsInChildren<ItemSlotUI>(true);
        previewSlot.gameObject.SetActive(false);
    }
    public void SwitchInventory()
    {
        // gameObject.activeSelf:bool = ������Ʈ Ȱ��ȭ ����
        gameObject.SetActive(!gameObject.activeSelf);
    }
    public void UpdateItem(Item[] items, Item selectedItem)
    {
        for(int i = 0; i<items.Length; i++)
            slots[i].UpdateItem(items[i]);

        previewSlot.UpdateItem(selectedItem);
        previewSlot.gameObject.SetActive(selectedItem);
    }
    public void OnClickSort()
    {
        inven.SortItem();
    }


    private void Update()
    {
        previewSlot.transform.position = Input.mousePosition;
        if(Input.GetMouseButtonDown(0) && !SplitPopup.isShowingPopup)
            OnClickMouseButton();
    }

    private void OnClickMouseButton()
    {
        // inven���� Ŭ�� �̺�Ʈ�� �����ϰ� ����, ���� ���¸� ���Ϲ޴´�.
        bool isInside = RectTransformUtility.RectangleContainsScreenPoint(slotInsideRect, Input.mousePosition);
        bool isCtrl = Input.GetKey(KeyCode.LeftControl);
        int slotIndex = -1;
        if (isInside)
        {
            ItemSlotUI selectedSlot = RayToSlotUI();
            slotIndex = selectedSlot?.transform.GetSiblingIndex() ?? -1;
        }

        // �κ��丮���� Ŭ�� �̺�Ʈ�� �����Ѵ�.
        // �ٸ�, ���� ������ �� ���� �ֱ� ������ �̺�Ʈ�� ó���Ѵ�.
        // �̺�Ʈ ���� ���� �������� �������� �����̴�.
        inven.SelectItem(slotIndex, isInside, isCtrl);
    }
    private ItemSlotUI RayToSlotUI()
    {
        // ������ �̺�Ʈ�� ���� Ȱ�� �̺�Ʈ�� �����ϰ� ��ġ ���� ���콺�� �����.
        PointerEventData pointEvent = new PointerEventData(EventSystem.current);
        pointEvent.position = Input.mousePosition;

        // ���콺 Ŭ�� Ray�� UI������ ���.
        List<RaycastResult> resultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointEvent, resultList);
        foreach (RaycastResult result in resultList)
        {
            ItemSlotUI slotUI = result.gameObject.GetComponent<ItemSlotUI>();
            if (slotUI != null)
                return slotUI;
        }

        return null;
    }
}
