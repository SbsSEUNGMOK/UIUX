using System;
using TMPro;
using UnityEngine;

public class ConfirmPopup : MonoBehaviour
{
    public static ConfirmPopup Instance { get; private set; }

    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text contextText;

    Action<bool> onCallback;

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
    public void ShowPopup(string context, Action<bool> onCallback)
    {
        this.onCallback = onCallback;
        contextText.text = context;
        panel.SetActive(true);
    }
    public void OnSelectButton(bool isConfirm)
    {
        onCallback?.Invoke(isConfirm);
        panel.SetActive(false);
    }
}
