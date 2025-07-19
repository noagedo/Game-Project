using UnityEngine;

public class FairyShootScript : MonoBehaviour
{
    public GameObject magicBulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public int numberOfBullets = 5;
    public float spreadAngle = 15f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // לדוגמה, ירי כשנלחץ רווח
        {
            ShootSpread();
        }
    }

    void ShootSpread()
    {
        int mid = numberOfBullets / 2;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = (i - mid) * spreadAngle;
            Quaternion rotation = Quaternion.Euler(0, angle, 0) * firePoint.rotation;

            GameObject bullet = Instantiate(magicBulletPrefab, firePoint.position, rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = rotation * Vector3.forward * bulletSpeed;
        }
    }
}
