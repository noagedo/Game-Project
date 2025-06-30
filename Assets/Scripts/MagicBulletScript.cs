using UnityEngine;

public class MagicBulletScript : MonoBehaviour
{
    public int damage = 1;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GoblinHealthScript goblinHealth = collision.gameObject.GetComponent<GoblinHealthScript>();
            if (goblinHealth != null)
            {
                goblinHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}
