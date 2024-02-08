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
        float prev = fill.fillAmount;                                       // ���� ���� ��.
        float offset = current - prev;                                      // ������ ������ ���� ��.
        float time = 0.0f;                                                  // �ð� ��.
        if(maxTime <= 0.0f)
        {
            time = 1f;
            maxTime = 1f;
        }

        do
        {
            time = Mathf.Clamp(time + Time.deltaTime, 0.0f, maxTime);       // �ð� �� ����.
            fill.fillAmount = prev + (offset * time / maxTime);             // ���� �� + (���� * �ð� ����)

            // ���� ü�� ������ �ش��ϴ� ���� �� ã��.
            HpInfo info = System.Array.Find(hpInfos, (info) => fill.fillAmount <= info.maxValue);
            fill.color = info.color;

            yield return null;                                              // 1������ ���.
        }
        while (time <= maxTime);                                            // �ð��� �ִ� �ð����� ���� ���.
    }

    public void OnEndProgress()
    {
        Debug.Log("���α׷��� ����");
    }
}
