using UnityEngine;

public class WolfSimpleAI : MonoBehaviour
{
    private Animator animator;

    // ����� ������ ��� �����
    private float timer = 0f;
    private float howlDuration = 5f; // ����� �� 5 �����

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isHowling", false);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= howlDuration)
        {
            // ���� �����
            animator.SetBool("isHowling", true);
        }
        else if (timer >= howlDuration + 3f) 
        {
            animator.SetBool("isHowling", false);
            timer = 0f;
        }
    }
}
