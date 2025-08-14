using UnityEngine;

public class CrystalPickupScript : MonoBehaviour
{
    public int points = 1;
    private bool collected = false;
    private Collider col;

    void Awake()
    {
        GameManager.RegisterCrystal();
        col = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            if (col) col.enabled = false; // למנוע כפילויות טריגר

            GameManager.AddCrystals(points);   // מעלה מונה מצטבר
            GameManager.UnregisterCrystal();   // מפחית "כמה נשאר בסצנה"
            Destroy(gameObject);
        }
    }
}