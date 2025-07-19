using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PatrolEnemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    private NavMeshAgent agent;
    private int currentPoint = 0;
    private bool isWaiting = false; // ��� �������

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GoToNextPoint();
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.destination = patrolPoints[currentPoint].position;
        currentPoint = (currentPoint + 1) % patrolPoints.Length;
        isWaiting = false; // ����� ��� ������
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f && !isWaiting)
        {
            StartCoroutine(WaitAndMove());
        }
    }

    IEnumerator WaitAndMove()
    {
        isWaiting = true; // ���� ������ ����� �����
        yield return new WaitForSeconds(2);
        GoToNextPoint();
    }
}
