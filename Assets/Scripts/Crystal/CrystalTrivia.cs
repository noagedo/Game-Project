using UnityEngine;

public class CrystalTrivia : MonoBehaviour
{
    public TriviaManager triviaManager;
    public Transform teleportPoint;
    private Transform playerTransform;
    private bool triggered = false;  
    private bool collected = false;   
    private Collider crystalCollider;

    void Awake()
    {
       
        GameManager.RegisterCrystal();
    }

    void Start()
    {
        crystalCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;                
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            if (crystalCollider != null)
                crystalCollider.enabled = false; 

            playerTransform = other.transform;
            triviaManager.player = playerTransform;
            triviaManager.teleportPoint = teleportPoint;

           
            triviaManager.ShowQuestion(OnCorrectAnswer, TeleportPlayer);
        }
    }

    private void OnCorrectAnswer()
    {
        if (collected) return; 
        collected = true;

        Debug.Log("Player answered correctly. Crystal collected!");

        
        GameManager.AddCrystals(1);

       
        GameManager.UnregisterCrystal();

        
        Destroy(gameObject);
        
    }

    private void TeleportPlayer()
    {
        Debug.Log("TeleportPlayer called!");
        if (playerTransform != null)
        {
            var controller = playerTransform.GetComponent<CharacterController>();
            if (controller != null) controller.enabled = false;
            playerTransform.position = teleportPoint.position;
            if (controller != null) controller.enabled = true;
        }

       
        triviaManager.ResetQuestion();

        if (!collected && crystalCollider != null)
            crystalCollider.enabled = true; 

        triggered = false; 
    }
}