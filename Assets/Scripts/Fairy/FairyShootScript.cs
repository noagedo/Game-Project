using UnityEngine;

public class FairyShootScript : MonoBehaviour
{
    public GameObject magicBulletPrefab;
    public Transform shootPoint;
    public float bulletSpeed = 10f;

    void Update()
    {

        Animator animator = GetComponent<Animator>();
        if (animator != null && animator.GetBool("isDead")) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(magicBulletPrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = shootPoint.forward * bulletSpeed; // �����: velocity, �� linearVelocity
        }
    }
}
