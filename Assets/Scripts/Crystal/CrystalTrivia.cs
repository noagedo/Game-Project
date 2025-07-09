using UnityEngine;

public class CrystalTrivia : MonoBehaviour
{
    public TriviaManager triviaManager;
    public Transform teleportPoint;
    private bool triggered = false;

    private Transform playerTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            playerTransform = other.transform;
            triviaManager.ShowQuestion(OnCorrectAnswer, TeleportPlayer);
        }
    }

    void OnCorrectAnswer()
    {
        gameObject.SetActive(false); 
    }

    void TeleportPlayer()
    {
        Debug.Log("🚀 TeleportPlayer called!");

        if (playerTransform != null)
        {
            // נטרול CharacterController זמנית
            CharacterController controller = playerTransform.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
            }

            playerTransform.position = teleportPoint.position;

            // הפעלה מחדש
            if (controller != null)
            {
                controller.enabled = true;
            }
        }

        triggered = false;
    }

}
