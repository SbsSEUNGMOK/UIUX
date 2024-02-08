using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Hp")]
    [SerializeField] ProgressBar progressBar;
    [SerializeField] DelayProgressBar delayProgressBar;
    [SerializeField] float maxHP;
    [SerializeField] float hp;

    [Header("Etc")]
    [SerializeField] SkillIcon iconQ;
    [SerializeField] UnityEvent onDeadPlayer;


    private void Start()
    {
        progressBar.UpdateBar(hp, maxHP);
        
    }
 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            TakeDamage(Random.Range(300, 1000));
        if (Input.GetKeyDown(KeyCode.S))
            TakeDamage(Random.Range(3500, 4000));
        if (Input.GetKey(KeyCode.Q))
            iconQ.SetTime(3f);
    }
    private void TakeDamage(float damage)
    {   
        hp -= damage;
        delayProgressBar.UpdateBar(hp, maxHP);
        Debug.Log($"�÷��̾ {damage}�� �������� �޾Ҵ�!");

        if(hp <= 0)
        {
            Debug.Log("�÷��̾� ���");
            onDeadPlayer?.Invoke();
        }
    }
}
