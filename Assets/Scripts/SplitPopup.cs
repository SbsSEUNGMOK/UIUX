using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SplitPopup : MonoBehaviour
{
    public static bool isShowingPopup;
    public static SplitPopup Instance { get; private set; }

    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text totalCountText;
    [SerializeField] TMP_InputField inputField;

    Action<int> onCallback;
    int totalCount;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        panel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            OnSelectButton(true);
        else if (Input.GetKeyDown(KeyCode.Escape))
            OnSelectButton(false);
    }

    public void ShowPopup(int totalCount, Action<int> onCallback)
    {
        isShowingPopup = true;

        this.onCallback = onCallback;
        this.totalCount = totalCount;

        inputField.text = (totalCount / 2).ToString();
        totalCountText.text = $"최대 개수 : {totalCount}";
        panel.SetActive(true);
    }

    bool isFlag;
    public void OnChangeSplitCount(string str)
    {
        if (isFlag)
            return;

        // 허용하지 않는 범위의 값을 입력할 경우 조정한다.
        isFlag = true;
        int.TryParse(str, out int count);
        count = Mathf.Clamp(count, 1, totalCount);
        inputField.text = count.ToString();
        isFlag = false;
    }
    public void OnSelectButton(bool isConfirm)
    {
        if(!isConfirm)
        {
            onCallback?.Invoke(-1);
            panel.SetActive(false);
            return;
        }

        // 요청자에게 입력된 개수 반환.
        int.TryParse(inputField.text, out int count);
        onCallback?.Invoke(count);
        panel.SetActive(false);

        isShowingPopup = false;
    }
}
