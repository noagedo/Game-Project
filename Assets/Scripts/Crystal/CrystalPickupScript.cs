using UnityEngine;
using UnityEngine.SceneManagement;

public class CrystalPickupScript : MonoBehaviour
{
    public int points = 1;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by " + other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Crystal collected!");
            GameManager.AddCrystals(points);
            Destroy(gameObject);
        }
    }
}
