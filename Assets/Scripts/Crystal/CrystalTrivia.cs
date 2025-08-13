using UnityEngine;
public class CrystalTrivia : MonoBehaviour
{
    public TriviaManager triviaManager;
    public Transform teleportPoint;
    private Transform playerTransform;
    private bool triggered = false;
    private Collider crystalCollider;
    private void Start()
    {
        crystalCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            if (crystalCollider != null)
                crystalCollider.enabled = false; // מונעים כניסה חוזרת בזמן Trivia
            playerTransform = other.transform;
            triviaManager.player = playerTransform;
            triviaManager.teleportPoint = teleportPoint;
            triviaManager.ShowQuestion(OnCorrectAnswer, TeleportPlayer);
        }
    }
    private void OnCorrectAnswer()
    {
        Debug.Log("Player answered correctly. Crystal collected!");
        GameManager.AddCrystals(1);
        gameObject.SetActive(false); // לוקחים את הקריסטל
        triggered = false;
    }
    private void TeleportPlayer()
    {
        Debug.Log("TeleportPlayer called!");
        if (playerTransform != null)
        {
            CharacterController controller = playerTransform.GetComponent<CharacterController>();
            if (controller != null)
                controller.enabled = false;
            playerTransform.position = teleportPoint.position;
            if (controller != null)
                controller.enabled = true;
        }
        triviaManager.ResetQuestion(); // מאפשר לנסות שוב
        if (crystalCollider != null)
            crystalCollider.enabled = true; // אפשרות לנסות שוב
        triggered = false;
    }
}
