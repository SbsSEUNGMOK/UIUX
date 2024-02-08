using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    [SerializeField] Image iconImage;
    [SerializeField] Image fillImage;
    [SerializeField] TMP_Text timeText;

    bool isUpdate;

    private void Awake()
    {
        ResetIcon();
    }

    public bool SetTime(float time)
    {
        if (isUpdate)
            return false;

        StartCoroutine(IEUpdate(time));
        return true;
    }
    private IEnumerator IEUpdate(float maxTime)
    {
        isUpdate = true;
        iconImage.color = new Color(0.3f, 0.3f, 0.3f);
        float time = maxTime;
        while(time > 0.0f)
        {
            fillImage.fillAmount = time / maxTime;
            time = Mathf.Clamp(time - Time.deltaTime, 0.0f, maxTime);
            timeText.text = (time < 1.0f) ? time.ToString("N1") : Mathf.FloorToInt(time).ToString("N0");
            yield return null;
        }

        ResetIcon();
    }
    private void ResetIcon()
    {
        iconImage.color = Color.white;
        fillImage.fillAmount = 0f;
        timeText.text = string.Empty;
        isUpdate = false;
    }

    public void OnDisableSkillIcon()
    {
        Debug.Log("스킬 아이콘 비활성화");
    }

}
