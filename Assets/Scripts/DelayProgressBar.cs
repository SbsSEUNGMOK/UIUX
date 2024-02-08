using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayProgressBar : MonoBehaviour
{
    [SerializeField] Image damageFill;
    [SerializeField] Image hpFill;
    [SerializeField] float maxDelayTime;

    float currentRatio;     // 현재 비율 (hp fill)
    float delayTime;        // 대기 시간.

    private void Start()
    {
        damageFill.fillAmount = 1f;
        hpFill.fillAmount = 1f;

        currentRatio = 1f;
        delayTime = 0.0f;
    }

    private void Update()
    {
        // 딜레이 시간이 남았다면 지속적으로 감소시킨다.
        if(delayTime > 0.0f)
            delayTime = Mathf.Clamp(delayTime - Time.deltaTime, 0.0f, maxDelayTime);

        // delay시간이 지난 후 체력을 깍는다.
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
        Debug.Log("딜레이 프로그래스 바 강제 종료");
    }
}
