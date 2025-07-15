using UnityEngine;

public class MemoryPuzzleTrigger : MonoBehaviour
{
    public GameObject puzzleCanvas;
    public GameObject crystal;

    private bool puzzleStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !puzzleStarted)
        {
            puzzleStarted = true;
            puzzleCanvas.SetActive(true);

            var manager = puzzleCanvas.GetComponent<MatchingGameManager>();
            if (manager != null)
            {
                manager.InitializeGame();
                manager.crystalToDestroy = crystal;
            }
        }
    }
}
