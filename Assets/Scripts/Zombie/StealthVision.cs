using UnityEngine;

public class StealthVision : MonoBehaviour
{
    public Transform player; 
    public float sightRange = 10f; 
    public LayerMask playerLayer; 
    public LayerMask obstacleLayer; 

    void Update()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        
        if (distanceToPlayer <= sightRange)
        {
            Ray ray = new Ray(transform.position + Vector3.up, directionToPlayer.normalized);
            RaycastHit hit;

            
            if (Physics.Raycast(ray, out hit, sightRange, playerLayer | obstacleLayer))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Debug.Log("👀 השחקנית נראתה! מתחיל רדיפה.");
                   
                }
            }
        }
    }
}
