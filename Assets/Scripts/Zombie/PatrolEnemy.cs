using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PatrolEnemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    private NavMeshAgent agent;
    private int currentPoint = 0;

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
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            StartCoroutine(WaitAndMove());
    }

    IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(2); 
        GoToNextPoint();
    }
}
