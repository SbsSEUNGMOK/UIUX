using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignIn : MonoBehaviour
{
    [SerializeField] Toggle toggleAll;
    [SerializeField] Toggle[] toggleNeeds;
    [SerializeField] Toggle[] toggleSelects;
    [SerializeField] Button nextButton;

    private bool isLockToggle;

    // 오브젝트가 활성화되는 순간 호출.
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        OnPressToggleAll(false);
    }    

    public void OnPressToggleAll(bool isOn)
    {
        if (isLockToggle)
            return;

        isLockToggle = true;
        toggleAll.isOn = isOn;

        foreach (Toggle toggle in toggleNeeds)
            toggle.isOn = isOn;
        foreach (Toggle toggle in toggleSelects)
            toggle.isOn = isOn;

        nextButton.interactable = isOn;
        isLockToggle = false;
    }
    public void OnPressToggleOption()
    {
        if (isLockToggle)
            return;
        isLockToggle = true;

        // 필수 그룹이 전체 선택 되었는가?
        bool isOnAllNeed = true;
        foreach (Toggle toggle in toggleNeeds)
            isOnAllNeed = isOnAllNeed && toggle.isOn;

        // 선택 그룹이 전체 선택 되었는가?
        bool isOnAllSelect = true;
        foreach (Toggle toggle in toggleSelects)
            isOnAllSelect = isOnAllSelect && toggle.isOn;

        toggleAll.isOn = isOnAllNeed && isOnAllSelect;
        nextButton.interactable = isOnAllNeed;
        isLockToggle = false;
    }

    private void OnApplicationPause(bool pause)
    {
        Debug.Log("OnApplicationFocus : " + pause);
    }
    private void OnApplicationFocus(bool focus)
    {
        Debug.Log("OnApplicationFocus : " + focus);
    }
    private void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit");
    }
}
