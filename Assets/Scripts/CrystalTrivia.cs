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
        if (playerTransform != null)
        {
            playerTransform.position = teleportPoint.position;
        }

        triggered = false; 
    }
}
