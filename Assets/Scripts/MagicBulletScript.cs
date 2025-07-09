using UnityEngine;

public class MagicBulletScript : MonoBehaviour
{
    public int damage = 1;
    public float explosionRadius = 3f; // רדיוס פגיעה רחב יותר

    void OnCollisionEnter(Collision collision)
    {
        // פגיעה בכל מי שנמצא סביב מקום הפיצוץ
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                GoblinHealthScript goblinHealth = hit.GetComponent<GoblinHealthScript>();
                if (goblinHealth != null)
                {
                    goblinHealth.TakeDamage(damage);
                    Debug.Log("🎯 קסם פגע בגובלין: " + hit.name);
                }
            }
        }

        Destroy(gameObject); // השמדת הכדור הקסום לאחר הפיצוץ
    }

    // אופציונלי – לראות את הרדיוס בגיזמוס
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
