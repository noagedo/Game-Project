using UnityEngine;

public class StealthVision : MonoBehaviour
{
    public Transform player; // גררי את השחקן לכאן
    public float sightRange = 10f; // טווח ראייה של השד
    public LayerMask playerLayer; // שכבת השחקן (למשל "Player")
    public LayerMask obstacleLayer; // שכבת מכשולים (עצים, קירות וכו')

    void Update()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // בדיקה אם השחקן נמצא בטווח הראייה
        if (distanceToPlayer <= sightRange)
        {
            Ray ray = new Ray(transform.position + Vector3.up, directionToPlayer.normalized);
            RaycastHit hit;

            // Raycast שמזהה אם יש קו ראייה ישיר אל השחקן (בלי מכשולים)
            if (Physics.Raycast(ray, out hit, sightRange, playerLayer | obstacleLayer))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Debug.Log("👀 השחקנית נראתה! מתחיל רדיפה.");
                    // כאן את יכולה לקרוא לפונקציית רדיפה או לעדכן משתנה:
                    // לדוגמה:
                    // GetComponent<ChaseEnemy>().StartChase();
                }
            }
        }
    }
}
