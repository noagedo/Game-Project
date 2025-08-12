using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TriviaManager : MonoBehaviour
{
    public GameObject triviaPanel;
    public Text questionText;
    public Text feedbackText;
    public Button[] answerButtons;

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

    public void ShowQuestion(System.Action onCorrect, System.Action onWrong)
    {
        if (questionAnswered) return;

        onCorrectCallback = onCorrect;
        onWrongCallback = onWrong;

        triviaPanel.SetActive(true);
        questionText.text = "What is the number of Goblins in scene 1?";

        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "4";
        answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "5";
        answerButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = "6";

        foreach (Button btn in answerButtons)
            btn.onClick.RemoveAllListeners();

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

        if (isCorrect)
        {
            feedbackText.text = "Correct Answer!";
            triviaPanel.SetActive(false);
            questionAnswered = true;
            onCorrectCallback?.Invoke();
        }
        else
        {
            feedbackText.text = "Wrong answer, teleporting...";
            triviaPanel.SetActive(false);
            onWrongCallback?.Invoke();
            questionAnswered = false;
        }
    }
}
