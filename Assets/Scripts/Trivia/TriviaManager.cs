using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TriviaManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject triviaPanel;
    public Text questionText;
    public Text feedbackText;
    public Button[] answerButtons;
    [Header("Player Settings")]
    public Transform player;
    public Transform teleportPoint;
    private System.Action onCorrectCallback;
    private System.Action onWrongCallback;
    private bool questionAnswered = false;
    void Start()
    {
        if (triviaPanel != null)
            triviaPanel.SetActive(false);
        if (feedbackText != null)
            feedbackText.text = "";
    }
    public void ShowQuestion(System.Action onCorrect = null, System.Action onWrong = null)
    {
        if (questionAnswered) return;
        onCorrectCallback = onCorrect;
        onWrongCallback = onWrong ?? DefaultTeleport;
        triviaPanel.SetActive(true);
        questionText.text = "What is the number of Goblins in scene 1?";
        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "4";
        answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "5";
        answerButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = "6";
        foreach (Button btn in answerButtons)
            btn.onClick.RemoveAllListeners();
        // הגדרת התשובות
        answerButtons[0].onClick.AddListener(() => Answer(false));
        answerButtons[1].onClick.AddListener(() => Answer(false));
        answerButtons[2].onClick.AddListener(() => Answer(true));
    }
    void Update()
    {
        if (!triviaPanel.activeSelf) return;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            answerButtons[0].onClick.Invoke();
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            answerButtons[1].onClick.Invoke();
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            answerButtons[2].onClick.Invoke();
    }
    void Answer(bool isCorrect)
    {
        if (feedbackText == null) return;
        triviaPanel.SetActive(false);
        if (isCorrect)
        {
            feedbackText.text = "Correct Answer!";
            questionAnswered = true;
            onCorrectCallback?.Invoke();
        }
        else
        {
            feedbackText.text = "Wrong answer, teleporting...";
            onWrongCallback?.Invoke();
        }
    }
    public void ResetQuestion()
    {
        questionAnswered = false;
    }
    private void DefaultTeleport()
    {
        if (player != null && teleportPoint != null)
            player.position = teleportPoint.position;
    }
}
