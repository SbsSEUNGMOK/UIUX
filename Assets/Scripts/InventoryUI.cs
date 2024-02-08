using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] RectTransform itemSlotParent;      // 아이템 슬롯 부모 오브젝트.
    [SerializeField] RectTransform slotInsideRect;      // 슬롯의 내부 영역.
    [SerializeField] ItemSlotUI previewSlot;            // 미리보기 슬롯.

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
        // gameObject.activeSelf:bool = 오브젝트 활성화 여부
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
        // inven에게 클릭 이벤트를 전달하고 선택, 비선택 상태를 리턴받는다.
        bool isInside = RectTransformUtility.RectangleContainsScreenPoint(slotInsideRect, Input.mousePosition);
        bool isCtrl = Input.GetKey(KeyCode.LeftControl);
        int slotIndex = -1;
        if (isInside)
        {
            ItemSlotUI selectedSlot = RayToSlotUI();
            slotIndex = selectedSlot?.transform.GetSiblingIndex() ?? -1;
        }

        // 인벤토리에게 클릭 이벤트를 전달한다.
        // 다만, 지연 실행이 될 수도 있기 때문에 이벤트로 처리한다.
        // 이벤트 전달 값은 선택중인 아이템의 정보이다.
        inven.SelectItem(slotIndex, isInside, isCtrl);
    }
    private ItemSlotUI RayToSlotUI()
    {
        // 포인터 이벤트를 현재 활성 이벤트로 생성하고 위치 값을 마우스에 맞춘다.
        PointerEventData pointEvent = new PointerEventData(EventSystem.current);
        pointEvent.position = Input.mousePosition;

        // 마우스 클릭 Ray를 UI용으로 쏜다.
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
