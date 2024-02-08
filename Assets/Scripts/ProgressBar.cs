using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [System.Serializable]
    struct HpInfo
    {
        public float maxValue;
        public Color color;
    }

    [SerializeField] Image fill;
    [SerializeField] HpInfo[] hpInfos;

    public void UpdateBar(float current, float max, float time = 0f)
    {
        StartCoroutine(IEUpdate(current / max, time));
    }
    private IEnumerator IEUpdate(float current, float maxTime)
    {
        float prev = fill.fillAmount;                                       // 이전 비율 값.
        float offset = current - prev;                                      // 이전과 현재의 차이 값.
        float time = 0.0f;                                                  // 시간 값.
        if(maxTime <= 0.0f)
        {
            time = 1f;
            maxTime = 1f;
        }

        do
        {
            time = Mathf.Clamp(time + Time.deltaTime, 0.0f, maxTime);       // 시간 값 증가.
            fill.fillAmount = prev + (offset * time / maxTime);             // 이전 값 + (차이 * 시간 비율)

            // 현재 체력 비율에 해당하는 색상 값 찾기.
            HpInfo info = System.Array.Find(hpInfos, (info) => fill.fillAmount <= info.maxValue);
            fill.color = info.color;

            yield return null;                                              // 1프레임 대기.
        }
        while (time <= maxTime);                                            // 시간이 최대 시간보다 적을 경우.
    }

    public void OnEndProgress()
    {
        Debug.Log("프로그래스 종료");
    }
}
