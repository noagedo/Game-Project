using UnityEngine;

public class SignSwing : MonoBehaviour
{
    public float torqueForce = 1f;
    public float interval = 2f;

    private float timer = 0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            float randomDirection = Random.Range(-1f, 1f);
            rb.AddTorque(transform.forward * randomDirection * torqueForce, ForceMode.Impulse);
            timer = 0f;
        }
    }
}
