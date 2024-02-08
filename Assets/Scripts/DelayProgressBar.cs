using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayProgressBar : MonoBehaviour
{
    [SerializeField] Image damageFill;
    [SerializeField] Image hpFill;
    [SerializeField] float maxDelayTime;

    float currentRatio;     // ���� ���� (hp fill)
    float delayTime;        // ��� �ð�.

    private void Start()
    {
        damageFill.fillAmount = 1f;
        hpFill.fillAmount = 1f;

        currentRatio = 1f;
        delayTime = 0.0f;
    }

    private void Update()
    {
        // ������ �ð��� ���Ҵٸ� ���������� ���ҽ�Ų��.
        if(delayTime > 0.0f)
            delayTime = Mathf.Clamp(delayTime - Time.deltaTime, 0.0f, maxDelayTime);

        // delay�ð��� ���� �� ü���� ��´�.
        if(delayTime <= 0.0f && damageFill.fillAmount != currentRatio)
            damageFill.fillAmount = Mathf.MoveTowards(damageFill.fillAmount, currentRatio, Time.deltaTime);
    }
    
    public void UpdateBar(float current, float max)
    {
        currentRatio = current / max;
        delayTime = maxDelayTime;
        hpFill.fillAmount = current / max;
    }

    public void OnShutdown()
    {
        Debug.Log("������ ���α׷��� �� ���� ����");
    }
}
