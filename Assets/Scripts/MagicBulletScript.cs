using UnityEngine;

public class MagicBulletScript : MonoBehaviour
{
    public int damage = 1;
    public float explosionRadius = 3f; 

    void OnCollisionEnter(Collision collision)
    {
        
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

        Destroy(gameObject); 
    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
